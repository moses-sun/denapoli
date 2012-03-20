using System.Windows.Input;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class CustomerView : NotifyPropertyChanged, ICommandView
    {
        public CustomerView()
        {
            Customer = new Client{Nom="Nom",Prenom = "Prenom", Email = "Email", Tel = "Tel"};
            Address = new Adresse();
            PaiementCommand = new ActionCommand(()=>NotifyChanged("Validate"));
            CancelCommand = new ActionCommand(() => NotifyChanged("Cancel"));
        }
        public Client Customer { get; set; }
        public Adresse Address { get; set; }

        public ICommand PaiementCommand { get; set; }

        public ICommand CancelCommand { get; private set; }
    }
}