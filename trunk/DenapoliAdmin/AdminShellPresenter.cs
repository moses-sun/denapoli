using System.ComponentModel.Composition;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Behavior;
using Denapoli.Modules.Infrastructure.Command;
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
            ImprimerCommand = new ActionCommand(() => EventAggregator.GetEvent<ImprimerEvent>().Publish(null));
            PreparerCommand = new ActionCommand(() => EventAggregator.GetEvent<PreparerEvent>().Publish(null));
            PreteCommand = new ActionCommand(() => EventAggregator.GetEvent<PreteEvent>().Publish(null));
            LivrerCommand = new ActionCommand(() => EventAggregator.GetEvent<LivrerEvent>().Publish(null));
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

        public ActionCommand ImprimerCommand { get; set; }
        public ActionCommand PreparerCommand { get; set; }
        public ActionCommand PreteCommand { get; set; }
        public ActionCommand LivrerCommand { get; set; }

    }
}