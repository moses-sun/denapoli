using System.ComponentModel.Composition;
using System.Windows.Controls;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.CommandScreen.View
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.ProductsRegion)]
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
        }
    }
}
