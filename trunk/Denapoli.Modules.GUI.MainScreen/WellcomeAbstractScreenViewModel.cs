using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Timers;
using System.Windows.Input;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Behavior;
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
        public IDataProvider DataProvider { get; set; }
        public ILocalizationService LocalizationService { get; set; }

        [ImportingConstructor]
        public WellcomeAbstractScreenViewModel(IEventAggregator eventAggregator,IDataProvider dataProvider,  ILocalizationService localizationService)
        {
            EventAggregator = eventAggregator;
            DataProvider = dataProvider;
            IsVisible = false;
            LocalizationService = localizationService;
            ScreenName = "WellCome";
            OrderCommand = new ActionCommand(()=>EventAggregator.GetEvent<NewCommandEvent>().Publish(null));

            var timer = new Timer { Interval = 6000 };
            timer.Elapsed += (sender, args) => NotifyChanged("AvailableLangages");
            timer.Enabled = true;
            timer.Start();
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