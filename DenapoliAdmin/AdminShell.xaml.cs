using System.ComponentModel.Composition;

namespace DenapoliAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [Export]
    public partial class AdminShell
    {
        public AdminShell()
        {
            InitializeComponent();
        }

        [Import]
        public AdminShellPresenter ViewModel
        {
            set
            {
                DataContext = value;
            }
        }
    }
}
