using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.View
{
   
    [Export]
    [ViewExport(RegionName = RegionNames.AdminMenusRegion)]
    public partial class MenusView
    {
        public MenusView()
        {
            InitializeComponent();
        }
    }
}
