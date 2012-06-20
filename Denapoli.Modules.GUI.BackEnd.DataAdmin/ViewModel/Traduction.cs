using System.ComponentModel;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    public class Traduction : NotifyPropertyChanged, IEditableObject
    {
        private string _oldNom;
        private string _nom;
        public string Nom
        {
            get { return _nom; }
            set
            {
                _nom = value;
                NotifyChanged("Nom");
            }
        }

        private string _oldDescription;
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyChanged("Description");
            }
        }

        private string _langue;
        public string Langue
        {
            get { return _langue; }
            set
            {
                _langue = value;
                NotifyChanged("Langue");
            }
        }


        public Langage Langage { get; set; }
        
        public void BeginEdit()
        {
            _oldNom = Nom;
            _oldDescription = Description;
        }

        public void EndEdit()
        {
        }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Description = _oldDescription;
        }
    }
}