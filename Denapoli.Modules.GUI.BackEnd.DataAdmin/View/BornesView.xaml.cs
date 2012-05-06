using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.View
{
    [Export]
    [ViewExport(RegionName = RegionNames.AdminBornesRegion)]
    public partial class BornesView
    {
        public BornesView()
        {
            InitializeComponent();
        }
    }
}
