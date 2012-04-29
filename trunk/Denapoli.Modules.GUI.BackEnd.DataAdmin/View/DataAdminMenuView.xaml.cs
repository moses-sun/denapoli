using System.ComponentModel.Composition;
using Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.View
{
    /// <summary>
    /// Interaction logic for AdataAdminMenuView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.AdminMenuRegion)]
    public partial class DataAdminMenuView
    {
        public DataAdminMenuView()
        {
            InitializeComponent();
        }

        [Import]
        public DataAdminViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
