using System.ComponentModel;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    public class BorneVm: NotifyPropertyChanged, IEditableObject
    {
        public Borne Borne { get; set; }
        public static IDataProvider DataProvider { get; set; }

        public BorneVm()
        {
            Borne = new Borne();
            BorneAdresse = new Adresse();
            ReSetProperties();
        }

        public BorneVm(Borne b, IDataProvider dataProvider)
        {
            Borne = b;
            BorneAdresse = b.Adresse;
            DataProvider = dataProvider;
            ReSetProperties();
        }

        private void ReSetProperties()
        {
            Num = BorneAdresse.Num;
            Voie = BorneAdresse.Voie;
            Complement = BorneAdresse.Complement;
            CP = BorneAdresse.CP;
            Ville = BorneAdresse.Ville;
        }

        private Adresse _borneAdresse;
        public Adresse BorneAdresse
        {
            get { return _borneAdresse; }
            set
            {
                _borneAdresse = value;
                NotifyChanged("BorneAdresse");
            }
        }


        private string _oldVille;
        private string _ville;
        public string Ville
        {
            get {
                return _ville;
            }
            set {
                _ville = value;
                NotifyChanged("Ville");
            }
        }

        private string _oldCp;
        private string _cp;
        public string CP
        {
            get {
                return _cp;
            }
            set {
                _cp = value;
                NotifyChanged("CP");
            }
        }

        private string _oldComplement;
        private string _complement;
        public string Complement
        {
            get {
                return _complement;
            }
            set {
                _complement = value;
                NotifyChanged("Complement");
            }
        }

        private string _oldVoie;
        private string _voie;
        public string Voie
        {
            get { return _voie; }
            set
            {
                _voie = value;
                NotifyChanged("Voie");
            }
        }

        private int? _oldNum;
        private int? _num;
        public int? Num
        {
            get { return _num; }
            set
            {
                _num = value;
                NotifyChanged("Num");
            }
        }


        public void BeginEdit()
        {
            _oldNum = Num;
            _oldVoie = Voie;
            _oldComplement = Complement;
            _oldCp = CP;
            _oldVille = Ville;
        }

        private void UpdateProduit()
        {
            BorneAdresse.Num = Num;
            BorneAdresse.Voie = Voie;
            BorneAdresse.Complement = Complement;
            BorneAdresse.Ville = Ville;
            BorneAdresse.CP = CP;
        }

        public void EndEdit()
        {
            UpdateProduit();
            BorneAdresse = DataProvider.InsertIfNotExists(BorneAdresse);
            Borne.IdaDr = BorneAdresse.IdaDr;
            DataProvider.InsertIfNotExists(Borne);
        }

        public void CancelEdit()
        {
           Num          = _oldNum ;
           Voie         = _oldVoie ;
           Complement   = _oldComplement ;
           CP           = _oldCp ;
           Ville        = _oldVille ;
        }
    }
}