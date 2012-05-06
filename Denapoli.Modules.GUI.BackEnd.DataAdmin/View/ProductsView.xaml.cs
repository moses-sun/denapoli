using System.ComponentModel.Composition;
using System.Windows.Controls;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.View
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.AdminProductsRegion)]
    public partial class ProductsView
    {
        public ProductsView()
        {
            InitializeComponent();
        }
    }
}
