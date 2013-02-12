using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using DbLinq.Data.Linq;

namespace Denapoli.Modules.Data.Entities
{
    [Table(Name="produit")]
    public sealed class Produit : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private string _description;
        private int? _idfAMil;
        private int _idpRod;
        private string _imageUrl;
        private string _noM;
        private float _prIX;
        private float _tva;
        private sbyte _isaPp;
        private sbyte _isWeb;
        private sbyte _isaCtIf;
		
        private EntitySet<Menu> _menu;
        private EntitySet<ProduitsCommande> _prodUiTsCommandE;
        private EntitySet<ProduitComposition> _proDuiTcOmposition;
        private EntitySet<ProduitsMenu> _prodUiTsMenu;
        private EntityRef<Famille> _faMillE;
		
        public Produit()
        {
            _menu = new EntitySet<Menu>(MenuAttach, MenuDetach);
            _prodUiTsCommandE = new EntitySet<ProduitsCommande>(ProdUItsCommandEAttach, ProdUItsCommandEDetach);
            _proDuiTcOmposition = new EntitySet<ProduitComposition>(ProDuiTCompositionAttach, ProDuiTCompositionDetach);
            _prodUiTsMenu = new EntitySet<ProduitsMenu>(ProdUItsMenuAttach, ProdUItsMenuDetach);
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


        public int Column { get; set; }
        public int Row { get; set; }

        public bool IsMenu { get { return ProduitComposition.Count > 0; } }
		
        [Column(Storage="_description", Name="DESCRIPTION", DbType="varchar(200)", AutoSync=AutoSync.Never)]
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
		
        [Column(Storage="_idfAMil", Name="ID_FAMIL", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int? IDFaMil
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
		
        [Column(Storage="_idpRod", Name="ID_PROD", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int IDProd
        {
            get
            {
                return _idpRod;
            }
            set
            {
                if ((_idpRod == value)) return;
                SendPropertyChanging();
                _idpRod = value;
                SendPropertyChanged("IDProd");
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
		
        [Column(Storage="_noM", Name="NOM", DbType="varchar(50)", AutoSync=AutoSync.Never)]
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
		
        [Column(Storage="_prIX", Name="PRIX", DbType="float", AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public float Prix
        {
            get
            {
                return _prIX;
            }
            set
            {
                SendPropertyChanging();
                _prIX = value;
                SendPropertyChanged("Prix");
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
                IsaCtIf = (sbyte) (value ? 1 : 0);
            }
        }

        public bool IsApp
        {
            get { return IsaPp == 1; }
            set
            {
                IsaPp = (sbyte)(value ? 1 : 0);
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
                IsWeb = (sbyte)(value ? 1 : 0);
            }
        }



        #region Children
        [Association(Storage="_menu", OtherKey="IDProd", ThisKey="IDProd", Name="menu")]
        [DebuggerNonUserCode]
        public EntitySet<Menu> Menu
        {
            get
            {
                return _menu;
            }
            set
            {
                _menu = value;
            }
        }
		
        [Association(Storage="_prodUiTsCommandE", OtherKey="IDProd", ThisKey="IDProd", Name="produits_commande")]
        [DebuggerNonUserCode]
        public EntitySet<ProduitsCommande> ProduitsCommande
        {
            get
            {
                return _prodUiTsCommandE;
            }
            set
            {
                _prodUiTsCommandE = value;
            }
        }
		
        [Association(Storage="_proDuiTcOmposition", OtherKey="IDProd", ThisKey="IDProd", Name="produits_famille")]
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
		
        [Association(Storage="_prodUiTsMenu", OtherKey="IDProd", ThisKey="IDProd", Name="produits_menu")]
        [DebuggerNonUserCode]
        public EntitySet<ProduitsMenu> ProduitsMenu
        {
            get
            {
                return _prodUiTsMenu;
            }
            set
            {
                _prodUiTsMenu = value;
            }
        }
        #endregion
		
        #region Parents
        [Association(Storage="_faMillE", OtherKey="IDFaMil", ThisKey="IDFaMil", Name="famille_de_produit", IsForeignKey=true)]
        [DebuggerNonUserCode]
        public Famille Famille
        {
            get
            {
                return _faMillE.Entity;
            }
            set
            {
                if ((_faMillE.Entity == value)) return;
                if ((_faMillE.Entity != null))
                {
                    var previousFamille = _faMillE.Entity;
                    _faMillE.Entity = null;
                    previousFamille.Produits.Remove(this);
                }
                _faMillE.Entity = value;
                if ((value != null))
                {
                    value.Produits.Add(this);
                    _idfAMil = value.IDFaMil;
                }
                else
                {
                    _idfAMil = default(int);
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
        private void MenuAttach(Menu entity)
        {
            SendPropertyChanging();
            entity.Produit = this;
        }
		
        private void MenuDetach(Menu entity)
        {
            SendPropertyChanging();
            entity.Produit = null;
        }
		
        private void ProdUItsCommandEAttach(ProduitsCommande entity)
        {
            SendPropertyChanging();
            entity.Produit = this;
        }
		
        private void ProdUItsCommandEDetach(ProduitsCommande entity)
        {
            SendPropertyChanging();
            entity.Produit = null;
        }
		
        private void ProDuiTCompositionAttach(ProduitComposition entity)
        {
            SendPropertyChanging();
            entity.Produit = this;
        }
		
        private void ProDuiTCompositionDetach(ProduitComposition entity)
        {
            SendPropertyChanging();
            entity.Produit = null;
        }
		
        private void ProdUItsMenuAttach(ProduitsMenu entity)
        {
            SendPropertyChanging();
            entity.Produit = this;
        }
		
        private void ProdUItsMenuDetach(ProduitsMenu entity)
        {
            SendPropertyChanging();
            entity.Produit = null;
        }
        #endregion
    }

    [Table(Name = "produit_composition")]
    public sealed class ProduitComposition : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private int _idfAMil;
        private int _idpRod;
        private sbyte _ismEmE;
        private int? _quaNtItE;
        private EntityRef<Famille> _faMillE;
        private EntityRef<Produit> _proDuiT;

        [Column(Storage = "_idfAMil", Name = "ID_FAMIL", DbType = "int", IsPrimaryKey = true, AutoSync = AutoSync.Never, CanBeNull = false)]
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

        [Column(Storage = "_idpRod", Name = "ID_PROD", DbType = "int", IsPrimaryKey = true, AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public int IDProd
        {
            get
            {
                return _idpRod;
            }
            set
            {
                if ((_idpRod != value))
                {
                    SendPropertyChanging();
                    _idpRod = value;
                    SendPropertyChanged("IDProd");
                }
            }
        }

        [Column(Storage = "_quaNtItE", Name = "QUANTITE", DbType = "int", AutoSync = AutoSync.Never)]
        [DebuggerNonUserCode]
        public int? Quantite
        {
            get
            {
                return _quaNtItE;
            }
            set
            {
                if ((_quaNtItE == value)) return;
                SendPropertyChanging();
                _quaNtItE = value;
                SendPropertyChanged("Quantite");
            }
        }

        [Column(Storage = "_ismEmE", Name = "IS_MEME", DbType = "tinyint(1)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public sbyte IsmEmE
        {
            get
            {
                return _ismEmE;
            }
            set
            {
                if ((_ismEmE == value)) return;
                SendPropertyChanging();
                _ismEmE = value;
                SendPropertyChanged("IsmEmE");
            }
        }

        public bool IsMeme
        {
            get { return IsmEmE==1; }
            set { IsmEmE = (sbyte) (value ? 1 : 0); }
        }

        #region Parents
        [Association(Storage = "_faMillE", OtherKey = "IDFaMil", ThisKey = "IDFaMil", Name = "familles_produit", IsForeignKey = true)]
        [DebuggerNonUserCode]
        public Famille Famille
        {
            get
            {
                return _faMillE.Entity;
            }
            set
            {
                if ((_faMillE.Entity == value)) return;
                if ((_faMillE.Entity != null))
                {
                    var previousFamille = _faMillE.Entity;
                    _faMillE.Entity = null;
                    previousFamille.ProduitComposition.Remove(this);
                }
                _faMillE.Entity = value;
                if ((value != null))
                {
                    value.ProduitComposition.Add(this);
                    _idfAMil = value.IDFaMil;
                }
                else
                {
                    _idfAMil = default(int);
                }
            }
        }

        [Association(Storage = "_proDuiT", OtherKey = "IDProd", ThisKey = "IDProd", Name = "produits_famille", IsForeignKey = true)]
        [DebuggerNonUserCode]
        public Produit Produit
        {
            get
            {
                return _proDuiT.Entity;
            }
            set
            {
                if ((_proDuiT.Entity == value)) return;
                if ((_proDuiT.Entity != null))
                {
                    var previousProduit = _proDuiT.Entity;
                    _proDuiT.Entity = null;
                    previousProduit.ProduitComposition.Remove(this);
                }
                _proDuiT.Entity = value;
                if ((value != null))
                {
                    value.ProduitComposition.Add(this);
                    _idpRod = value.IDProd;
                }
                else
                {
                    _idpRod = default(int);
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
    }
}