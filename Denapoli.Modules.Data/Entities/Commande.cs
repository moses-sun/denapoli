using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.Windows.Media;
using DbLinq.Data.Linq;

namespace Denapoli.Modules.Data.Entities
{
    [Table(Name="denapoli.commande")]
    public sealed class Commande : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private DateTime? _date;
        private int _idaDr;
        private int _idbOrn;
        private int _idclIen;
        private int _num;
        private string _statut;
        private float _total;
        private EntitySet<Menu> _menus;
        private EntitySet<ProduitsCommande> _prodUiTsCommandE;
        private EntityRef<Adresse> _adRessE;
        private EntityRef<Borne> _borne;
        private EntityRef<Client> _client;
        private int? _idlIVrEUr;
        private EntityRef<Livreur> _liVrEUr = new EntityRef<Livreur>();
		
        public Commande()
        {
            _menus = new EntitySet<Menu>(MenuAttach, MenuDetach);
            _prodUiTsCommandE = new EntitySet<ProduitsCommande>(ProdUItsCommandEAttach, ProdUItsCommandEDetach);
        }

        [Column(Storage = "_idlIVrEUr", Name = "ID_LIVREUR", DbType = "int", AutoSync = AutoSync.Never)]
        [DebuggerNonUserCode()]
        public int? IDLiVReUR
        {
            get
            {
                return this._idlIVrEUr;
            }
            set
            {
                if ((_idlIVrEUr != value))
                {
                    this.SendPropertyChanging();
                    this._idlIVrEUr = value;
                    this.SendPropertyChanged("IDLiVReUR");
                }
            }
        }

