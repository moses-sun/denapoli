using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.Prism.Events;

namespace Denapoli.Modules.GUI.MainScreen
{
    [Export]
    public class WellcomeScreenViewModel : NotifyPropertyChanged, IScreenViewModel
    {

        private IEventAggregator EventAggregator { get; set; }
        private ILocalizationService LocalizationService { get; set; }

        [ImportingConstructor]
        public WellcomeScreenViewModel(IEventAggregator eventAggregator, ILocalizationService localizationService)
        {
            EventAggregator = eventAggregator;
            LocalizationService = localizationService;
            ScreenName = "WellCome";
            OrderCommand = new ActionCommand(()=>EventAggregator.GetEvent<NewCommandEvent>().Publish(null));
        }

        public IEnumerable<Langage> AvailableLangages
        {
            get { return LocalizationService.AvailableLangages; }
        }

        private Langage _selectedLangage;
        public Langage SelectedLangage
        {
            get { return _selectedLangage; }
            set
            {
                _selectedLangage = value;
                LocalizationService.CurrentLangage = _selectedLangage;
                NotifyChanged("SelectedLangage");
            }
        }

        public bool IsVisible { get; set; }

        public string ScreenName { get; set; }

        public ICommand OrderCommand { get; private set; }
        
    }


    
}