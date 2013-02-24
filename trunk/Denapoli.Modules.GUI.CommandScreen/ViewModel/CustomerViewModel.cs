using System.Windows;
using System.Windows.Input;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class CustomerViewModel : NotifyPropertyChanged, ICommandView
    {
        public CustomerViewModel()
        {
            Customer = new Client{Nom="",Prenom = "", Email = "", Tel = ""};
            Address = new Adresse();
            PaiementCommand = new ActionCommand(()=>
                                                    {
                                                        if (string.IsNullOrEmpty(Customer.Nom) 
                                                            || string.IsNullOrEmpty(Customer.Prenom) 
                                                            || string.IsNullOrEmpty(Address.NumCHamBRe) 
                                                            || string.IsNullOrEmpty(Customer.Tel))
                                                            return;
                                                        NotifyChanged("Validate");
                                                    });
            CancelCommand = new ActionCommand(() => NotifyChanged("Cancel"));
            BackCommand = new ActionCommand(() => NotifyChanged("Back"));
        }

        public ILocalizationService LocalizationService { get; set; }
  
        public Client Customer { get; set; }
        public Adresse Address { get; set; }

        public ICommand PaiementCommand { get; set; }

        public ICommand CancelCommand { get; private set; }
        public ICommand BackCommand { get; private set; }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyChanged("Visibility");
            }
        }
        public Visibility Visibility
        {
            get { return IsVisible ? Visibility.Visible :  Visibility.Collapsed; }
            
        }
    }
}