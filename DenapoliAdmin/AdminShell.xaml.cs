using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Telerik.Windows.Controls;

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
              Closing += ApplicationCommandsCloseExecuted;
              PreparerCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));
              PreteCommand.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));
              LivrerCommand.InputGestures.Add(new KeyGesture(Key.L, ModifierKeys.Control));
              ImprimerCommand.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
              EnterCommand.InputGestures.Add(new KeyGesture(Key.Enter));

        }

        public static readonly RoutedCommand PreparerCommand = new RoutedCommand();
        public static readonly RoutedCommand PreteCommand = new RoutedCommand();
        public static readonly RoutedCommand LivrerCommand = new RoutedCommand();
        public static readonly RoutedCommand ImprimerCommand = new RoutedCommand();
        public static readonly RoutedCommand EnterCommand = new RoutedCommand();

        [Import] private AdminShellPresenter _presenter;
        public AdminShellPresenter ViewModel
        {
            set
            {
                DataContext = value;
                _presenter = value;
            }

            get { return _presenter; }
        }


       

        private void ApplicationCommandsCloseExecuted(object sender, EventArgs executedRoutedEventArgs)
        {
        }

        private void PreparerCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.PreparerCommand.Execute(null);
        }

        private void PreteCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.PreteCommand.Execute(null);
        }

        private void LivrerCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.LivrerCommand.Execute(null);
        }

        private void ImprimerCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.ImprimerCommand.Execute(null);
        }

        private void EnterCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }

    
}
