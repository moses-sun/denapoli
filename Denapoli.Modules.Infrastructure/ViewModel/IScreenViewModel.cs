using System.ComponentModel;

namespace Denapoli.Modules.Infrastructure.ViewModel
{
    public interface IScreenViewModel : INotifyPropertyChanged
    {
        bool IsVisible { get; set; }
        string ScreenName { get; set; }
    }
}