using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.Prism.Events;
using Menu = Denapoli.Modules.Data.Entities.Menu;
using Timer = System.Timers.Timer;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class CommandScreenViewModel : AbstractScreenViewModel, ICommandView
    {

        private static int idGen = 1;
        private int id = idGen++;
        public CommandScreenViewModel(IEventAggregator eventAggregator, IDataProvider dataProvider, IPaymentService paymentService, ILocalizationService localizationService, ISettingsService settingsService)
        {
            EventAggregator = eventAggregator;
            DataProvider = dataProvider;
            PaymentService = paymentService;
            LocalizationService = localizationService;
            SettingsService = settingsService;
            ScreenName = "Commande";
            IsVisible = true;
            Families = new ObservableCollection<Famille>();
            UpdateFamilles();
            Products = new ObservableCollection<Produit>();
            OrderedProdects = new ObservableCollection<ProductViewModel>();
            Borne = DataProvider.GetBorne(SettingsService.GetBorneId());
            CustomerViewModel = new CustomerViewModel {Address = Borne.Adresse,IsVisible = false, LocalizationService = localizationService};
            CustomerViewModel.PropertyChanged += CustommerViewHandler;
            PaymentService.FinishEvent += PaiementViewHandler;
            PaymentService.PropertyChanged += PaymentServiceOnPropertyChanged;
            PaiementView = new PaiementViewModel {IsVisible = false, LocalizationService = localizationService};
            ShowCustomerCommand = new ActionCommand(() =>
                                                        {
                                                            if(OrderedProdects.Count > 0)
                                                                SelectedView = CustomerViewModel;
                                                        });
            CancelCommand = new ActionCommand(() =>
                                                  {
                                                      EventAggregator.GetEvent<EndCommandEvent>().Publish(this);
                                                      Cancel();
                                                  });

            const int maxCommandDuration = 60000*5;
            var timer = new Timer { Interval = maxCommandDuration };
            timer.Elapsed += (sender, args) =>
                                 {
                                     CancelCommand.Execute(null);
                                     timer.Enabled = false;
                                     timer.Stop();
                                 };
            timer.Enabled = true;
            timer.Start();

            LeftScollImage = "scroll_left.png";
            Logo = "logo.jpg";
            SelectedView = this;
            Total = 0.0f;
        }

        private void PaymentServiceOnPropertyChanged(object o, PropertyChangedEventArgs propertyChangedEventArgs)
        {
           PaiementView.ScreenMessage = PaymentService.Message;
        }

        private void UpdateFamilles()
        {
            Families.Clear();
            DataProvider.GetAvailableFamilies().Where(f=>f.IsActif&&f.IsApp).ForEach(item => Families.Add(item));
        }

        public void Cancel()
        {
            PaymentService.FinishEvent -= PaiementViewHandler;
            PaymentService.PropertyChanged -= PaymentServiceOnPropertyChanged;
        }

        private void PaiementViewHandler(object sender, PropertyChangedEventArgs e)
        {
            var i = id;
            PaymentService.FinishEvent -= PaiementViewHandler;
            PaiementView.ScreenMessage = PaymentService.Message;
            PaiementView.IsSuccesfull = PaymentService.State;
            if (!PaiementView.IsSuccesfull)
            {
                var total = 0f;
                try
                {
                    FinalizeOrder();
                    total = Total;
                }
                catch (Exception) { }
                if (PaymentService.Enregistrement(total))
                {
                    PaiementView.ScreenMessage = "Votre commande sera livrée dans 30 minutes\n Au revoir et à bientot";
                }
            }
            new Thread(() =>
            {
                Thread.Sleep(5000);
                EventAggregator.GetEvent<EndCommandEvent>().Publish(this);
            }).Start();
        }

        private bool _finished;
        private void FinalizeOrder()
        {
            if (_finished) return;
            _finished = true;
            var client = DataProvider.InsertIfNotExists(CustomerViewModel.Customer);
            var addr = DataProvider.InsertIfNotExists(CustomerViewModel.Address);
            var command = new Commande
                              {
                                  Num = 0,
                                  IDCLien = client.IDCLien,
                                  IDBorn = Borne.IDBorn,
                                  IdaDr = addr.IdaDr,
                                  Statut = "ATTENTE",
                                  Source = "BORNE",
                                  Total = Total,
                                  Tva = Tva,
                                  Date = DateTime.Now
                              };

            OrderedProdects.ForEach(prod=>
                                        {
                                            if (prod.Produit == null) return;
                                            if(prod.IsMenu)
                                            {
                                                var menu = new Menu{IDProd = prod.Produit.IDProd, Quantite = prod.Quantite};
                                                var m = (MenuViewModel)prod;
                                                m.MenuProducts.ForEach(item =>
                                                                           {
                                                                               if (item.Produit == null) return;
                                                                               var existingProd = menu.ProduitsMenu.FirstOrDefault(i => i.IDProd == item.Produit.IDProd);
                                                                               if (existingProd == null)
                                                                                    menu.ProduitsMenu.Add(new ProduitsMenu{IDProd = item.Produit.IDProd,Quantite = item.Quantite});
                                                                               else
                                                                                   existingProd.Quantite += item.Quantite;
                                                                           });
                                                command.Menus.Add(menu);
                                            }
                                            else
                                            {
                                              var existingProd = command.ProduitsCommande.FirstOrDefault(item => item.IDProd == prod.Produit.IDProd);
                                              if (existingProd == null)
                                                   command.ProduitsCommande.Add(new ProduitsCommande { IDProd = prod.Produit.IDProd, Quantite = prod.Quantite });
                                              else
                                                   existingProd.Quantite += prod.Quantite;
                                            }
                                        });
            new TicketPrinter().Print(DataProvider.AddCommande(command));
        }

        private const string Suivre = "Suivez les instructions de paiement sur le petit écran vert";

        private void CustommerViewHandler(object sender, PropertyChangedEventArgs e)
        {
            
            switch (e.PropertyName)
            {
                case "Validate":
                    PaiementView.TotalPrice = Total;
                    SelectedView = PaiementView;
                    PaiementView.ScreenMessage = Suivre;
                    new Thread(()=> PaymentService.DemandeSolvabilite(Total)).Start();
                    break;
                case "Cancel" :
                    CancelCommand.Execute(null);
                    break;
                case "Back":
                    SelectedView = this;
                    break;
            }
        }

        private ICommandView _selectedView;
        public ICommandView SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                IsActive = value == this;
                CustomerViewModel.IsVisible = false;
                PaiementView.IsVisible = false;
                IsVisible = false;
                OrderedProdects.ForEach(item=>item.IsVisible=false);
                value.IsVisible = true;
                NotifyChanged("SelectedView");
            }
        }

        public bool IsActive
        {
            get {
                return _isActive;
            }
            set {
                _isActive = value;
                NotifyChanged("IsActive");
            }
        }

        public Borne Borne { get; set; }

        public PaiementViewModel PaiementView { get; set; }
        public MenuViewModel MenuViewModel { get; set; }
        public ObservableCollection<ProductViewModel> OrderedProdects { get; set; }
        public CustomerViewModel CustomerViewModel { get; set; }

        public string ScreenName { get; set; }

        private IView _view;
        public IView View
        {
            get { return _view; }
            set
            {
                _view = value;
                _view.ViewModel = this;
            }
        }

        public IEventAggregator EventAggregator { get; set; }
        private IDataProvider DataProvider { get; set; }
        public IPaymentService PaymentService { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        public ISettingsService SettingsService { get; set; }
        public ObservableCollection<Famille> Families { get; set; }
        public ObservableCollection<Produit> Products { get; set; }

        private Famille _selectedFamily;
        public Famille SelectedFamily
        {
            get { return _selectedFamily; }
            set
            {
                _selectedFamily = value;
                NotifyChanged("SelectedFamily");
                if (_selectedFamily != null)
                    UpdateProductsList();
            }
        }

        private Produit _selectedProduct;
        public Produit SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyChanged("SelectedProduct");
                if (value != null)
                    AddProductToCommand(_selectedProduct);
                SelectedFamily = null;
                if (value == null) return;
                var timer = new Timer();
                timer.Elapsed += (sender, args) => { SelectedProduct = null;timer.Stop(); }; 
                timer.Interval = (100);             
                timer.Enabled = true;                       
                timer.Start();   
            }
        }

        private float _total;
        private bool _isActive;

        public float Total
        {
            get { return _total; }
            set
            {
                _total = value;
                NotifyChanged("Total");
            }
        }

        private float _tva;
        public float Tva
        {
            get { return _tva; }
            set
            {
                _tva = value;
                NotifyChanged("Tva");
            }
        }

        public ICommand ShowCustomerCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private ProductViewModel _selectedOrder;
        public ProductViewModel SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                if (_selectedOrder != null && _selectedOrder is MenuViewModel)
                {
                    ((MenuViewModel)_selectedOrder).EditCommand.Execute(null);
                }
            }
        }

        public string LeftScollImage { get; private set; }
        public string RightScollImage { get; private set; }

        public string Logo { get; private set; }


        private void AddProductToCommand(Produit produit)
        {
            if (produit == null) return;
            
            if(produit.IsMenu)
            {
                var menuVm = new MenuViewModel(produit, DataProvider.GetMenuComposition(produit)){Quantite = 1, LocalizationService = LocalizationService};
                menuVm.PropertyChanged += OnMenuViewAction;
                menuVm.PropertyChanged += UpdateTotal;
                menuVm.PropertyChanged += DeleteHandler;
                menuVm.PropertyChanged += EditHandler;
                SelectedView = menuVm;
            }
            else
            {
                var produitsCommande = new ProductViewModel(produit){Quantite = 1};
                produitsCommande.PropertyChanged += UpdateTotal;
                produitsCommande.PropertyChanged += DeleteHandler;
                OrderedProdects.Add(produitsCommande);
            }
            Total += produit.Prix;
            Tva += produit.Famille.Tva * produit.Prix / 100;
        }

        private void EditHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Edit") return;
            if (!(sender is MenuViewModel)) return;
            var menu = sender as MenuViewModel;
            SelectedView = menu;
        }

        private void DeleteHandler(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName != "Delete") return;
            var product = sender as ProductViewModel;
            product.PropertyChanged -= UpdateTotal;
            product.PropertyChanged -= DeleteHandler;
            OrderedProdects.Remove(product);

            if (product.IsMenu)
            {
                product.PropertyChanged -= OnMenuViewAction;
                if (SelectedView == product)
                    SelectedView = this;
            }
            UpdateTotal(sender, e);
        }

        private void UpdateTotal(object sender, PropertyChangedEventArgs e)
        {
            var total = 0.0f;
            var tva = 0.0f;
            OrderedProdects.ForEach(item =>
                                        {
                                            total += item.PrixTotal;
                                            tva += item.PrixTotal*item.Produit.Famille.Tva/100;
                                        });
            Total = total;
            Tva = tva;
        }

        private void OnMenuViewAction(object sender, PropertyChangedEventArgs e)
        {
            var menuVm = sender as MenuViewModel;
                switch (e.PropertyName)
                {
                    case "Validate":
                        SelectedView = this;
                        if (!OrderedProdects.Contains(menuVm))
                            OrderedProdects.Add(menuVm);
                        break;
                    case "Cancel":
                        SelectedView = this;
                        menuVm.PropertyChanged -= OnMenuViewAction;
                        break;
                }
        }

        private void UpdateProductsList()
       {
           Products.Clear();
            var row = 0;
            var col = 0;
           DataProvider.GetFamilyProducts(SelectedFamily).Where(p => p.IsActif && p.IsApp).ForEach(item =>
                                                                                                       {
                                                                                                           item.Column =col;
                                                                                                           item.Row =row;
                                                                                                           Products.Add(item);
                                                                                                           col = (col+1)%2;
                                                                                                           row = col==0 ? row+1 : row;
                                                                                                       });
       }
    }


    public class GridF : UniformGrid
    {
        protected override Size MeasureOverride(Size constraint)
        {
            var b = base.MeasureOverride(constraint);
            var t = this.ActualWidth;
            foreach (ListBoxItem child in this.Children)
            {
                var g = child.ActualWidth;
                var g2 = child.Width;
                child.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                //child.Width = child.ActualHeight;
            }
            return b;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var b = base.ArrangeOverride(arrangeSize);
            var t = this.ActualWidth;
            foreach (ListBoxItem child in this.Children)
            {
                var g = child.ActualWidth;
                var g2 = child.Width;
                //child.Width = child.ActualHeight;
                child.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            }
            return b;
        }
    }

}
