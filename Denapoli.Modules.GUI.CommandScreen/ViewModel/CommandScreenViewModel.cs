using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
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
    public class CommandScreenViewModel : NotifyPropertyChanged, IScreenViewModel, ICommandView
    {

        public CommandScreenViewModel(IEventAggregator eventAggregator, IDataProvider dataProvider, IPaymentService paymentService)
        {
            EventAggregator = eventAggregator;
            DataProvider = dataProvider;
            PaymentService = paymentService;
            ScreenName = "Commande";
            IsVisible = true;
            Families = new ObservableCollection<Famille>();
            dataProvider.GetAvailableFamilies().ForEach(item =>
                                                            {
                                                                Families.Add(item);
                                                                Families.Add(item);
                                                                Families.Add(item);
                                                            });
            
            Products = new ObservableCollection<Produit>();
            OrderedProdects = new ObservableCollection<ProductViewModel>();
            SelectedView = this;
            CustommerView = new CustomerView();
            CustommerView.PropertyChanged += CustommerViewHandler;
            PaymentService.PropertyChanged += PaiementViewHandler;
            PaiementView = new PaiementViewModel();
            ShowCustomerCommand = new ActionCommand(() => SelectedView = CustommerView);
        }

        private void PaiementViewHandler(object sender, PropertyChangedEventArgs e)
        {
            PaiementView.ScreenMessage = PaymentService.Message;
            PaiementView.IsSuccesfull = PaymentService.State;
            if(PaiementView.IsSuccesfull)
            {
                PaiementView.ScreenMessage = "Votre commande sera livrée dans 30 minutes\n au revoir";
                var timer = new Timer(5000);
                timer.Elapsed += (o, args) =>
                                     {
                                         FinalizeOrder();
                                         EventAggregator.GetEvent<EndCommandEvent>().Publish(this);
                                         timer.Stop();
                                     };
                timer.Start();
            } 
        }

        private void FinalizeOrder()
        {
            PaymentService.PropertyChanged -= PaiementViewHandler;
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

        public PaiementViewModel PaiementView { get; set; }

        public ObservableCollection<ProductViewModel> OrderedProdects { get; set; }
        public CustomerView CustommerView { get; set; }

        public bool IsVisible { get; set; }
        public string ScreenName { get; set; }
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

        private double _total;
        private bool _isActive;

        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
                NotifyChanged("Total");
            }
        }

        public ICommand ShowCustomerCommand { get; private set; }

        public Action<object, RoutedEventArgs> ToLeft { get; private set; }


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
            var total = 0.0;
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
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
           DataProvider.GetFamilyProducts(SelectedFamily).ForEach(item=> Products.Add(item));
       }
    }
}
