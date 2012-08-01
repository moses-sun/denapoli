using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Behavior;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.MainScreen.View
{
    [Export]
    [ViewExport(RegionName = RegionNames.DisabledRegion)]
    public partial class DisabledScreenView : IView
    {
        public DisabledScreenView()
        {
            InitializeComponent();
        }

        public AbstractScreenViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }

}
