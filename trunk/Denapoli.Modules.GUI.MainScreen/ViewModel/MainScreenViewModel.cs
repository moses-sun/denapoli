using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.Prism.Events;

namespace Denapoli.Modules.GUI.MainScreen.ViewModel
{
    [Export]
    public class MainScreenViewModel : NotifyPropertyChanged
    {
        private IEventAggregator EventAggregator { get; set; }
        private WellcomeScreenViewModel _wellcomeScreenViewModel;

        [ImportingConstructor]
        public MainScreenViewModel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            Screens = new ObservableCollection<IScreenViewModel>();
            EventAggregator.GetEvent<ScreenChangedEvent>().Subscribe(ScreenChangerEventHandler);
            EventAggregator.GetEvent<EndCommandEvent>().Subscribe(EndCommandEventHandler);
        }

        private void EndCommandEventHandler(IScreenViewModel screen)
        {
            if (Screens.Contains(screen))
            {
                Screens.Remove(screen);
                screen.PropertyChanged -= ScreenSelected;
            }
            SelectedScreen = WellcomeScreenViewModel;
            screen.IsVisible = false;
            Screens.ForEach(s => s.IsVisible = false);
        }

        private void ScreenChangerEventHandler(IScreenViewModel screen)
        {
            if (!Screens.Contains(screen))
            {
                Screens.Add(screen);
                screen.PropertyChanged += ScreenSelected;
            }
            SelectedScreen = screen;
            screen.IsVisible = true;
            Screens.ForEach(s => s.IsVisible = false);
        }

        private void ScreenSelected(object sender, PropertyChangedEventArgs e)
        {
            var screen = sender as IScreenViewModel;
            if (e.PropertyName != "IsVisible") return;
            if (screen.IsVisible)
            {
                SelectedScreen = screen;
                Screens.ForEach(s => { if (s != screen) s.IsVisible = false; });
            }
            else SelectedScreen.IsVisible = true;
        }

        [Import]
        private WellcomeScreenViewModel WellcomeScreenViewModel
        {
            get { return _wellcomeScreenViewModel; }
            set
            {
                _wellcomeScreenViewModel = value;
                 SelectedScreen = WellcomeScreenViewModel;
            }
        }

        private IScreenViewModel _selectedScreen;
        public IScreenViewModel SelectedScreen
        {
            get { return _selectedScreen; }
            set
            {
                _selectedScreen = value;
                NotifyChanged("SelectedScreen");

                EventAggregator.GetEvent<ScreenChangedEvent>().Unsubscribe(ScreenChangerEventHandler);
                EventAggregator.GetEvent<ScreenChangedEvent>().Publish(value);
                EventAggregator.GetEvent<ScreenChangedEvent>().Subscribe(ScreenChangerEventHandler);

            }
        }

        public ObservableCollection<IScreenViewModel> Screens { get; private set; }
    }
}