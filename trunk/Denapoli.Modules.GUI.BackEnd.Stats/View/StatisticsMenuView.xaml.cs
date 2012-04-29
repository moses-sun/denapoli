using System.ComponentModel.Composition;
using Denapoli.Modules.GUI.BackEnd.Stats.ViewModel;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.Stats.View
{
    /// <summary>
    /// Interaction logic for StatsMenuView.xaml
    /// </summary>
    
    [Export]
    [ViewExport(RegionName = RegionNames.AdminMenuRegion)]
    public partial class StatisticsMenuView
    {
        public StatisticsMenuView()
        {
            InitializeComponent();
        }

        [Import]
        public StatsViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
