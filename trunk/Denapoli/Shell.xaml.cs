using System.ComponentModel.Composition;
using System.Windows.Input;

namespace Denapoli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [Export]
    public partial class Shell
    {
        public Shell()
        {
            //Mouse.OverrideCursor = Cursors.None; //TODO
            InitializeComponent();
        }

        private ShellPresenter _presenter;

        [Import]
        public ShellPresenter ViewModel
        {
            set
            {
                DataContext = value;
                _presenter = value;
            }
        }
    }
}
