using System.Data.Linq.Mapping;
using System.Diagnostics;
using DbLinq.Data.Linq;

namespace Denapoli.Modules.Data.Entities
{
    [Table(Name = "denapoli.livreur")]
    public class Livreur : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {

        private static readonly System.ComponentModel.PropertyChangingEventArgs EmptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");

        private int _idlIVrEUr;
        private string _noM;
        private string _preNoM;
        private EntitySet<Commande> _commandes;


        public Livreur()
        {
            _commandes = new EntitySet<Commande>(CommandEAttach, CommandEDetach);
        }

        [Column(Storage = "_idlIVrEUr", Name = "ID_LIVREUR", DbType = "int", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode()]
        public int IDLiVReUR
        {
            get
            {
                return _idlIVrEUr;
            }
            set
            {
                if ((_idlIVrEUr == value)) return;
                SendPropertyChanging();
                _idlIVrEUr = value;
                SendPropertyChanged("IDLiVReUR");
            }
        }

        [Column(Storage = "_noM", Name = "NOM", DbType = "varchar(50)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public string NoM
        {
            get
            {
                return _noM;
            }
            set
            {
                if (((_noM == value)
                            == false))
                {
                    SendPropertyChanging();
                    _noM = value;
                    SendPropertyChanged("NoM");
                }
            }
        }

        [Column(Storage = "_preNoM", Name = "PRENOM", DbType = "varchar(50)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public string PreNoM
        {
            get
            {
                return _preNoM;
            }
            set
            {
                if (((_preNoM == value)
                            == false))
                {
                    SendPropertyChanging();
                    _preNoM = value;
                    SendPropertyChanged("PreNoM");
                }
            }
        }

        #region Children
        [Association(Storage = "_commandE", OtherKey = "IDLiVReUR", ThisKey = "IDLiVReUR", Name = "livreur_de_commande")]
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

        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
            if ((h != null))
            {
                h(this, EmptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
            if ((h != null))
            {
                h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Attachment handlers
        private void CommandEAttach(Commande entity)
        {
            SendPropertyChanging();
            entity.Livreur = this;
        }

        private void CommandEDetach(Commande entity)
        {
            SendPropertyChanging();
            entity.Livreur = null;
        }
        #endregion
    }
	
}