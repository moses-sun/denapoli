using System.ComponentModel.Composition;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.MainScreen.ViewModel
{
    [Export]
    public class DisabledSwcreenViewModel : AbstractScreenViewModel
    {
        public ILocalizationService LocalizationService { get; set; }

        [ImportingConstructor]
        public DisabledSwcreenViewModel(ILocalizationService localizationService)
        {
            LocalizationService = localizationService;
            IsVisible = false;
            ScreenName = LocalizationService.Localize("Borne fermée");
            Message = LocalizationService.Localize("Borne fermée");
            HorairesOuverture = "00:00";
        }


        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyChanged("Message");
            }
        }

        private string _horairesOuverture;
        public string HorairesOuverture
        {
            get { return _horairesOuverture; }
            set
            {
                _horairesOuverture = value;
                NotifyChanged("HorairesOuverture");
            }
        }

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
    }
}