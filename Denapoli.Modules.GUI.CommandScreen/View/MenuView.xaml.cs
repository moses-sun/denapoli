using System.ComponentModel.Composition;
using System.Windows.Controls;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.CommandScreen.View
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.MenuRegion)]
    public partial class MenuView
    {
        public MenuView()
        {
            InitializeComponent();
        }
    }
}