        [Column(Storage = "_date", Name = "DATE", DbType = "datetime", AutoSync = AutoSync.Never)]
        [DebuggerNonUserCode()]
        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                if ((_date == value)) _date = DateTime.Now;
                SendPropertyChanging();
                _date = value;
                SendPropertyChanged("Date");
            }
        }

        private SolidColorBrush _color = Brushes.Green;
        public SolidColorBrush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                SendPropertyChanged("Color");
            }
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
		
        [Column(Storage="_idbOrn", Name="ID_BORN", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int IDBorn
        {
            get
            {
                return _idbOrn;
            }
            set
            {
                if ((_idbOrn == value)) return;
                SendPropertyChanging();
                _idbOrn = value;
                SendPropertyChanged("IDBorn");
            }
        }
		
        [Column(Storage="_idclIen", Name="ID_CLIEN", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int IDCLien
        {
            get
            {
                return _idclIen;
            }
            set
            {
                if ((_idclIen == value)) return;
                SendPropertyChanging();
                _idclIen = value;
                SendPropertyChanged("IDCLien");
            }
        }
		
        [Column(Storage="_num", Name="NUM", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int Num
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

        [Column(Storage = "_statut", Name = "STATUT", DbType = "varchar(20)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public string Statut
        {
            get
            {
                return _statut;
            }
            set
            {
                if ((_statut == value)) return;
                SendPropertyChanging();
                _statut = value;
                SendPropertyChanged("Statut");
            }
        }
		
        [Column(Storage="_total", Name="TOTAL", DbType="float", AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public float Total
        {
            get
            {
                return _total;
            }
            set
            {
                SendPropertyChanging();
                _total = value;
                SendPropertyChanged("Total");
            }
        }
		
        #region Children
        [Association(Storage="_menu", OtherKey="NumCom", ThisKey="Num", Name="commande_du_menu")]
        [DebuggerNonUserCode]
        public EntitySet<Menu> Menus
        {
            get
            {
                return _menus;
            }
            set
            {
                _menus = value;
            }
        }
		
        [Association(Storage="_prodUiTsCommandE", OtherKey="NumCom", ThisKey="Num", Name="commande_produits")]
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
        #endregion
		
        #region Parents
        [Association(Storage="_adRessE", OtherKey="IdaDr", ThisKey="IdaDr", Name="adresse_de_commande", IsForeignKey=true)]
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
                    previousAdresse.Commandes.Remove(this);
                }
                _adRessE.Entity = value;
                if ((value != null))
                {
                    value.Commandes.Add(this);
                    _idaDr = value.IdaDr;
                }
                else
                {
                    _idaDr = default(int);
                }
            }
        }
		
        [Association(Storage="_borne", OtherKey="IDBorn", ThisKey="IDBorn", Name="borne_qui_commande", IsForeignKey=true)]
        [DebuggerNonUserCode]
        public Borne Borne
        {
            get
            {
                return _borne.Entity;
            }
            set
            {
                if ((_borne.Entity == value)) return;
                if ((_borne.Entity != null))
                {
                    var previousBorne = _borne.Entity;
                    _borne.Entity = null;
                    previousBorne.Commandes.Remove(this);
                }
                _borne.Entity = value;
                if ((value != null))
                {
                    value.Commandes.Add(this);
                    _idbOrn = value.IDBorn;
                }
                else
                {
                    _idbOrn = default(int);
                }
            }
        }
		
        [Association(Storage="_client", OtherKey="IDCLien", ThisKey="IDCLien", Name="client_qui_commande", IsForeignKey=true)]
        [DebuggerNonUserCode]
        public Client Client
        {
            get
            {
                return _client.Entity;
            }
            set
            {
                if ((_client.Entity == value)) return;
                if ((_client.Entity != null))
                {
                    var previousClient = _client.Entity;
                    _client.Entity = null;
                    previousClient.Commandes.Remove(this);
                }
                _client.Entity = value;
                if ((value != null))
                {
                    value.Commandes.Add(this);
                    _idclIen = value.IDCLien;
                }
                else
                {
                    _idclIen = default(int);
                }
            }
        }

        [Association(Storage = "_liVrEUr", OtherKey = "IDLiVReUR", ThisKey = "IDLiVReUR", Name = "livreur_de_commande", IsForeignKey = true)]
        [DebuggerNonUserCode]
        public Livreur Livreur
        {
            get
            {
                return _liVrEUr.Entity;
            }
            set
            {
                if ((_liVrEUr.Entity == value)) return;
                if ((_liVrEUr.Entity != null))
                {
                    var previousLiVreUr = _liVrEUr.Entity;
                    _liVrEUr.Entity = null;
                    previousLiVreUr.Commandes.Remove(this);
                }
                _liVrEUr.Entity = value;
                if ((value != null))
                {
                    value.Commandes.Add(this);
                    _idlIVrEUr = value.IDLiVReUR;
                }
                else
                {
                    _idlIVrEUr = null;
                }
            }
        }

        private int _chrono;
        public int Chrono
        {
            set
            {
                _chrono = value;
                if (_chrono < 46) Color = Brushes.Green;
                if (_chrono < 30) Color = Brushes.Orange;
                if (_chrono < 15) Color = Brushes.Red;
                SendPropertyChanged("Chrono");
            }
            get { return _chrono; }
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
            entity.Commande = this;
        }
		
        private void MenuDetach(Menu entity)
        {
            SendPropertyChanging();
            entity.Commande = null;
        }
		
        private void ProdUItsCommandEAttach(ProduitsCommande entity)
        {
            SendPropertyChanging();
            entity.Commande = this;
        }
		
        private void ProdUItsCommandEDetach(ProduitsCommande entity)
        {
            SendPropertyChanging();
            entity.Commande = null;
        }
        #endregion
    }



    [Table(Name = "denapoli.produits_commande")]
    public sealed class ProduitsCommande : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private int _idpRod;
        private int _numCom;
        private int _quaNtItE;
        private EntityRef<Commande> _commandE;
        private EntityRef<Produit> _proDuiT;

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

        [Column(Storage = "_numCom", Name = "NUM_COM", DbType = "int", IsPrimaryKey = true, AutoSync = AutoSync.Never, CanBeNull = false)]
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
                SendPropertyChanged("Quantite");
            }
        }

        #region Parents
        [Association(Storage = "_commandE", OtherKey = "Num", ThisKey = "NumCom", Name = "commande_produits", IsForeignKey = true)]
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
                    previousCommande.ProduitsCommande.Remove(this);
                }
                _commandE.Entity = value;
                if ((value != null))
                {
                    value.ProduitsCommande.Add(this);
                    _numCom = value.Num;
                }
                else
                {
                    _numCom = default(int);
                }
            }
        }

        [Association(Storage = "_proDuiT", OtherKey = "IDProd", ThisKey = "IDProd", Name = "produits_commande", IsForeignKey = true)]
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
                    previousProduit.ProduitsCommande.Remove(this);
                }
                _proDuiT.Entity = value;
                if ((value != null))
                {
                    value.ProduitsCommande.Add(this);
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