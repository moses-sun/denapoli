using System.ComponentModel.Composition;
using System.Timers;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Behavior;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.Prism.Events;

namespace Denapoli
{
    [Export]
    public class ShellPresenter : NotifyPropertyChanged 
    {
        public ILocalizationService LocalizationService { get; set; }

        [ImportingConstructor]
        public ShellPresenter(ILocalizationService localizationService, IEventAggregator eventAggregator, ISettingsService settingsService)
        {
            LocalizationService = localizationService;
            LocalizationConverter.LocalizationService = LocalizationService;
            SettingsService = settingsService;
            ImageUriSourceConverter.SettingsService = SettingsService;
           
            LocalizationConverter.LocalizationService = LocalizationService;
            var timer = new Timer { Interval = 6000 };
            timer.Elapsed += (sender, args) =>
                                 {
                                     LocalizationService.Reset();
                                     ImageUriSourceConverter.Reset();
                                 };
            timer.Enabled = true;
            timer.Start();
        }

        public ISettingsService SettingsService { get; set; }

        private IEventAggregator EventsAggregator { get; set; }
        private bool _isBusy = false;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyChanged("IsBusy");
            }
        }
    }
}