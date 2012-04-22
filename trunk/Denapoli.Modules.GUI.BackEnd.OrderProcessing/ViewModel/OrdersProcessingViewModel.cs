using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Timers;
using System.Windows;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.GUI.BackEnd.OrderProcessing.View;
using Denapoli.Modules.GUI.CommandScreen.ViewModel;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Denapoli.Modules.GUI.BackEnd.OrderProcessing.ViewModel
{
    [Export]
    public class OrdersProcessingViewModel : NotifyPropertyChanged
    {
        private IDataProvider DataProvider { get; set; }
        public OrdersProcessingView View { get; set; }

        [ImportingConstructor]
        public OrdersProcessingViewModel(IDataProvider dataProvider)
        {
            DataProvider = dataProvider;
            Orders = new ObservableCollection<Commande>();
           
            Products = new ObservableCollection<ProductViewModel>();
           var timer = new Timer {Interval = 6000};
            timer.Elapsed += (sender, args) => View.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                                           new System.Windows.Threading.DispatcherOperationCallback(delegate
                                                                                                                                        {
                                                                                                                                            Orders.ForEach(item => item.Chrono--);
                                                                                                                                            UpdateCommandes();
                                                                                                                                            return null;
                                                                                                                      }), null);
            UpdateCommandes();   
            timer.Start();
        }

        public ObservableCollection<ProductViewModel> Products { get; set; } 

        public ObservableCollection<Commande> Orders { get; set; }

        private void UpdateCommandes()
        {
            var old = SelectedCommand == null ? -1  : SelectedCommand.Num;
            Orders.Clear();
            DataProvider.GetMenuAllCommandes().ForEach(item =>
                                                           {
                                                               var diff = DateTime.Now - item.Date;
                                                               item.Chrono = diff!=null ? 45-diff.Value.Minutes : 0;
                                                               Orders.Add(item);
                                                           });
            SelectedCommand = Orders.FirstOrDefault(item => item.Num == old);
            SelectedCommand = SelectedCommand ?? Orders.FirstOrDefault();
        }

        private Commande _selectedCommand;
        public Commande SelectedCommand
        {
            get { return _selectedCommand; }
            set
            {
                _selectedCommand = value;
                NotifyChanged("SelectedCommand");
                Updateproducts();
            }
        }

        private void Updateproducts()
        {
            Products.Clear();
            if (SelectedCommand == null) return;
            foreach (var produit in SelectedCommand.ProduitsCommande)
            {
                if (produit.Produit.IsMenu)
                {
                    var menu = new MenuVM(produit.Produit){Quantite = produit.Quantite};
                    Products.Add(menu);
                }
                Products.Add(new ProductViewModel(produit.Produit) { Quantite = produit .Quantite});
            }
        }
    }

    public class MenuVM : ProductViewModel
    {
        public MenuVM(Produit prod ) : base(prod)
        {
           Composition = new ObservableCollection<ProductViewModel>();
           
        }

        public ObservableCollection<ProductViewModel> Composition { get; private set; } 
    }
}
