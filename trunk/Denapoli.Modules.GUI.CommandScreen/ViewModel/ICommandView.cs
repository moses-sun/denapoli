using System.Windows;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public interface ICommandView
    {
        bool IsVisible { get; set; }
        Visibility Visibility { get;  } 
    }
}