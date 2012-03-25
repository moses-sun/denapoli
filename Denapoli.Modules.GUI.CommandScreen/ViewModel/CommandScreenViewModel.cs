using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.Prism.Events;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class CommandScreenViewModel : AbstractScreenViewModel, ICommandView
    {

        public CommandScreenViewModel(IEventAggregator eventAggregator, IDataProvider dataProvider, IPaymentService paymentService)
        {
            EventAggregator = eventAggregator;
            DataProvider = dataProvider;
            PaymentService = paymentService;
            ScreenName = "Commande";
            IsVisible = true;
            Families = new ObservableCollection<Famille>();
            dataProvider.GetAvailableFamilies().ForEach(item => Families.Add(item));
            Products = new ObservableCollection<Produit>();
            OrderedProdects = new ObservableCollection<ProductViewModel>();
            Borne = DataProvider.GetBorne(1);
            CustomerView = new CustomerView {Address = Borne.Adresse,IsVisible = false};
            CustomerView.PropertyChanged += CustommerViewHandler;
            PaymentService.PropertyChanged += PaiementViewHandler;
            PaiementView = new PaiementViewModel {IsVisible = false};
            ShowCustomerCommand = new ActionCommand(() => SelectedView = CustomerView);
            LeftScollImage = "scroll_left.png";
            Logo = "logo.jpg";
            SelectedView = this;
        }


        private void PaiementViewHandler(object sender, PropertyChangedEventArgs e)
        {
            PaiementView.ScreenMessage = PaymentService.Message;
            PaiementView.IsSuccesfull = PaymentService.State;
            if(PaiementView.IsSuccesfull)
            {
                PaymentService.PropertyChanged -= PaiementViewHandler;
                FinalizeOrder();
                PaiementView.ScreenMessage = "Votre commande sera livrée dans 30 minutes\n au revoir";
                EventAggregator.GetEvent<EndCommandEvent>().Publish(this);
            } 
        }

        private void FinalizeOrder()
        {
            var client = DataProvider.InsertIfNotExists(CustomerView.Customer);
            var command = new Commande
                              {
                                  IDCLien = client.IDCLien,
                                  IDBorn = Borne.IDBorn,
                                  IdaDr =  Borne.IdaDr,
                                  Statut = "ATTENTE",
                                  Total = Total,
                              };

            OrderedProdects.ForEach(prod=>
                                        {
                                            if(prod.IsMenu)
                                            {
                                                var menu = new Menu{IDProd = prod.Produit.IDProd};
                                                var m = (MenuViewModel)prod;
                                                m.MenuProducts.ForEach(item => menu.ProduitsMenu.Add(new ProduitsMenu{IDProd = item.Produit.IDProd}));
                                                command.Menus.Add(menu);
                                            }
                                            else
                                                command.ProduitsCommande.Add(new ProduitsCommande { IDProd = prod.Produit.IDProd });

                                            
                                        });
           // DataProvider.AddCommande(command);
        }

        private void CustommerViewHandler(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Validate":
                    PaiementView.TotalPrice = Total;
                    SelectedView = PaiementView;
                    PaymentService.Pay(Total);
                    break;
                case "Cancel" :
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
                CustomerView.IsVisible = false;
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
        public CustomerView CustomerView { get; set; }

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

        public ICommand ShowCustomerCommand { get; private set; }

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
                var menuVm = new MenuViewModel(produit, DataProvider.GetMenuComposition(produit)){Quantite = 1};
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
            OrderedProdects.ForEach(item => total += item.PrixTotal);
            Total = total;
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
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
       }
    }
}
