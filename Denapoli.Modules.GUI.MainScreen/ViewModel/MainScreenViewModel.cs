using System.ComponentModel.Composition;
using System.Windows;
using Denapoli.Modules.GUI.MainScreen.View;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.Prism.Events;

namespace Denapoli.Modules.GUI.MainScreen.ViewModel
{
    [Export]
    public class MainScreenViewModel : NotifyPropertyChanged
    {
        private IEventAggregator EventAggregator { get; set; }
        private WellcomeAbstractScreenViewModel _wellcomeAbstractScreenViewModel;

        [ImportingConstructor]
        public MainScreenViewModel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            EventAggregator.GetEvent<ScreenChangedEvent>().Subscribe(ScreenChangerEventHandler);
            EventAggregator.GetEvent<EndCommandEvent>().Subscribe(EndCommandEventHandler);
            CommandVisibility = Visibility.Collapsed;
            WellcomeVisibility = Visibility.Visible;
        }

        private void EndCommandEventHandler(AbstractScreenViewModel screen)
        {
            screen.IsVisible = false;
            SelectedScreen = WellcomeAbstractScreenViewModel;
        }

        private void ScreenChangerEventHandler(AbstractScreenViewModel screen)
        {
            SelectedScreen.IsVisible = false;
            SelectedScreen = screen;
        }

        [Import]
        private WellcomeAbstractScreenViewModel WellcomeAbstractScreenViewModel
        {
            get { return _wellcomeAbstractScreenViewModel; }
            set
            {
                _wellcomeAbstractScreenViewModel = value;
                SelectedScreen = value;
            }
        }


        private Visibility _commandVisibility;
        public Visibility CommandVisibility
        {
            get { return _commandVisibility; }
            set
            {
                _commandVisibility = value;
                NotifyChanged("CommandVisibility");
            }
        }

        private Visibility _wellcomeVisibility;
        public Visibility WellcomeVisibility
        {
            get { return _wellcomeVisibility; }
            set
            {
                _wellcomeVisibility = value;
                NotifyChanged("WellcomeVisibility");
            }
        }

        private WellcomeScreenView _view;
        [Import]
        public WellcomeScreenView View
        {
            set
            {
                _view = value;
                WellcomeAbstractScreenViewModel.View = value;
            }
            get { return _view; }
        }

        private AbstractScreenViewModel _selectedScreen;
        public AbstractScreenViewModel SelectedScreen
        {
            get { return _selectedScreen; }
            set
            {
                _selectedScreen = value;
                NotifyChanged("SelectedScreen");
                _selectedScreen.IsVisible = true;
              if (_selectedScreen == WellcomeAbstractScreenViewModel)
                {
                    WellcomeVisibility = Visibility.Visible;
                    CommandVisibility = Visibility.Collapsed;
                }
                else
                {
                    WellcomeVisibility = Visibility.Collapsed;
                    CommandVisibility = Visibility.Visible;
                }
            }
        }
    }
}