using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using DbLinq.Data.Linq;

namespace Denapoli.Modules.Data.Entities
{
    [Table(Name="famille")]
    public sealed class Famille : INotifyPropertyChanging, INotifyPropertyChanged
    {
		
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private int _idfAMil;
        private string _imageUrl;
        private string _noM;
        private string _description;
        private float _tva;
        private sbyte _isaPp;
        private sbyte _isWeb;
        private sbyte _isaCtIf;

        private EntitySet<ProduitComposition> _proDuiTcOmposition;
        private EntitySet<Produit> _produits;
		
        public Famille()
        {
            _proDuiTcOmposition = new EntitySet<ProduitComposition>(ProDuiTCompositionAttach, ProDuiTCompositionDetach);
            _produits = new EntitySet<Produit>(ProDuiTAttach, ProDuiTDetach);
        }

        private sbyte _isDeleted;
        [Column(Storage = "_isDeleted", Name = "IS_DELETED", DbType = "tinyint(1)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public sbyte IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                if ((_isDeleted == value)) return;
                SendPropertyChanging();
                _isDeleted = value;
                SendPropertyChanged("IsDeleted");
            }
        }

        public bool IsRemoved
        {
            get { return IsDeleted == 1; }
            set { IsDeleted = (sbyte)(value ? 1 : 0); }
        }

		
        [Column(Storage="_idfAMil", Name="ID_FAMIL", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int IDFaMil
        {
            get
            {
                return _idfAMil;
            }
            set
            {
                if ((_idfAMil == value)) return;
                SendPropertyChanging();
                _idfAMil = value;
                SendPropertyChanged("IDFaMil");
            }
        }
		
        [Column(Storage="_imageUrl", Name="IMAGE_URL", DbType="varchar(100)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string ImageURL
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                if ((_imageUrl == value)) return;
                SendPropertyChanging();
                _imageUrl = value;
                SendPropertyChanged("ImageURL");
            }
        }
		
        [Column(Storage="_noM", Name="NOM", DbType="varchar(100)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string Nom
        {
            get
            {
                return _noM;
            }
            set
            {
                if ((_noM == value)) return;
                SendPropertyChanging();
                _noM = value;
                SendPropertyChanged("Nom");
            }
        }

        [Column(Storage = "_description", Name = "DESCRIPTION", DbType = "varchar(150)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if ((_description == value)) return;
                SendPropertyChanging();
                _description = value;
                SendPropertyChanged("Description");
            }
        }


        [Column(Storage = "_tva", Name = "TVA", DbType = "float", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public float Tva
        {
            get
            {
                return _tva;
            }
            set
            {
                SendPropertyChanging();
                _tva = value;
                SendPropertyChanged("Tva");
            }
        }

        [Column(Storage = "_isaPp", Name = "IS_APP", DbType = "tinyint(1)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public sbyte IsaPp
        {
            get
            {
                return _isaPp;
            }
            set
            {
                if ((_isaPp != value))
                {
                    SendPropertyChanging();
                    _isaPp = value;
                    SendPropertyChanged("IsaPp");
                    SendPropertyChanged("IsApp");
                }
            }
        }

        [Column(Storage = "_isWeb", Name = "IS_WEB", DbType = "tinyint(1)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public sbyte IsWeb
        {
            get
            {
                return _isWeb;
            }
            set
            {
                if ((_isWeb == value)) return;
                SendPropertyChanging();
                _isWeb = value;
                SendPropertyChanged("IsWeb");
                SendPropertyChanged("IsWEB");
            }
        }


        [Column(Storage = "_isaCtIf", Name = "IS_ACTIF", DbType = "tinyint(1)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode()]
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

        private bool _isActif;
        public bool IsActif
        {
            get { return IsaCtIf == 1; }
            set
            {
                IsaCtIf = (sbyte)(value ? 1 : 0);
            }
        }

        public bool IsApp
        {
            get { return IsaPp==1; }
            set
            {
                IsaPp = (sbyte) (value ? 1 : 0);
            }
        }

        public bool IsWEB
        {
            get
            {
                return IsWeb == 1;
            }
            set
            {
                IsWeb = (sbyte) (value ? 1 : 0);
            }
        }

        #region Children
        [Association(Storage="_proDuiTcOmposition", OtherKey="IDFaMil", ThisKey="IDFaMil", Name="familles_produit")]
        [DebuggerNonUserCode]
        public EntitySet<ProduitComposition> ProduitComposition
        {
            get
            {
                return _proDuiTcOmposition;
            }
            set
            {
                _proDuiTcOmposition = value;
            }
        }
		
        [Association(Storage="_proDuiT", OtherKey="IDFaMil", ThisKey="IDFaMil", Name="famille_de_produit")]
        [DebuggerNonUserCode]
        public EntitySet<Produit> Produits
        {
            get
            {
                return _produits;
            }
            set
            {
                _produits = value;
            }
        }

        public List<Produit> Produitss{get{return new List<Produit>(Produits);}}

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
        private void ProDuiTCompositionAttach(ProduitComposition entity)
        {
            SendPropertyChanging();
            entity.Famille = this;
        }
		
        private void ProDuiTCompositionDetach(ProduitComposition entity)
        {
            SendPropertyChanging();
            entity.Famille = null;
        }
		
        private void ProDuiTAttach(Produit entity)
        {
            SendPropertyChanging();
            entity.Famille = this;
        }
		
        private void ProDuiTDetach(Produit entity)
        {
            SendPropertyChanging();
            entity.Famille = null;
        }
        #endregion
    }
}