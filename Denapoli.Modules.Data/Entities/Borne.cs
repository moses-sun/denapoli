using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using DbLinq.Data.Linq;

namespace Denapoli.Modules.Data.Entities
{
    [Table(Name="denapoli.borne")]
    public sealed class Borne : INotifyPropertyChanging, INotifyPropertyChanged
    {
		
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private int _idaDr;
        private int _idbOrn;
        private EntitySet<Commande> _commandes;
        private EntityRef<Adresse> _adRessE;
		
        public Borne()
        {
            _commandes = new EntitySet<Commande>(CommandEAttach, CommandEDetach);
        }
		
        [Column(Storage="_idaDr", Name="ID_ADR", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
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