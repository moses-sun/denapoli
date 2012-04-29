using System.ComponentModel.Composition;
using Denapoli.Modules.GUI.BackEnd.Stats.ViewModel;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.Stats.View
{
    /// <summary>
    /// Interaction logic for StatsView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.MainRegion)]
    public partial class StatisticsView
    {
        public StatisticsView()
        {
            InitializeComponent();
        }

        [Import]
        public StatsViewModel ViewModel
        {
            set
            {
                DataContext = value;
                value.Window = this;
            }
        }
    }
}
