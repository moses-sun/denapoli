using System.ComponentModel.Composition;
using System.Windows.Controls;
using Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.View
{
    /// <summary>
    /// Interaction logic for DataAdminvView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.MainRegion)]
    public partial class DataAdminView
    {
        public DataAdminView()
        {
            InitializeComponent();
        }


        [Import]
        public DataAdminViewModel ViewModel
        {
            set
            {
                DataContext = value;
                value.Window = this;
            }
        }

    }
}
