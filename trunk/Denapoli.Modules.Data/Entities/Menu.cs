using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using DbLinq.Data.Linq;

namespace Denapoli.Modules.Data.Entities
{
    [Table(Name="denapoli.menu")]
    public sealed class Menu : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private int _idmEnu;
        private int _idpRod;
        private int _numCom;
        private int _quaNtItE;
        private EntitySet<ProduitsMenu> _prodUiTsMenu;
        private EntityRef<Commande> _commandE;
        private EntityRef<Produit> _proDuiT;
		
        public Menu()
        {
            _prodUiTsMenu = new EntitySet<ProduitsMenu>(ProdUItsMenuAttach, ProdUItsMenuDetach);
        }
		
        [Column(Storage="_idmEnu", Name="ID_MENU", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int IDMenu
        {
            get
            {
                return _idmEnu;
            }
            set
            {
                if ((_idmEnu == value)) return;
                SendPropertyChanging();
                _idmEnu = value;
                SendPropertyChanged("IDMenu");
            }
        }
		
        [Column(Storage="_idpRod", Name="ID_PROD", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
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
		
        [Column(Storage="_numCom", Name="NUM_COM", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int NumCom
        {
            get
            {
                return _numCom;
            }
            set
            {
                if ((_numCom == value)) return;
                SendPropertyChanging();
                _numCom = value;
                SendPropertyChanged("NumCom");
            }
        }

        [Column(Storage = "_quaNtItE", Name = "QUANTITE", DbType = "int", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public int Quantite
        {
            get
            {
                return _quaNtItE;
            }
            set
            {
                if ((_quaNtItE != value))
                {
                    SendPropertyChanging();
                    _quaNtItE = value;
                    SendPropertyChanged("Quantite");
                }
            }
        }
		
		
        #region Children
        [Association(Storage="_prodUiTsMenu", OtherKey="IDMenu", ThisKey="IDMenu", Name="menu_produits")]
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
        [Association(Storage="_commandE", OtherKey="Num", ThisKey="NumCom", Name="commande_du_menu", IsForeignKey=true)]
        [DebuggerNonUserCode]
        public Commande Commande
        {
            get
            {
                return _commandE.Entity;
            }
            set
            {
                if ((_commandE.Entity == value)) return;
                if ((_commandE.Entity != null))
                {
                    var previousCommande = _commandE.Entity;
                    _commandE.Entity = null;
                    previousCommande.Menus.Remove(this);
                }
                _commandE.Entity = value;
                if ((value != null))
                {
                    value.Menus.Add(this);
                    _numCom = value.Num;
                }
                else
                {
                    _numCom = default(int);
                }
            }
        }
		
        [Association(Storage="_proDuiT", OtherKey="IDProd", ThisKey="IDProd", Name="menu", IsForeignKey=true)]
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
                    previousProduit.Menu.Remove(this);
                }
                _proDuiT.Entity = value;
                if ((value != null))
                {
                    value.Menu.Add(this);
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
		
        #region Attachment handlers
        private void ProdUItsMenuAttach(ProduitsMenu entity)
        {
            SendPropertyChanging();
            entity.Menu = this;
        }
		
        private void ProdUItsMenuDetach(ProduitsMenu entity)
        {
            SendPropertyChanging();
            entity.Menu = null;
        }
        #endregion
    }



    [Table(Name = "denapoli.produits_menu")]
    public sealed class ProduitsMenu : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private int _idmEnu;
        private int _idpRod;
        private int _quaNtItE;
        private EntityRef<Menu> _menu;
        private EntityRef<Produit> _proDuiT;

        [Column(Storage = "_idmEnu", Name = "ID_MENU", DbType = "int", IsPrimaryKey = true, AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public int IDMenu
        {
            get
            {
                return _idmEnu;
            }
            set
            {
                if ((_idmEnu == value)) return;
                SendPropertyChanging();
                _idmEnu = value;
                SendPropertyChanged("IDMenu");
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
                if ((_idpRod == value)) return;
                SendPropertyChanging();
                _idpRod = value;
                SendPropertyChanged("IDProd");
            }
        }

        [Column(Storage = "_quaNtItE", Name = "QUANTITE", DbType = "int(6)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public int Quantite
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
                SendPropertyChanged("QuaNtItE");
            }
        }

        #region Parents
        [Association(Storage = "_menu", OtherKey = "IDMenu", ThisKey = "IDMenu", Name = "menu_produits", IsForeignKey = true)]
        [DebuggerNonUserCode]
        public Menu Menu
        {
            get
            {
                return _menu.Entity;
            }
            set
            {
                if (((_menu.Entity == value)
                            == false))
                {
                    if ((_menu.Entity != null))
                    {
                        var previousMenu = _menu.Entity;
                        _menu.Entity = null;
                        previousMenu.ProduitsMenu.Remove(this);
                    }
                    _menu.Entity = value;
                    if ((value != null))
                    {
                        value.ProduitsMenu.Add(this);
                        _idmEnu = value.IDMenu;
                    }
                    else
                    {
                        _idmEnu = default(int);
                    }
                }
            }
        }

        [Association(Storage = "_proDuiT", OtherKey = "IDProd", ThisKey = "IDProd", Name = "produits_menu", IsForeignKey = true)]
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
                    previousProduit.ProduitsMenu.Remove(this);
                }
                _proDuiT.Entity = value;
                if ((value != null))
                {
                    value.ProduitsMenu.Add(this);
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