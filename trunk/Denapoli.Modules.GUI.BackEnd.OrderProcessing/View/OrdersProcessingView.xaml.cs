using System.ComponentModel.Composition;
using Denapoli.Modules.GUI.BackEnd.OrderProcessing.ViewModel;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.OrderProcessing.View
{
    /// <summary>
    /// Interaction logic for OrdersProcessingView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.MainRegion)]
    public partial class OrdersProcessingView 
    {
        public OrdersProcessingView()
        {
            InitializeComponent();
        }

        [Import]
        public OrdersProcessingViewModel ViewModel
        {
            set
            {
                value.View = this;
                DataContext = value;
            }
        }

}
        
    }
