using System.ComponentModel.Composition;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Behavior;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.Prism.Events;

namespace DenapoliAdmin
{
    [Export]
    public class AdminShellPresenter : NotifyPropertyChanged
    {
        public ILocalizationService LocalizationService { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public ISettingsService SettingsService { get; set; }

        [ImportingConstructor]
        public AdminShellPresenter(ILocalizationService localizationService, IEventAggregator eventAggregator, ISettingsService  settingsService)
        {
            LocalizationService = localizationService;
            EventAggregator = eventAggregator;
            SettingsService = settingsService;
            ImageUriSourceConverter.SettingsService = SettingsService;
            LocalizationConverter.LocalizationService = LocalizationService;
        }

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