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
    public class WellcomeAbstractScreenViewModel : AbstractScreenViewModel
    {

        private IEventAggregator EventAggregator { get; set; }
        private ILocalizationService LocalizationService { get; set; }

        [ImportingConstructor]
        public WellcomeAbstractScreenViewModel(IEventAggregator eventAggregator, ILocalizationService localizationService)
        {
            EventAggregator = eventAggregator;
            IsVisible = false;
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

        private IView _view;
        public IView View
        {
            get { return _view; }
            set
            {
                _view = value;
                _view.ViewModel = this;
            }
        }

        public ICommand OrderCommand { get; private set; }
        
    }


    
}