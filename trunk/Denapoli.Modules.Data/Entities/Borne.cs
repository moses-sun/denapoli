using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using DbLinq.Data.Linq;

namespace Denapoli.Modules.Data.Entities
{
    [Table(Name="borne")]
    public sealed class Borne : INotifyPropertyChanging, INotifyPropertyChanged
    {
		
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private int? _idaDr;
        private int _idbOrn;
        private DateTime _hfERmEJoUr;
        private DateTime _hfERmESoIr;
        private DateTime _hoUvErtJoUr;
        private DateTime _hoUvErtSoIr;
        private sbyte _isaCtIf;
        private sbyte _isoUvErt;
        private string _message;
        private string _messageInActIf;
        private EntitySet<Commande> _commandes;
        private EntityRef<Adresse> _adRessE;
		
        public Borne()
        {
            _commandes = new EntitySet<Commande>(CommandEAttach, CommandEDetach);
        }
		
        [Column(Storage="_idaDr", Name="ID_ADR", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int? IdaDr
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
		
        [Column(Storage="_idbOrn", Name="ID_BORN", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int IDBorn
        {
            get{ return _idbOrn; }
            set
            {
                if ((_idbOrn == value)) return;
                SendPropertyChanging();
                _idbOrn = value;
                SendPropertyChanged("IDBorn");
            }
        }


        [Column(Storage = "_hfERmEJoUr", Name = "H_FERME_JOUR", DbType = "time", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public DateTime HeureFermetureJour
        {
            get
            {
                return _hfERmEJoUr;
            }
            set
            {
                if ((_hfERmEJoUr == value)) return;
                SendPropertyChanging();
                _hfERmEJoUr = value;
                SendPropertyChanged("HeureFermetureJour");
            }
        }

        [Column(Storage = "_hfERmESoIr", Name = "H_FERME_SOIR", DbType = "time", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public DateTime HeureFermetureSoir
        {
            get
            {
                return _hfERmESoIr;
            }
            set
            {
                if ((_hfERmESoIr != value))
                {
                    SendPropertyChanging();
                    _hfERmESoIr = value;
                    SendPropertyChanged("HeureFermetureSoir");
                }
            }
        }

        [Column(Storage = "_hoUvErtJoUr", Name = "H_OUVERT_JOUR", DbType = "time", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public DateTime HeureOuvertureJour
        {
            get
            {
                return _hoUvErtJoUr;
            }
            set
            {
                if ((_hoUvErtJoUr == value)) return;
                SendPropertyChanging();
                _hoUvErtJoUr = value;
                SendPropertyChanged("HeureOuvertureJour");
            }
        }

        [Column(Storage = "_hoUvErtSoIr", Name = "H_OUVERT_SOIR", DbType = "time", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public DateTime HeureOuvertureSoir
        {
            get
            {
                return _hoUvErtSoIr;
            }
            set
            {
                if ((_hoUvErtSoIr == value)) return;
                SendPropertyChanging();
                _hoUvErtSoIr = value;
                SendPropertyChanged("HeureOuvertureSoir");
            }
        }

        [Column(Storage = "_isaCtIf", Name = "IS_ACTIF", DbType = "tinyint(1)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public sbyte IsaCtIf
        {
            get
            {
                return _isaCtIf;
            }
            set
            {
                if ((_isaCtIf == value)) return;
                SendPropertyChanging();
                _isaCtIf = value;
                SendPropertyChanged("IsaCtIf");
                SendPropertyChanged("IsActif");
            }
        }

        public bool IsActif
        {
            get { return IsaCtIf==1 ; }
            set { IsaCtIf = (sbyte) (value ? 1 : 0); }
        }

        [Column(Storage = "_isoUvErt", Name = "IS_OUVERT", DbType = "tinyint(1)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode()]
        public sbyte IsoUVert
        {
            get
            {
                return _isoUvErt;
            }
            set
            {
                if ((_isoUvErt == value)) return;
                SendPropertyChanging();
                _isoUvErt = value;
                SendPropertyChanged("IsoUVert");
                SendPropertyChanged("IsOuvert");
            }
        }

        public bool IsOuvert
        {
            get { return IsoUVert == 1; }
            set { IsoUVert = (sbyte)(value ? 1 : 0); }
        }

        [Column(Storage = "_message", Name = "MESSAGE", DbType = "varchar(500)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (((_message == value)
                            == false))
                {
                    SendPropertyChanging();
                    _message = value;
                    SendPropertyChanged("Message");
                }
            }
        }

        [Column(Storage = "_messageInActIf", Name = "MESSAGE_INACTIF", DbType = "varchar(500)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public string MessageInActIf
        {
            get
            {
                return _messageInActIf;
            }
            set
            {
                if (((_messageInActIf == value)
                            == false))
                {
                    SendPropertyChanging();
                    _messageInActIf = value;
                    SendPropertyChanged("MessageInActIf");
                }
            }
        }
		
        #region Children
        [Association(Storage="_commandE", OtherKey="IDBorn", ThisKey="IDBorn", Name="borne_qui_commande")]
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
		
        #region Parents
        [Association(Storage="_adRessE", OtherKey="IdaDr", ThisKey="IdaDr", Name="adresse_de_borne", IsForeignKey=true)]
        [DebuggerNonUserCode]
        public Adresse Adresse
        {
            get
            {
                return _adRessE.Entity;
            }
            set
            {
                if ((_adRessE.Entity == value)) return;
                if ((_adRessE.Entity != null))
                {
                    var previousAdresse = _adRessE.Entity;
                    _adRessE.Entity = null;
                    previousAdresse.Borne.Remove(this);
                }
                _adRessE.Entity = value;
                if ((value != null))
                {
                    value.Borne.Add(this);
                    _idaDr = value.IdaDr;
                }
                else
                {
                    _idaDr = default(int);
                }
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
        private void CommandEAttach(Commande entity)
        {
            SendPropertyChanging();
            entity.Borne = this;
        }
		
        private void CommandEDetach(Commande entity)
        {
            SendPropertyChanging();
            entity.Borne = null;
        }
        #endregion
    }
}