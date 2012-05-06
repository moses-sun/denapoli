using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.View
{
    [Export]
    [ViewExport(RegionName = RegionNames.AdminI18nRegion)]
    public partial class I18nView
    {
        public I18nView()
        {
            InitializeComponent();
        }
    }
}
