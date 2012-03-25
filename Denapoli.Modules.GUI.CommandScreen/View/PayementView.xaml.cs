using System.ComponentModel.Composition;
using System.Windows.Controls;
using Denapoli.Modules.Infrastructure.Behavior;

namespace Denapoli.Modules.GUI.CommandScreen.View
{
    /// <summary>
    /// Interaction logic for PayementView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.PaiementRegion)]
    public partial class PayementView
    {
        public PayementView()
        {
            InitializeComponent();
        }
    }
}
