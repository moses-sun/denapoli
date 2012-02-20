using System.ComponentModel.Composition;
using System.Windows.Input;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.Prism.Events;

namespace Denapoli.Modules.GUI.MainScreen
{
    [Export]
    public class WellcomeScreenViewModel : NotifyPropertyChanged, IScreenViewModel
    {
        private IEventAggregator EventAggregator { get; set; }
        public bool IsProject { get; set; }


        [ImportingConstructor]
        public WellcomeScreenViewModel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            ScreenName = "WellCome";
            IsProject = false;
            OrderCommand = new ActionCommand(()=>EventAggregator.GetEvent<NewCommandEvent>().Publish(null));
        }

        public bool IsVisible { get; set; }

        public string ScreenName { get; set; }

        public ICommand OrderCommand { get; private set; }
    }
}