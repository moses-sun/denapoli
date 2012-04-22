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
        private EntitySet<Commande> _Commande;

        public Livreur()
        {
            _Commande = new EntitySet<Commande>(Commande_Attach, Commande_Detach);
        }

        [Column(Storage = "_idlIVrEUr", Name = "ID_LIVREUR", DbType = "int", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
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
        [DebuggerNonUserCode()]
        public string NoM
        {
            get
            {
                return this._noM;
            }
            set
            {
                if (((_noM == value)
                     == false))
                {
                    this.SendPropertyChanging();
                    this._noM = value;
                    this.SendPropertyChanged("NoM");
                }
            }
        }

        [Column(Storage = "_preNoM", Name = "PRENOM", DbType = "varchar(50)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode()]
        public string PreNoM
        {
            get
            {
                return this._preNoM;
            }
            set
            {
                if (((_preNoM == value)
                     == false))
                {
                    this.SendPropertyChanging();
                    this._preNoM = value;
                    this.SendPropertyChanged("PreNoM");
                }
            }
        }

        #region Children
        [Association(Storage = "_Commande", OtherKey = "IDLiVReUR", ThisKey = "IDLiVReUR", Name = "livreur_de_Commande")]
        [DebuggerNonUserCode()]
        public EntitySet<Commande> Commande
        {
            get
            {
                return this._Commande;
            }
            set
            {
                this._Commande = value;
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
        private void Commande_Attach(Commande entity)
        {
            this.SendPropertyChanging();
            //entity.LiVReUR = this;
        }

        private void Commande_Detach(Commande entity)
        {
            this.SendPropertyChanging();
            //entity.LiVReUR = null;
        }
        #endregion
    }
}