using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Timers;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.GUI.BackEnd.OrderProcessing.View;

namespace Denapoli.Modules.GUI.BackEnd.OrderProcessing.ViewModel
{
    [Export]
    public class OrdersProcessingViewModel
    {
        private IDataProvider DataProvider { get; set; }
        public OrdersProcessingView View { get; set; }

        [ImportingConstructor]
        public OrdersProcessingViewModel(IDataProvider dataProvider)
        {
            DataProvider = dataProvider;
            Orders = new ObservableCollection<Commande>();
           var timer = new Timer {Interval = 2000};
            timer.Elapsed += (sender, args) => View.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                                           new System.Windows.Threading.DispatcherOperationCallback(delegate
                                                                                                                                        {
                                                                                                                                            UpdateCommandes();
                                                                                                                                            return null;
                                                                                                                                        }), null);
           timer.Start();
        }

        public ObservableCollection<Commande> Orders { get; set; }

        public void UpdateCommandes()
        {
            Orders.Clear();
            DataProvider.GetMenuAllCommandes().ForEach(item => Orders.Add(item));
        }

       
    }
}
