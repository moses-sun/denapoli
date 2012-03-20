using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.Infrastructure.Services
{
    public class Dico : NotifyPropertyChanged
    {

        public void Notify()
        {
            foreach (var prop in GetType().GetProperties())
            {
                NotifyChanged(prop.Name);
            }
           
        }

        public string TouchezLecranPourCommander { get; set; }
    }
}