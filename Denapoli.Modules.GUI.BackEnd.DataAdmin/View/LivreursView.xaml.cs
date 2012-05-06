using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.View
{
    [Export]
    [ViewExport(RegionName = RegionNames.AdminLivreursRegion)]
    public partial class LivreursView
    {
        public LivreursView()
        {
            InitializeComponent();
        }
    }
}
