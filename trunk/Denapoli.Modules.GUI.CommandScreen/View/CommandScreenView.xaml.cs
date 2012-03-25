using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Behavior;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.CommandScreen.View
{
    /// <summary>
    /// Interaction logic for CommandScreenView.xaml
    /// </summary>
    [Export]
    [ViewExport(RegionName = RegionNames.CommandRegion)]
    public partial class CommandScreenView :IView 
    {
        public CommandScreenView()
        {
            InitializeComponent();
        }

        public AbstractScreenViewModel ViewModel
        {
            set
            {
                DataContext = value;
                ProductsView.DataContext = value;
                MenuView.DataContext = value;
                CustomerView.DataContext = value;
                PayementView.DataContext = value;
            }
        }

        [Import]
        public ProductsView ProductsView { get; set; }

        [Import]
        public MenuView MenuView { get; set; }

        [Import]
        public CustomerView CustomerView { get; set; }

        [Import]
        public PayementView PayementView { get; set; }
    }
}
