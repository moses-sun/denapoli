using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.View
{
    [Export]
    [ViewExport(RegionName = RegionNames.AdminFamiliesRegion)]
    public partial class FamiliesView
    {
        public FamiliesView()
        {
            InitializeComponent();
        }
    }
}
