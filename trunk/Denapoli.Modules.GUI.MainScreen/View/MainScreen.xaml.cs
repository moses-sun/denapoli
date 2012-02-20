using System.ComponentModel.Composition;
using Denapoli.Modules.GUI.MainScreen.ViewModel;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.MainScreen.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.MainRegion)]
    public partial class MainScreen
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        [Import]
        public MainScreenViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
