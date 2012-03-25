using System.Windows;

namespace Denapoli.Modules.Infrastructure.ViewModel
{
    public abstract class AbstractScreenViewModel : NotifyPropertyChanged
    {
        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                Visibility = _isVisible ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _visibility;
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                NotifyChanged("Visibility");
            }
        }
        public string ScreenName { get; set; }
        public IView View { get; set; }
    }
}