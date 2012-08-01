using System;
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

            HeureOuvertureJour = Borne.HeureOuvertureJour;
            HeureFermetureJour = Borne.HeureFermetureJour;
            HeureOuvertureSoir = Borne.HeureOuvertureSoir;
            HeureFermetureSoir = Borne.HeureFermetureSoir;
            IsActif = Borne.IsActif;
            IsOuvert = Borne.IsOuvert;
            Message = Borne.Message;
            MessageInactif = Borne.MessageInActIf;
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


        private DateTime _oldHeureOuvertureJour;
        private DateTime _heureOuvertureJour;
        public DateTime HeureOuvertureJour
        {
            get { return _heureOuvertureJour; }
            set
            {
                _heureOuvertureJour = value;
                NotifyChanged("HeureOuvertureJour");
            }
        }

        private DateTime _oldHeureOuvertureSoir;
        private DateTime _heureOuvertureSoir;
        public DateTime HeureOuvertureSoir
        {
            get { return _heureOuvertureSoir; }
            set
            {
                _heureOuvertureSoir = value;
                NotifyChanged("HeureOuvertureSoir");
            }
        }

        private DateTime _oldHeureFermetureJour;
        private DateTime _heureFermetureJour;
        public DateTime HeureFermetureJour
        {
            get { return _heureFermetureJour; }
            set
            {
                _heureFermetureJour = value;
                NotifyChanged("HeureFermetureJour");
            }
        }

        private DateTime _oldHeureFermetureSoir;
        private DateTime _heureFermetureSoir;
        public DateTime HeureFermetureSoir
        {
            get { return _heureFermetureSoir; }
            set
            {
                _heureFermetureSoir = value;
                NotifyChanged("HeureFermetureSoir");
            }
        }

        private bool _oldIsActif;
        private bool _isActif;
        public bool IsActif
        {
            get { return _isActif; }
            set
            {
                _isActif = value;
                NotifyChanged("IsActif");
            }
        }

        private bool _oldIsOuvert;
        private bool _isOuvert;
        public bool IsOuvert
        {
            get { return _isOuvert; }
            set
            {
                _isOuvert = value;
                NotifyChanged("IsOuvert");
            }
        }

        private string _oldMessage;
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

        private string _oldMessageInactif;
        private string _messageInactif;
        public string MessageInactif
        {
            get { return _messageInactif; }
            set
            {
                _messageInactif = value;
                NotifyChanged("MessageInactif");
            }
        }

        public void BeginEdit()
        {
            _oldNum = Num;
            _oldVoie = Voie;
            _oldComplement = Complement;
            _oldCp = CP;
            _oldVille = Ville;

            _oldHeureOuvertureJour = HeureOuvertureJour;
            _oldHeureFermetureJour = HeureFermetureJour;
            _oldHeureOuvertureSoir = HeureOuvertureSoir;
            _oldHeureFermetureSoir = HeureFermetureSoir;
            _oldIsActif = IsActif;
            _oldIsOuvert = IsOuvert;
            _oldMessage = Message;
            _oldMessageInactif = Borne.MessageInActIf;
        }

        private void UpdateProduit()
        {
            BorneAdresse.Num = Num;
            BorneAdresse.Voie = Voie;
            BorneAdresse.Complement = Complement;
            BorneAdresse.Ville = Ville;
            BorneAdresse.CP = CP;

            Borne.HeureOuvertureJour = HeureOuvertureJour;
            Borne.HeureFermetureJour = HeureFermetureJour;
            Borne.HeureOuvertureSoir = HeureOuvertureSoir;
            Borne.HeureFermetureSoir = HeureFermetureSoir;
            Borne.IsActif = IsActif;
            Borne.IsOuvert = IsOuvert;
            Borne.Message = Message;
            Borne.MessageInActIf = MessageInactif;
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

           HeureOuvertureJour = _oldHeureOuvertureJour;
           HeureFermetureJour = _oldHeureFermetureJour;
           HeureOuvertureSoir = _oldHeureOuvertureSoir;
           HeureFermetureSoir = _oldHeureFermetureSoir;
           IsActif = _oldIsActif;
           IsOuvert = _oldIsOuvert;
           Message = _oldMessage;
           MessageInactif = _oldMessageInactif;
        }
    }
}