using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class PaiementViewModel : NotifyPropertyChanged, ICommandView
    {
        public PaiementViewModel()
        {
            ScreenMessage = "inserer votre carte";
        }

        private double _totalPrice;
        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                NotifyChanged("TotalPrice");
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyChanged("IsBusy");
            }
        }

        private bool _isSuccesfull;
        public bool IsSuccesfull
        {
            get { return _isSuccesfull; }
            set
            {
                _isSuccesfull = value;
                NotifyChanged("IsSuccesfull");
            }
        }

        private string _screenMessage;
        public string ScreenMessage
        {
            get { return _screenMessage; }
            set
            {
                _screenMessage = value;
                NotifyChanged("ScreenMessage");
            }
        }
    }
}