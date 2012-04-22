using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Diagnostics;

namespace Denapoli.Modules.Data.Entities
{
    [Table(Name="denapoli.adresse")]
    public sealed class Adresse : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private string _numChAmBrE;
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private string _complement;
        private string _cp;
        private int _idaDr;
        private int? _num;
        private string _viLlE;
        private string _voiE;
        private EntitySet<Borne> _borne;
        private EntitySet<Commande> _commandes;
		
        public Adresse()
        {
            _borne = new EntitySet<Borne>(BorneAttach, BorneDetach);
            _commandes = new EntitySet<Commande>(CommandEAttach, CommandEDetach);
        }

        [Column(Storage = "_numChAmBrE", Name = "NUM_CHAMBRE", DbType = "varchar(15)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public string NumCHamBRe
        {
            get
            {
                return _numChAmBrE;
            }
            set
            {
                if (((_numChAmBrE == value)
                            == false))
                {
                    SendPropertyChanging();
                    _numChAmBrE = value;
                    SendPropertyChanged("NumCHamBRe");
                }
            }
        }
		
		
        [Column(Storage="_complement", Name="COMPLEMENT", DbType="varchar(200)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string Complement
        {
            get
            {
                return _complement;
            }
            set
            {
                if ((_complement == value)) return;
                SendPropertyChanging();
                _complement = value;
                SendPropertyChanged("Complement");
            }
        }
		
        [Column(Storage="_cp", Name="CP", DbType="varchar(100)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string CP
        {
            get
            {
                return _cp;
            }
            set
            {
                if ((_cp == value)) return;
                SendPropertyChanging();
                _cp = value;
                SendPropertyChanged("CP");
            }
        }
		
        [Column(Storage="_idaDr", Name="ID_ADR", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int IdaDr
        {
            get
            {
                return _idaDr;
            }
            set
            {
                if ((_idaDr == value)) return;
                SendPropertyChanging();
                _idaDr = value;
                SendPropertyChanged("IdaDr");
            }
        }
		
        [Column(Storage="_num", Name="NUM", DbType="int(6)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public int? Num
        {
            get
            {
                return _num;
            }
            set
            {
                if ((_num == value)) return;
                SendPropertyChanging();
                _num = value;
                SendPropertyChanged("Num");
            }
        }
		
        [Column(Storage="_viLlE", Name="VILLE", DbType="varchar(100)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string Ville
        {
            get
            {
                return _viLlE;
            }
            set
            {
                if ((_viLlE == value)) return;
                SendPropertyChanging();
                _viLlE = value;
                SendPropertyChanged("Ville");
            }
        }
		
        [Column(Storage="_voiE", Name="VOIE", DbType="varchar(200)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string Voie
        {
            get
            {
                return _voiE;
            }
            set
            {
                if ((_voiE == value)) return;
                SendPropertyChanging();
                _voiE = value;
                SendPropertyChanged("Voie");
            }
        }
		
        #region Children
        [Association(Storage="_borne", OtherKey="IdaDr", ThisKey="IdaDr", Name="adresse_de_borne")]
        [DebuggerNonUserCode]
        public EntitySet<Borne> Borne
        {
            get
            {
                return _borne;
            }
            set
            {
                _borne = value;
            }
        }
		
        [Association(Storage="_commandE", OtherKey="IdaDr", ThisKey="IdaDr", Name="adresse_de_commande")]
        [DebuggerNonUserCode]
        public EntitySet<Commande> Commandes
        {
            get
            {
                return _commandes;
            }
            set
            {
                _commandes = value;
            }
        }
        #endregion
		
        public event PropertyChangingEventHandler PropertyChanging;
		
        public event PropertyChangedEventHandler PropertyChanged;

        private void SendPropertyChanging()
        {
            var h = PropertyChanging;
            if ((h != null))
            {
                h(this, EmptyChangingEventArgs);
            }
        }

        private void SendPropertyChanged(string propertyName)
        {
            var h = PropertyChanged;
            if ((h != null))
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }
		
        #region Attachment handlers
        private void BorneAttach(Borne entity)
        {
            SendPropertyChanging();
            entity.Adresse = this;
        }
		
        private void BorneDetach(Borne entity)
        {
            SendPropertyChanging();
            entity.Adresse = null;
        }
		
        private void CommandEAttach(Commande entity)
        {
            SendPropertyChanging();
            entity.Adresse = this;
        }
		
        private void CommandEDetach(Commande entity)
        {
            SendPropertyChanging();
            entity.Adresse = null;
        }
        #endregion
    }
}