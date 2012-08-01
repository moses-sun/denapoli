using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.GUI.BackEnd.OrderProcessing.ViewModel;
using Denapoli.Modules.GUI.BackEnd.Stats.View;
using Denapoli.Modules.GUI.CommandScreen.ViewModel;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.Prism.Events;

namespace Denapoli.Modules.GUI.BackEnd.Stats.ViewModel
{
    [Export]
    public class StatsViewModel : AbstractScreenViewModel
    {
        private IDataProvider DataProvider { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public Window Window { get; set; }
        public ICommand ShowStatsCommand { get; set; }
        public StatisticsView View { get; set; }

        [ImportingConstructor]
        public StatsViewModel(IDataProvider dataProvider, IEventAggregator eventAggregator)
        {
            DataProvider = dataProvider;
            EventAggregator = eventAggregator;
            IsVisible = true;
            ShowStatsCommand = new ActionCommand(Show);
            Orders = new ObservableCollection<Commande>();
            Products = new ObservableCollection<ProductViewModel>();
            var timer = new Timer { Interval = 6000 };
            timer.Elapsed += (sender, args) =>
            {
                if (View == null) return;
                View.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                                            new System.Windows.Threading.
                                                DispatcherOperationCallback(delegate{UpdateCommandes();return null;}),
                                                null);
            };
            UpdateCommandes();
            timer.Start();
            
        }

        public ObservableCollection<Commande> Orders { get; set; }
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
        
        public ObservableCollection<ProductViewModel> Products { get; set; } 
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
            foreach (var menu in SelectedCommand.Menus)
            {
                var m = new MenuVM(menu.Produit) { Quantite = 1 };
                foreach (var comp in menu.ProduitsMenu)
                {
                    m.Composition.Add(new ProductViewModel(comp.Produit){Quantite = comp.Quantite});
                }
                Products.Add(m);
            }
        }

        private delegate void OpenCallback();
        private void Show()
        {
            if (Window.Dispatcher.CheckAccess() == false)
            {
                var uCallBack = new OpenCallback(Show);
                Window.Dispatcher.Invoke(uCallBack, new object());
            }
            else
            {
               Window.ShowDialog();
               UpdateCommandes();  
            }
        }


        private void UpdateCommandes()
        {
            var old = SelectedCommand == null ? -1 : SelectedCommand.Num;
            Orders.CollectionChanged -= OrdersOnCollectionChanged;
            Orders.Clear();
            DataProvider.GetMenuAllCommandes().ForEach(item => Orders.Add(item));
            SelectedCommand = Orders.FirstOrDefault(item => item.Num == old);
            SelectedCommand = SelectedCommand ?? Orders.FirstOrDefault();
            Orders.CollectionChanged += OrdersOnCollectionChanged;
        }

        private void OrdersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    DataProvider.Delete((Commande) e.OldItems[0]);
                    break;
            }
        }
    }
}
