using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.CommandScreen.View
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.CustomerRegion)]
    public partial class CustomerView
    {
        public CustomerView()
        {
            InitializeComponent();
        }
    }
}
