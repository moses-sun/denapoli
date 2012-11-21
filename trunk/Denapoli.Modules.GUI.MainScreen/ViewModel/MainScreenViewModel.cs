using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Timers;
using System.Windows;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.DataProvider;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.GUI.MainScreen.View;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.Prism.Events;
using Timer = System.Timers.Timer;

namespace Denapoli.Modules.GUI.MainScreen.ViewModel
{
    [Export]
    public class MainScreenViewModel : NotifyPropertyChanged
    {
        private IEventAggregator EventAggregator { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        public IDataProvider DataProvider { get; set; }
        public ISettingsService SettingsService { get; set; }
        public IPaymentService PaymentService { get; set; }
        private WellcomeAbstractScreenViewModel _wellcomeAbstractScreenViewModel;

        [ImportingConstructor]
        public MainScreenViewModel(IEventAggregator eventAggregator, ILocalizationService localizationService,IDataProvider dataProvider, ISettingsService settingsService, IPaymentService paymentService)
        {
            EventAggregator = eventAggregator;
            LocalizationService = localizationService;
            DataProvider = dataProvider;
            SettingsService = settingsService;
            PaymentService = paymentService;
            EventAggregator.GetEvent<ScreenChangedEvent>().Subscribe(ScreenChangerEventHandler);
            EventAggregator.GetEvent<EndCommandEvent>().Subscribe(EndCommandEventHandler);
            CommandVisibility = Visibility.Collapsed;
            DisabledVisibility = Visibility.Collapsed;
            WellcomeVisibility = Visibility.Visible;

            var timer = new Timer { Interval = 6000 };
            timer.Elapsed += CheckBorneStateHandler;
            timer.Enabled = true;
            timer.Start();
        }

        private void CheckBorneStateHandler(object sender, ElapsedEventArgs e)
        {
            var borne = DataProvider.GetBorne(SettingsService.GetBorneId());
            if (borne == null) return;
            DisabledSwcreenViewModel.HorairesOuverture = String.Format("Ouvert de {0} à {1} et de {2} à {3}",
                borne.HeureOuvertureJour.ToString("HH:mm", null),
                borne.HeureFermetureJour.ToString("HH:mm", null),
                borne.HeureOuvertureSoir.ToString("HH:mm", null),
                borne.HeureFermetureSoir.ToString("HH:mm", null));

            if (borne.IsActif)
            {

                if (borne.IsOuvert && IsHorairesOuverture(borne))
                {
                    if (SelectedScreen == DisabledSwcreenViewModel)
                        EventAggregator.GetEvent<EndCommandEvent>().Publish(DisabledSwcreenViewModel);
                }
                else if (borne.IsOuvert)
                {
                    DisabledSwcreenViewModel.Message = "Di Napoli Pizza";
                    SelectedScreen = DisabledSwcreenViewModel;
                    new Thread(() => PaymentService.LancerTelecollecte()).Start();
                }
                else if (!borne.IsOuvert)
                {
                    DisabledSwcreenViewModel.Message = borne.Message;
                    DisabledSwcreenViewModel.HorairesOuverture = "Di Napoli Pizza";
                    SelectedScreen = DisabledSwcreenViewModel;
                }
            }
            else
            {
                DisabledSwcreenViewModel.Message = borne.MessageInActIf;
                DisabledSwcreenViewModel.HorairesOuverture = "";
                SelectedScreen = DisabledSwcreenViewModel;
            }
        }

        private bool IsHorairesOuverture(Borne borne)
        {
            var a = new DateTime(2000, 1, 1, borne.HeureOuvertureJour.Hour, borne.HeureOuvertureJour.Minute, 0);
            var b = new DateTime(2000, 1, 1, borne.HeureFermetureJour.Hour, borne.HeureFermetureJour.Minute, 0);
            var c = new DateTime(2000, 1, 1, borne.HeureOuvertureSoir.Hour, borne.HeureOuvertureSoir.Minute, 0);
            var d = new DateTime(2000, 1, 1, borne.HeureFermetureSoir.Hour, borne.HeureFermetureSoir.Minute, 0);
            var now = new DateTime(2000, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            if (now >= a && now < b) return true;
            if (now >= c && now < d) return true;
            return false;
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


        [Import]
        private DisabledSwcreenViewModel DisabledSwcreenViewModel { get; set; }


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

        private Visibility _disabledVisibility;
        public Visibility DisabledVisibility
        {
            get { return _disabledVisibility; }
            set
            {
                _disabledVisibility = value;
                NotifyChanged("DisabledVisibility");
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

        private DisabledScreenView _disabledScreenView;
        [Import]
        public DisabledScreenView DisabledScreenView
        {
            get { return _disabledScreenView; }
            set
            {
                _disabledScreenView = value;
                DisabledSwcreenViewModel.View = value;
            }
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
                    DisabledVisibility = Visibility.Collapsed;
                }
              else if (_selectedScreen == DisabledSwcreenViewModel)
              {
                  WellcomeVisibility = Visibility.Collapsed;
                  CommandVisibility = Visibility.Collapsed;
                  DisabledVisibility = Visibility.Visible;
              }
              else
              {
                  WellcomeVisibility = Visibility.Collapsed;
                  CommandVisibility = Visibility.Visible;
                  DisabledVisibility = Visibility.Collapsed;
              }
            }
        }
    }
}