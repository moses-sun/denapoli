using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Behavior;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.MainScreen.View
{
    /// <summary>
    /// Interaction logic for WellcomeScreenView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.WellComeRegion)]
    public partial class WellcomeScreenView : IView
    {
        public WellcomeScreenView()
        {
            InitializeComponent();
        }

        public AbstractScreenViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
