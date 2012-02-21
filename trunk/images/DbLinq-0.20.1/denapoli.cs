// 
//  ____  _     __  __      _        _ 
// |  _ \| |__ |  \/  | ___| |_ __ _| |
// | | | | '_ \| |\/| |/ _ \ __/ _` | |
// | |_| | |_) | |  | |  __/ || (_| | |
// |____/|_.__/|_|  |_|\___|\__\__,_|_|
//
// Auto-generated from denapoli on 2012-02-02 22:30:23Z.
// Please visit http://code.google.com/p/dblinq2007/ for more information.
//
namespace denapoli
{
	using System;
	using System.ComponentModel;
	using System.Data;
#if MONO_STRICT
	using System.Data.Linq;
#else   // MONO_STRICT
	using DbLinq.Data.Linq;
	using DbLinq.Vendor;
#endif  // MONO_STRICT
	using System.Data.Linq.Mapping;
	using System.Diagnostics;
	
	
	public partial class DenApoLi : DataContext
	{
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		#endregion
		
		
		public DenApoLi(string connectionString) : 
				base(connectionString)
		{
			this.OnCreated();
		}
		
		public DenApoLi(string connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public DenApoLi(IDbConnection connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public Table<ADressE> ADressE
		{
			get
			{
				return this.GetTable<ADressE>();
			}
		}
		
		public Table<Borne> Borne
		{
			get
			{
				return this.GetTable<Borne>();
			}
		}
		
		public Table<Client> Client
		{
			get
			{
				return this.GetTable<Client>();
			}
		}
		
		public Table<CommandE> CommandE
		{
			get
			{
				return this.GetTable<CommandE>();
			}
		}
		
		public Table<FaMillE> FaMillE
		{
			get
			{
				return this.GetTable<FaMillE>();
			}
		}
		
		public Table<Menu> Menu
		{
			get
			{
				return this.GetTable<Menu>();
			}
		}
		
		public Table<ProDuiT> ProDuiT
		{
			get
			{
				return this.GetTable<ProDuiT>();
			}
		}
		
		public Table<ProDuiTComposition> ProDuiTComposition
		{
			get
			{
				return this.GetTable<ProDuiTComposition>();
			}
		}
		
		public Table<ProdUItsCommandE> ProdUItsCommandE
		{
			get
			{
				return this.GetTable<ProdUItsCommandE>();
			}
		}
		
		public Table<ProdUItsMenu> ProdUItsMenu
		{
			get
			{
				return this.GetTable<ProdUItsMenu>();
			}
		}
	}
	
	#region Start MONO_STRICT
#if MONO_STRICT

	public partial class DenApoLi
	{
		
		public DenApoLi(IDbConnection connection) : 
				base(connection)
		{
			this.OnCreated();
		}
	}
	#region End MONO_STRICT
	#endregion
#else     // MONO_STRICT
	
	public partial class DenApoLi
	{
		
		public DenApoLi(IDbConnection connection) : 
				base(connection, new DbLinq.MySql.MySqlVendor())
		{
			this.OnCreated();
		}
		
		public DenApoLi(IDbConnection connection, IVendor sqlDialect) : 
				base(connection, sqlDialect)
		{
			this.OnCreated();
		}
		
		public DenApoLi(IDbConnection connection, MappingSource mappingSource, IVendor sqlDialect) : 
				base(connection, mappingSource, sqlDialect)
		{
			this.OnCreated();
		}
	}
	#region End Not MONO_STRICT
	#endregion
#endif     // MONO_STRICT
	#endregion
	
	[Table(Name="denapoli.adresse")]
	public partial class ADressE : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _complement;
		
		private string _cp;
		
		private int _idaDr;
		
		private System.Nullable<int> _num;
		
		private string _viLlE;
		
		private string _voiE;
		
		private EntitySet<Borne> _borne;
		
		private EntitySet<CommandE> _commandE;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnComplementChanged();
		
		partial void OnComplementChanging(string value);
		
		partial void OnCPChanged();
		
		partial void OnCPChanging(string value);
		
		partial void OnIdaDrChanged();
		
		partial void OnIdaDrChanging(int value);
		
		partial void OnNumChanged();
		
		partial void OnNumChanging(System.Nullable<int> value);
		
		partial void OnVIllEChanged();
		
		partial void OnVIllEChanging(string value);
		
		partial void OnVOIeChanged();
		
		partial void OnVOIeChanging(string value);
		#endregion
		
		
		public ADressE()
		{
			_borne = new EntitySet<Borne>(new Action<Borne>(this.Borne_Attach), new Action<Borne>(this.Borne_Detach));
			_commandE = new EntitySet<CommandE>(new Action<CommandE>(this.CommandE_Attach), new Action<CommandE>(this.CommandE_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_complement", Name="COMPLEMENT", DbType="varchar(200)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Complement
		{
			get
			{
				return this._complement;
			}
			set
			{
				if (((_complement == value) 
							== false))
				{
					this.OnComplementChanging(value);
					this.SendPropertyChanging();
					this._complement = value;
					this.SendPropertyChanged("Complement");
					this.OnComplementChanged();
				}
			}
		}
		
		[Column(Storage="_cp", Name="CP", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CP
		{
			get
			{
				return this._cp;
			}
			set
			{
				if (((_cp == value) 
							== false))
				{
					this.OnCPChanging(value);
					this.SendPropertyChanging();
					this._cp = value;
					this.SendPropertyChanged("CP");
					this.OnCPChanged();
				}
			}
		}
		
		[Column(Storage="_idaDr", Name="ID_ADR", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IdaDr
		{
			get
			{
				return this._idaDr;
			}
			set
			{
				if ((_idaDr != value))
				{
					this.OnIdaDrChanging(value);
					this.SendPropertyChanging();
					this._idaDr = value;
					this.SendPropertyChanged("IdaDr");
					this.OnIdaDrChanged();
				}
			}
		}
		
		[Column(Storage="_num", Name="NUM", DbType="int(6)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> Num
		{
			get
			{
				return this._num;
			}
			set
			{
				if ((_num != value))
				{
					this.OnNumChanging(value);
					this.SendPropertyChanging();
					this._num = value;
					this.SendPropertyChanged("Num");
					this.OnNumChanged();
				}
			}
		}
		
		[Column(Storage="_viLlE", Name="VILLE", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string VIllE
		{
			get
			{
				return this._viLlE;
			}
			set
			{
				if (((_viLlE == value) 
							== false))
				{
					this.OnVIllEChanging(value);
					this.SendPropertyChanging();
					this._viLlE = value;
					this.SendPropertyChanged("VIllE");
					this.OnVIllEChanged();
				}
			}
		}
		
		[Column(Storage="_voiE", Name="VOIE", DbType="varchar(200)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string VOIe
		{
			get
			{
				return this._voiE;
			}
			set
			{
				if (((_voiE == value) 
							== false))
				{
					this.OnVOIeChanging(value);
					this.SendPropertyChanging();
					this._voiE = value;
					this.SendPropertyChanged("VOIe");
					this.OnVOIeChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_borne", OtherKey="IdaDr", ThisKey="IdaDr", Name="adresse_de_borne")]
		[DebuggerNonUserCode()]
		public EntitySet<Borne> Borne
		{
			get
			{
				return this._borne;
			}
			set
			{
				this._borne = value;
			}
		}
		
		[Association(Storage="_commandE", OtherKey="IdaDr", ThisKey="IdaDr", Name="adresse_de_commande")]
		[DebuggerNonUserCode()]
		public EntitySet<CommandE> CommandE
		{
			get
			{
				return this._commandE;
			}
			set
			{
				this._commandE = value;
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
				h(this, emptyChangingEventArgs);
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
		private void Borne_Attach(Borne entity)
		{
			this.SendPropertyChanging();
			entity.ADressE = this;
		}
		
		private void Borne_Detach(Borne entity)
		{
			this.SendPropertyChanging();
			entity.ADressE = null;
		}
		
		private void CommandE_Attach(CommandE entity)
		{
			this.SendPropertyChanging();
			entity.ADressE = this;
		}
		
		private void CommandE_Detach(CommandE entity)
		{
			this.SendPropertyChanging();
			entity.ADressE = null;
		}
		#endregion
	}
	
	[Table(Name="denapoli.borne")]
	public partial class Borne : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _idaDr;
		
		private int _idbOrn;
		
		private EntitySet<CommandE> _commandE;
		
		private EntityRef<ADressE> _adRessE = new EntityRef<ADressE>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnIdaDrChanged();
		
		partial void OnIdaDrChanging(int value);
		
		partial void OnIDBornChanged();
		
		partial void OnIDBornChanging(int value);
		#endregion
		
		
		public Borne()
		{
			_commandE = new EntitySet<CommandE>(new Action<CommandE>(this.CommandE_Attach), new Action<CommandE>(this.CommandE_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_idaDr", Name="ID_ADR", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IdaDr
		{
			get
			{
				return this._idaDr;
			}
			set
			{
				if ((_idaDr != value))
				{
					this.OnIdaDrChanging(value);
					this.SendPropertyChanging();
					this._idaDr = value;
					this.SendPropertyChanged("IdaDr");
					this.OnIdaDrChanged();
				}
			}
		}
		
		[Column(Storage="_idbOrn", Name="ID_BORN", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDBorn
		{
			get
			{
				return this._idbOrn;
			}
			set
			{
				if ((_idbOrn != value))
				{
					this.OnIDBornChanging(value);
					this.SendPropertyChanging();
					this._idbOrn = value;
					this.SendPropertyChanged("IDBorn");
					this.OnIDBornChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_commandE", OtherKey="IDBorn", ThisKey="IDBorn", Name="borne_qui_commande")]
		[DebuggerNonUserCode()]
		public EntitySet<CommandE> CommandE
		{
			get
			{
				return this._commandE;
			}
			set
			{
				this._commandE = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_adRessE", OtherKey="IdaDr", ThisKey="IdaDr", Name="adresse_de_borne", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public ADressE ADressE
		{
			get
			{
				return this._adRessE.Entity;
			}
			set
			{
				if (((this._adRessE.Entity == value) 
							== false))
				{
					if ((this._adRessE.Entity != null))
					{
						ADressE previousADressE = this._adRessE.Entity;
						this._adRessE.Entity = null;
						previousADressE.Borne.Remove(this);
					}
					this._adRessE.Entity = value;
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
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
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
		private void CommandE_Attach(CommandE entity)
		{
			this.SendPropertyChanging();
			entity.Borne = this;
		}
		
		private void CommandE_Detach(CommandE entity)
		{
			this.SendPropertyChanging();
			entity.Borne = null;
		}
		#endregion
	}
	
	[Table(Name="denapoli.client")]
	public partial class Client : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _email;
		
		private int _idclIen;
		
		private string _noM;
		
		private string _preNoM;
		
		private string _teL;
		
		private EntitySet<CommandE> _commandE;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnEmailChanged();
		
		partial void OnEmailChanging(string value);
		
		partial void OnIDCLienChanged();
		
		partial void OnIDCLienChanging(int value);
		
		partial void OnNoMChanged();
		
		partial void OnNoMChanging(string value);
		
		partial void OnPreNoMChanged();
		
		partial void OnPreNoMChanging(string value);
		
		partial void OnTeLChanged();
		
		partial void OnTeLChanging(string value);
		#endregion
		
		
		public Client()
		{
			_commandE = new EntitySet<CommandE>(new Action<CommandE>(this.CommandE_Attach), new Action<CommandE>(this.CommandE_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_email", Name="EMAIL", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				if (((_email == value) 
							== false))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[Column(Storage="_idclIen", Name="ID_CLIEN", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDCLien
		{
			get
			{
				return this._idclIen;
			}
			set
			{
				if ((_idclIen != value))
				{
					this.OnIDCLienChanging(value);
					this.SendPropertyChanging();
					this._idclIen = value;
					this.SendPropertyChanged("IDCLien");
					this.OnIDCLienChanged();
				}
			}
		}
		
		[Column(Storage="_noM", Name="NOM", DbType="varchar(50)", AutoSync=AutoSync.Never)]
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
					this.OnNoMChanging(value);
					this.SendPropertyChanging();
					this._noM = value;
					this.SendPropertyChanged("NoM");
					this.OnNoMChanged();
				}
			}
		}
		
		[Column(Storage="_preNoM", Name="PRENOM", DbType="varchar(50)", AutoSync=AutoSync.Never)]
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
					this.OnPreNoMChanging(value);
					this.SendPropertyChanging();
					this._preNoM = value;
					this.SendPropertyChanged("PreNoM");
					this.OnPreNoMChanged();
				}
			}
		}
		
		[Column(Storage="_teL", Name="TEL", DbType="varchar(15)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string TeL
		{
			get
			{
				return this._teL;
			}
			set
			{
				if (((_teL == value) 
							== false))
				{
					this.OnTeLChanging(value);
					this.SendPropertyChanging();
					this._teL = value;
					this.SendPropertyChanged("TeL");
					this.OnTeLChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_commandE", OtherKey="IDCLien", ThisKey="IDCLien", Name="client_qui_commande")]
		[DebuggerNonUserCode()]
		public EntitySet<CommandE> CommandE
		{
			get
			{
				return this._commandE;
			}
			set
			{
				this._commandE = value;
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
				h(this, emptyChangingEventArgs);
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
		private void CommandE_Attach(CommandE entity)
		{
			this.SendPropertyChanging();
			entity.Client = this;
		}
		
		private void CommandE_Detach(CommandE entity)
		{
			this.SendPropertyChanging();
			entity.Client = null;
		}
		#endregion
	}
	
	[Table(Name="denapoli.commande")]
	public partial class CommandE : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _idaDr;
		
		private int _idbOrn;
		
		private int _idclIen;
		
		private int _num;
		
		private string _statUt;
		
		private float _total;
		
		private EntitySet<Menu> _menu;
		
		private EntitySet<ProdUItsCommandE> _prodUiTsCommandE;
		
		private EntityRef<ADressE> _adRessE = new EntityRef<ADressE>();
		
		private EntityRef<Borne> _borne = new EntityRef<Borne>();
		
		private EntityRef<Client> _client = new EntityRef<Client>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnIdaDrChanged();
		
		partial void OnIdaDrChanging(int value);
		
		partial void OnIDBornChanged();
		
		partial void OnIDBornChanging(int value);
		
		partial void OnIDCLienChanged();
		
		partial void OnIDCLienChanging(int value);
		
		partial void OnNumChanged();
		
		partial void OnNumChanging(int value);
		
		partial void OnStatUtChanged();
		
		partial void OnStatUtChanging(string value);
		
		partial void OnTotalChanged();
		
		partial void OnTotalChanging(float value);
		#endregion
		
		
		public CommandE()
		{
			_menu = new EntitySet<Menu>(new Action<Menu>(this.Menu_Attach), new Action<Menu>(this.Menu_Detach));
			_prodUiTsCommandE = new EntitySet<ProdUItsCommandE>(new Action<ProdUItsCommandE>(this.ProdUItsCommandE_Attach), new Action<ProdUItsCommandE>(this.ProdUItsCommandE_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_idaDr", Name="ID_ADR", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IdaDr
		{
			get
			{
				return this._idaDr;
			}
			set
			{
				if ((_idaDr != value))
				{
					this.OnIdaDrChanging(value);
					this.SendPropertyChanging();
					this._idaDr = value;
					this.SendPropertyChanged("IdaDr");
					this.OnIdaDrChanged();
				}
			}
		}
		
		[Column(Storage="_idbOrn", Name="ID_BORN", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDBorn
		{
			get
			{
				return this._idbOrn;
			}
			set
			{
				if ((_idbOrn != value))
				{
					this.OnIDBornChanging(value);
					this.SendPropertyChanging();
					this._idbOrn = value;
					this.SendPropertyChanged("IDBorn");
					this.OnIDBornChanged();
				}
			}
		}
		
		[Column(Storage="_idclIen", Name="ID_CLIEN", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDCLien
		{
			get
			{
				return this._idclIen;
			}
			set
			{
				if ((_idclIen != value))
				{
					this.OnIDCLienChanging(value);
					this.SendPropertyChanging();
					this._idclIen = value;
					this.SendPropertyChanged("IDCLien");
					this.OnIDCLienChanged();
				}
			}
		}
		
		[Column(Storage="_num", Name="NUM", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Num
		{
			get
			{
				return this._num;
			}
			set
			{
				if ((_num != value))
				{
					this.OnNumChanging(value);
					this.SendPropertyChanging();
					this._num = value;
					this.SendPropertyChanged("Num");
					this.OnNumChanged();
				}
			}
		}
		
		[Column(Storage="_statUt", Name="STATUT", DbType="varchar(20)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string StatUt
		{
			get
			{
				return this._statUt;
			}
			set
			{
				if (((_statUt == value) 
							== false))
				{
					this.OnStatUtChanging(value);
					this.SendPropertyChanging();
					this._statUt = value;
					this.SendPropertyChanged("StatUt");
					this.OnStatUtChanged();
				}
			}
		}
		
		[Column(Storage="_total", Name="TOTAL", DbType="float", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float Total
		{
			get
			{
				return this._total;
			}
			set
			{
				if ((_total != value))
				{
					this.OnTotalChanging(value);
					this.SendPropertyChanging();
					this._total = value;
					this.SendPropertyChanged("Total");
					this.OnTotalChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_menu", OtherKey="NumCom", ThisKey="Num", Name="commande_du_menu")]
		[DebuggerNonUserCode()]
		public EntitySet<Menu> Menu
		{
			get
			{
				return this._menu;
			}
			set
			{
				this._menu = value;
			}
		}
		
		[Association(Storage="_prodUiTsCommandE", OtherKey="NumCom", ThisKey="Num", Name="commande_produits")]
		[DebuggerNonUserCode()]
		public EntitySet<ProdUItsCommandE> ProdUItsCommandE
		{
			get
			{
				return this._prodUiTsCommandE;
			}
			set
			{
				this._prodUiTsCommandE = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_adRessE", OtherKey="IdaDr", ThisKey="IdaDr", Name="adresse_de_commande", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public ADressE ADressE
		{
			get
			{
				return this._adRessE.Entity;
			}
			set
			{
				if (((this._adRessE.Entity == value) 
							== false))
				{
					if ((this._adRessE.Entity != null))
					{
						ADressE previousADressE = this._adRessE.Entity;
						this._adRessE.Entity = null;
						previousADressE.CommandE.Remove(this);
					}
					this._adRessE.Entity = value;
					if ((value != null))
					{
						value.CommandE.Add(this);
						_idaDr = value.IdaDr;
					}
					else
					{
						_idaDr = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_borne", OtherKey="IDBorn", ThisKey="IDBorn", Name="borne_qui_commande", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Borne Borne
		{
			get
			{
				return this._borne.Entity;
			}
			set
			{
				if (((this._borne.Entity == value) 
							== false))
				{
					if ((this._borne.Entity != null))
					{
						Borne previousBorne = this._borne.Entity;
						this._borne.Entity = null;
						previousBorne.CommandE.Remove(this);
					}
					this._borne.Entity = value;
					if ((value != null))
					{
						value.CommandE.Add(this);
						_idbOrn = value.IDBorn;
					}
					else
					{
						_idbOrn = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_client", OtherKey="IDCLien", ThisKey="IDCLien", Name="client_qui_commande", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Client Client
		{
			get
			{
				return this._client.Entity;
			}
			set
			{
				if (((this._client.Entity == value) 
							== false))
				{
					if ((this._client.Entity != null))
					{
						Client previousClient = this._client.Entity;
						this._client.Entity = null;
						previousClient.CommandE.Remove(this);
					}
					this._client.Entity = value;
					if ((value != null))
					{
						value.CommandE.Add(this);
						_idclIen = value.IDCLien;
					}
					else
					{
						_idclIen = default(int);
					}
				}
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
				h(this, emptyChangingEventArgs);
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
		private void Menu_Attach(Menu entity)
		{
			this.SendPropertyChanging();
			entity.CommandE = this;
		}
		
		private void Menu_Detach(Menu entity)
		{
			this.SendPropertyChanging();
			entity.CommandE = null;
		}
		
		private void ProdUItsCommandE_Attach(ProdUItsCommandE entity)
		{
			this.SendPropertyChanging();
			entity.CommandE = this;
		}
		
		private void ProdUItsCommandE_Detach(ProdUItsCommandE entity)
		{
			this.SendPropertyChanging();
			entity.CommandE = null;
		}
		#endregion
	}
	
	[Table(Name="denapoli.famille")]
	public partial class FaMillE : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _idfAMil;
		
		private string _imageUrl;
		
		private string _noM;
		
		private EntitySet<ProDuiTComposition> _proDuiTcOmposition;
		
		private EntitySet<ProDuiT> _proDuiT;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnIDFaMilChanged();
		
		partial void OnIDFaMilChanging(int value);
		
		partial void OnImageURLChanged();
		
		partial void OnImageURLChanging(string value);
		
		partial void OnNoMChanged();
		
		partial void OnNoMChanging(string value);
		#endregion
		
		
		public FaMillE()
		{
			_proDuiTcOmposition = new EntitySet<ProDuiTComposition>(new Action<ProDuiTComposition>(this.ProDuiTComposition_Attach), new Action<ProDuiTComposition>(this.ProDuiTComposition_Detach));
			_proDuiT = new EntitySet<ProDuiT>(new Action<ProDuiT>(this.ProDuiT_Attach), new Action<ProDuiT>(this.ProDuiT_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_idfAMil", Name="ID_FAMIL", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDFaMil
		{
			get
			{
				return this._idfAMil;
			}
			set
			{
				if ((_idfAMil != value))
				{
					this.OnIDFaMilChanging(value);
					this.SendPropertyChanging();
					this._idfAMil = value;
					this.SendPropertyChanged("IDFaMil");
					this.OnIDFaMilChanged();
				}
			}
		}
		
		[Column(Storage="_imageUrl", Name="IMAGE_URL", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageURL
		{
			get
			{
				return this._imageUrl;
			}
			set
			{
				if (((_imageUrl == value) 
							== false))
				{
					this.OnImageURLChanging(value);
					this.SendPropertyChanging();
					this._imageUrl = value;
					this.SendPropertyChanged("ImageURL");
					this.OnImageURLChanged();
				}
			}
		}
		
		[Column(Storage="_noM", Name="NOM", DbType="varchar(100)", AutoSync=AutoSync.Never)]
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
					this.OnNoMChanging(value);
					this.SendPropertyChanging();
					this._noM = value;
					this.SendPropertyChanged("NoM");
					this.OnNoMChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_proDuiTcOmposition", OtherKey="IDFaMil", ThisKey="IDFaMil", Name="familles_produit")]
		[DebuggerNonUserCode()]
		public EntitySet<ProDuiTComposition> ProDuiTComposition
		{
			get
			{
				return this._proDuiTcOmposition;
			}
			set
			{
				this._proDuiTcOmposition = value;
			}
		}
		
		[Association(Storage="_proDuiT", OtherKey="IDFaMil", ThisKey="IDFaMil", Name="famille_de_produit")]
		[DebuggerNonUserCode()]
		public EntitySet<ProDuiT> ProDuiT
		{
			get
			{
				return this._proDuiT;
			}
			set
			{
				this._proDuiT = value;
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
				h(this, emptyChangingEventArgs);
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
		private void ProDuiTComposition_Attach(ProDuiTComposition entity)
		{
			this.SendPropertyChanging();
			entity.FaMillE = this;
		}
		
		private void ProDuiTComposition_Detach(ProDuiTComposition entity)
		{
			this.SendPropertyChanging();
			entity.FaMillE = null;
		}
		
		private void ProDuiT_Attach(ProDuiT entity)
		{
			this.SendPropertyChanging();
			entity.FaMillE = this;
		}
		
		private void ProDuiT_Detach(ProDuiT entity)
		{
			this.SendPropertyChanging();
			entity.FaMillE = null;
		}
		#endregion
	}
	
	[Table(Name="denapoli.menu")]
	public partial class Menu : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _idmEnu;
		
		private int _idpRod;
		
		private int _numCom;
		
		private EntitySet<ProdUItsMenu> _prodUiTsMenu;
		
		private EntityRef<CommandE> _commandE = new EntityRef<CommandE>();
		
		private EntityRef<ProDuiT> _proDuiT = new EntityRef<ProDuiT>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnIDMenuChanged();
		
		partial void OnIDMenuChanging(int value);
		
		partial void OnIDProdChanged();
		
		partial void OnIDProdChanging(int value);
		
		partial void OnNumComChanged();
		
		partial void OnNumComChanging(int value);
		#endregion
		
		
		public Menu()
		{
			_prodUiTsMenu = new EntitySet<ProdUItsMenu>(new Action<ProdUItsMenu>(this.ProdUItsMenu_Attach), new Action<ProdUItsMenu>(this.ProdUItsMenu_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_idmEnu", Name="ID_MENU", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDMenu
		{
			get
			{
				return this._idmEnu;
			}
			set
			{
				if ((_idmEnu != value))
				{
					this.OnIDMenuChanging(value);
					this.SendPropertyChanging();
					this._idmEnu = value;
					this.SendPropertyChanged("IDMenu");
					this.OnIDMenuChanged();
				}
			}
		}
		
		[Column(Storage="_idpRod", Name="ID_PROD", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDProd
		{
			get
			{
				return this._idpRod;
			}
			set
			{
				if ((_idpRod != value))
				{
					this.OnIDProdChanging(value);
					this.SendPropertyChanging();
					this._idpRod = value;
					this.SendPropertyChanged("IDProd");
					this.OnIDProdChanged();
				}
			}
		}
		
		[Column(Storage="_numCom", Name="NUM_COM", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int NumCom
		{
			get
			{
				return this._numCom;
			}
			set
			{
				if ((_numCom != value))
				{
					this.OnNumComChanging(value);
					this.SendPropertyChanging();
					this._numCom = value;
					this.SendPropertyChanged("NumCom");
					this.OnNumComChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_prodUiTsMenu", OtherKey="IDMenu", ThisKey="IDMenu", Name="menu_produits")]
		[DebuggerNonUserCode()]
		public EntitySet<ProdUItsMenu> ProdUItsMenu
		{
			get
			{
				return this._prodUiTsMenu;
			}
			set
			{
				this._prodUiTsMenu = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_commandE", OtherKey="Num", ThisKey="NumCom", Name="commande_du_menu", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public CommandE CommandE
		{
			get
			{
				return this._commandE.Entity;
			}
			set
			{
				if (((this._commandE.Entity == value) 
							== false))
				{
					if ((this._commandE.Entity != null))
					{
						CommandE previousCommandE = this._commandE.Entity;
						this._commandE.Entity = null;
						previousCommandE.Menu.Remove(this);
					}
					this._commandE.Entity = value;
					if ((value != null))
					{
						value.Menu.Add(this);
						_numCom = value.Num;
					}
					else
					{
						_numCom = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_proDuiT", OtherKey="IDProd", ThisKey="IDProd", Name="menu", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public ProDuiT ProDuiT
		{
			get
			{
				return this._proDuiT.Entity;
			}
			set
			{
				if (((this._proDuiT.Entity == value) 
							== false))
				{
					if ((this._proDuiT.Entity != null))
					{
						ProDuiT previousProDuiT = this._proDuiT.Entity;
						this._proDuiT.Entity = null;
						previousProDuiT.Menu.Remove(this);
					}
					this._proDuiT.Entity = value;
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
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
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
		private void ProdUItsMenu_Attach(ProdUItsMenu entity)
		{
			this.SendPropertyChanging();
			entity.Menu = this;
		}
		
		private void ProdUItsMenu_Detach(ProdUItsMenu entity)
		{
			this.SendPropertyChanging();
			entity.Menu = null;
		}
		#endregion
	}
	
	[Table(Name="denapoli.produit")]
	public partial class ProDuiT : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _description;
		
		private int _idfAMil;
		
		private int _idpRod;
		
		private string _imageUrl;
		
		private string _noM;
		
		private float _prIX;
		
		private EntitySet<Menu> _menu;
		
		private EntitySet<ProdUItsCommandE> _prodUiTsCommandE;
		
		private EntitySet<ProDuiTComposition> _proDuiTcOmposition;
		
		private EntitySet<ProdUItsMenu> _prodUiTsMenu;
		
		private EntityRef<FaMillE> _faMillE = new EntityRef<FaMillE>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnIDFaMilChanged();
		
		partial void OnIDFaMilChanging(int value);
		
		partial void OnIDProdChanged();
		
		partial void OnIDProdChanging(int value);
		
		partial void OnImageURLChanged();
		
		partial void OnImageURLChanging(string value);
		
		partial void OnNoMChanged();
		
		partial void OnNoMChanging(string value);
		
		partial void OnPRiXChanged();
		
		partial void OnPRiXChanging(float value);
		#endregion
		
		
		public ProDuiT()
		{
			_menu = new EntitySet<Menu>(new Action<Menu>(this.Menu_Attach), new Action<Menu>(this.Menu_Detach));
			_prodUiTsCommandE = new EntitySet<ProdUItsCommandE>(new Action<ProdUItsCommandE>(this.ProdUItsCommandE_Attach), new Action<ProdUItsCommandE>(this.ProdUItsCommandE_Detach));
			_proDuiTcOmposition = new EntitySet<ProDuiTComposition>(new Action<ProDuiTComposition>(this.ProDuiTComposition_Attach), new Action<ProDuiTComposition>(this.ProDuiTComposition_Detach));
			_prodUiTsMenu = new EntitySet<ProdUItsMenu>(new Action<ProdUItsMenu>(this.ProdUItsMenu_Attach), new Action<ProdUItsMenu>(this.ProdUItsMenu_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_description", Name="DESCRIPTION", DbType="varchar(200)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (((_description == value) 
							== false))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_idfAMil", Name="ID_FAMIL", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDFaMil
		{
			get
			{
				return this._idfAMil;
			}
			set
			{
				if ((_idfAMil != value))
				{
					this.OnIDFaMilChanging(value);
					this.SendPropertyChanging();
					this._idfAMil = value;
					this.SendPropertyChanged("IDFaMil");
					this.OnIDFaMilChanged();
				}
			}
		}
		
		[Column(Storage="_idpRod", Name="ID_PROD", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDProd
		{
			get
			{
				return this._idpRod;
			}
			set
			{
				if ((_idpRod != value))
				{
					this.OnIDProdChanging(value);
					this.SendPropertyChanging();
					this._idpRod = value;
					this.SendPropertyChanged("IDProd");
					this.OnIDProdChanged();
				}
			}
		}
		
		[Column(Storage="_imageUrl", Name="IMAGE_URL", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageURL
		{
			get
			{
				return this._imageUrl;
			}
			set
			{
				if (((_imageUrl == value) 
							== false))
				{
					this.OnImageURLChanging(value);
					this.SendPropertyChanging();
					this._imageUrl = value;
					this.SendPropertyChanged("ImageURL");
					this.OnImageURLChanged();
				}
			}
		}
		
		[Column(Storage="_noM", Name="NOM", DbType="varchar(50)", AutoSync=AutoSync.Never)]
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
					this.OnNoMChanging(value);
					this.SendPropertyChanging();
					this._noM = value;
					this.SendPropertyChanged("NoM");
					this.OnNoMChanged();
				}
			}
		}
		
		[Column(Storage="_prIX", Name="PRIX", DbType="float", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public float PRiX
		{
			get
			{
				return this._prIX;
			}
			set
			{
				if ((_prIX != value))
				{
					this.OnPRiXChanging(value);
					this.SendPropertyChanging();
					this._prIX = value;
					this.SendPropertyChanged("PRiX");
					this.OnPRiXChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_menu", OtherKey="IDProd", ThisKey="IDProd", Name="menu")]
		[DebuggerNonUserCode()]
		public EntitySet<Menu> Menu
		{
			get
			{
				return this._menu;
			}
			set
			{
				this._menu = value;
			}
		}
		
		[Association(Storage="_prodUiTsCommandE", OtherKey="IDProd", ThisKey="IDProd", Name="produits_commande")]
		[DebuggerNonUserCode()]
		public EntitySet<ProdUItsCommandE> ProdUItsCommandE
		{
			get
			{
				return this._prodUiTsCommandE;
			}
			set
			{
				this._prodUiTsCommandE = value;
			}
		}
		
		[Association(Storage="_proDuiTcOmposition", OtherKey="IDProd", ThisKey="IDProd", Name="produits_famille")]
		[DebuggerNonUserCode()]
		public EntitySet<ProDuiTComposition> ProDuiTComposition
		{
			get
			{
				return this._proDuiTcOmposition;
			}
			set
			{
				this._proDuiTcOmposition = value;
			}
		}
		
		[Association(Storage="_prodUiTsMenu", OtherKey="IDProd", ThisKey="IDProd", Name="produits_menu")]
		[DebuggerNonUserCode()]
		public EntitySet<ProdUItsMenu> ProdUItsMenu
		{
			get
			{
				return this._prodUiTsMenu;
			}
			set
			{
				this._prodUiTsMenu = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_faMillE", OtherKey="IDFaMil", ThisKey="IDFaMil", Name="famille_de_produit", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public FaMillE FaMillE
		{
			get
			{
				return this._faMillE.Entity;
			}
			set
			{
				if (((this._faMillE.Entity == value) 
							== false))
				{
					if ((this._faMillE.Entity != null))
					{
						FaMillE previousFaMillE = this._faMillE.Entity;
						this._faMillE.Entity = null;
						previousFaMillE.ProDuiT.Remove(this);
					}
					this._faMillE.Entity = value;
					if ((value != null))
					{
						value.ProDuiT.Add(this);
						_idfAMil = value.IDFaMil;
					}
					else
					{
						_idfAMil = default(int);
					}
				}
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
				h(this, emptyChangingEventArgs);
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
		private void Menu_Attach(Menu entity)
		{
			this.SendPropertyChanging();
			entity.ProDuiT = this;
		}
		
		private void Menu_Detach(Menu entity)
		{
			this.SendPropertyChanging();
			entity.ProDuiT = null;
		}
		
		private void ProdUItsCommandE_Attach(ProdUItsCommandE entity)
		{
			this.SendPropertyChanging();
			entity.ProDuiT = this;
		}
		
		private void ProdUItsCommandE_Detach(ProdUItsCommandE entity)
		{
			this.SendPropertyChanging();
			entity.ProDuiT = null;
		}
		
		private void ProDuiTComposition_Attach(ProDuiTComposition entity)
		{
			this.SendPropertyChanging();
			entity.ProDuiT = this;
		}
		
		private void ProDuiTComposition_Detach(ProDuiTComposition entity)
		{
			this.SendPropertyChanging();
			entity.ProDuiT = null;
		}
		
		private void ProdUItsMenu_Attach(ProdUItsMenu entity)
		{
			this.SendPropertyChanging();
			entity.ProDuiT = this;
		}
		
		private void ProdUItsMenu_Detach(ProdUItsMenu entity)
		{
			this.SendPropertyChanging();
			entity.ProDuiT = null;
		}
		#endregion
	}
	
	[Table(Name="denapoli.produit_composition")]
	public partial class ProDuiTComposition : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _idfAMil;
		
		private int _idpRod;
		
		private System.Nullable<int> _quaNtItE;
		
		private EntityRef<FaMillE> _faMillE = new EntityRef<FaMillE>();
		
		private EntityRef<ProDuiT> _proDuiT = new EntityRef<ProDuiT>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnIDFaMilChanged();
		
		partial void OnIDFaMilChanging(int value);
		
		partial void OnIDProdChanged();
		
		partial void OnIDProdChanging(int value);
		
		partial void OnQuaNtItEChanged();
		
		partial void OnQuaNtItEChanging(System.Nullable<int> value);
		#endregion
		
		
		public ProDuiTComposition()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_idfAMil", Name="ID_FAMIL", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDFaMil
		{
			get
			{
				return this._idfAMil;
			}
			set
			{
				if ((_idfAMil != value))
				{
					this.OnIDFaMilChanging(value);
					this.SendPropertyChanging();
					this._idfAMil = value;
					this.SendPropertyChanged("IDFaMil");
					this.OnIDFaMilChanged();
				}
			}
		}
		
		[Column(Storage="_idpRod", Name="ID_PROD", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDProd
		{
			get
			{
				return this._idpRod;
			}
			set
			{
				if ((_idpRod != value))
				{
					this.OnIDProdChanging(value);
					this.SendPropertyChanging();
					this._idpRod = value;
					this.SendPropertyChanged("IDProd");
					this.OnIDProdChanged();
				}
			}
		}
		
		[Column(Storage="_quaNtItE", Name="QUANTITE", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> QuaNtItE
		{
			get
			{
				return this._quaNtItE;
			}
			set
			{
				if ((_quaNtItE != value))
				{
					this.OnQuaNtItEChanging(value);
					this.SendPropertyChanging();
					this._quaNtItE = value;
					this.SendPropertyChanged("QuaNtItE");
					this.OnQuaNtItEChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_faMillE", OtherKey="IDFaMil", ThisKey="IDFaMil", Name="familles_produit", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public FaMillE FaMillE
		{
			get
			{
				return this._faMillE.Entity;
			}
			set
			{
				if (((this._faMillE.Entity == value) 
							== false))
				{
					if ((this._faMillE.Entity != null))
					{
						FaMillE previousFaMillE = this._faMillE.Entity;
						this._faMillE.Entity = null;
						previousFaMillE.ProDuiTComposition.Remove(this);
					}
					this._faMillE.Entity = value;
					if ((value != null))
					{
						value.ProDuiTComposition.Add(this);
						_idfAMil = value.IDFaMil;
					}
					else
					{
						_idfAMil = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_proDuiT", OtherKey="IDProd", ThisKey="IDProd", Name="produits_famille", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public ProDuiT ProDuiT
		{
			get
			{
				return this._proDuiT.Entity;
			}
			set
			{
				if (((this._proDuiT.Entity == value) 
							== false))
				{
					if ((this._proDuiT.Entity != null))
					{
						ProDuiT previousProDuiT = this._proDuiT.Entity;
						this._proDuiT.Entity = null;
						previousProDuiT.ProDuiTComposition.Remove(this);
					}
					this._proDuiT.Entity = value;
					if ((value != null))
					{
						value.ProDuiTComposition.Add(this);
						_idpRod = value.IDProd;
					}
					else
					{
						_idpRod = default(int);
					}
				}
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
				h(this, emptyChangingEventArgs);
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
	}
	
	[Table(Name="denapoli.produits_commande")]
	public partial class ProdUItsCommandE : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _idpRod;
		
		private int _numCom;
		
		private int _quaNtItE;
		
		private EntityRef<CommandE> _commandE = new EntityRef<CommandE>();
		
		private EntityRef<ProDuiT> _proDuiT = new EntityRef<ProDuiT>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnIDProdChanged();
		
		partial void OnIDProdChanging(int value);
		
		partial void OnNumComChanged();
		
		partial void OnNumComChanging(int value);
		
		partial void OnQuaNtItEChanged();
		
		partial void OnQuaNtItEChanging(int value);
		#endregion
		
		
		public ProdUItsCommandE()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_idpRod", Name="ID_PROD", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDProd
		{
			get
			{
				return this._idpRod;
			}
			set
			{
				if ((_idpRod != value))
				{
					this.OnIDProdChanging(value);
					this.SendPropertyChanging();
					this._idpRod = value;
					this.SendPropertyChanged("IDProd");
					this.OnIDProdChanged();
				}
			}
		}
		
		[Column(Storage="_numCom", Name="NUM_COM", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int NumCom
		{
			get
			{
				return this._numCom;
			}
			set
			{
				if ((_numCom != value))
				{
					this.OnNumComChanging(value);
					this.SendPropertyChanging();
					this._numCom = value;
					this.SendPropertyChanged("NumCom");
					this.OnNumComChanged();
				}
			}
		}
		
		[Column(Storage="_quaNtItE", Name="QUANTITE", DbType="int(6)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int QuaNtItE
		{
			get
			{
				return this._quaNtItE;
			}
			set
			{
				if ((_quaNtItE != value))
				{
					this.OnQuaNtItEChanging(value);
					this.SendPropertyChanging();
					this._quaNtItE = value;
					this.SendPropertyChanged("QuaNtItE");
					this.OnQuaNtItEChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_commandE", OtherKey="Num", ThisKey="NumCom", Name="commande_produits", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public CommandE CommandE
		{
			get
			{
				return this._commandE.Entity;
			}
			set
			{
				if (((this._commandE.Entity == value) 
							== false))
				{
					if ((this._commandE.Entity != null))
					{
						CommandE previousCommandE = this._commandE.Entity;
						this._commandE.Entity = null;
						previousCommandE.ProdUItsCommandE.Remove(this);
					}
					this._commandE.Entity = value;
					if ((value != null))
					{
						value.ProdUItsCommandE.Add(this);
						_numCom = value.Num;
					}
					else
					{
						_numCom = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_proDuiT", OtherKey="IDProd", ThisKey="IDProd", Name="produits_commande", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public ProDuiT ProDuiT
		{
			get
			{
				return this._proDuiT.Entity;
			}
			set
			{
				if (((this._proDuiT.Entity == value) 
							== false))
				{
					if ((this._proDuiT.Entity != null))
					{
						ProDuiT previousProDuiT = this._proDuiT.Entity;
						this._proDuiT.Entity = null;
						previousProDuiT.ProdUItsCommandE.Remove(this);
					}
					this._proDuiT.Entity = value;
					if ((value != null))
					{
						value.ProdUItsCommandE.Add(this);
						_idpRod = value.IDProd;
					}
					else
					{
						_idpRod = default(int);
					}
				}
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
				h(this, emptyChangingEventArgs);
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
	}
	
	[Table(Name="denapoli.produits_menu")]
	public partial class ProdUItsMenu : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _idmEnu;
		
		private int _idpRod;
		
		private int _quaNtItE;
		
		private EntityRef<Menu> _menu = new EntityRef<Menu>();
		
		private EntityRef<ProDuiT> _proDuiT = new EntityRef<ProDuiT>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnIDMenuChanged();
		
		partial void OnIDMenuChanging(int value);
		
		partial void OnIDProdChanged();
		
		partial void OnIDProdChanging(int value);
		
		partial void OnQuaNtItEChanged();
		
		partial void OnQuaNtItEChanging(int value);
		#endregion
		
		
		public ProdUItsMenu()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_idmEnu", Name="ID_MENU", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDMenu
		{
			get
			{
				return this._idmEnu;
			}
			set
			{
				if ((_idmEnu != value))
				{
					this.OnIDMenuChanging(value);
					this.SendPropertyChanging();
					this._idmEnu = value;
					this.SendPropertyChanged("IDMenu");
					this.OnIDMenuChanged();
				}
			}
		}
		
		[Column(Storage="_idpRod", Name="ID_PROD", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IDProd
		{
			get
			{
				return this._idpRod;
			}
			set
			{
				if ((_idpRod != value))
				{
					this.OnIDProdChanging(value);
					this.SendPropertyChanging();
					this._idpRod = value;
					this.SendPropertyChanged("IDProd");
					this.OnIDProdChanged();
				}
			}
		}
		
		[Column(Storage="_quaNtItE", Name="QUANTITE", DbType="int(6)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int QuaNtItE
		{
			get
			{
				return this._quaNtItE;
			}
			set
			{
				if ((_quaNtItE != value))
				{
					this.OnQuaNtItEChanging(value);
					this.SendPropertyChanging();
					this._quaNtItE = value;
					this.SendPropertyChanged("QuaNtItE");
					this.OnQuaNtItEChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_menu", OtherKey="IDMenu", ThisKey="IDMenu", Name="menu_produits", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Menu Menu
		{
			get
			{
				return this._menu.Entity;
			}
			set
			{
				if (((this._menu.Entity == value) 
							== false))
				{
					if ((this._menu.Entity != null))
					{
						Menu previousMenu = this._menu.Entity;
						this._menu.Entity = null;
						previousMenu.ProdUItsMenu.Remove(this);
					}
					this._menu.Entity = value;
					if ((value != null))
					{
						value.ProdUItsMenu.Add(this);
						_idmEnu = value.IDMenu;
					}
					else
					{
						_idmEnu = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_proDuiT", OtherKey="IDProd", ThisKey="IDProd", Name="produits_menu", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public ProDuiT ProDuiT
		{
			get
			{
				return this._proDuiT.Entity;
			}
			set
			{
				if (((this._proDuiT.Entity == value) 
							== false))
				{
					if ((this._proDuiT.Entity != null))
					{
						ProDuiT previousProDuiT = this._proDuiT.Entity;
						this._proDuiT.Entity = null;
						previousProDuiT.ProdUItsMenu.Remove(this);
					}
					this._proDuiT.Entity = value;
					if ((value != null))
					{
						value.ProdUItsMenu.Add(this);
						_idpRod = value.IDProd;
					}
					else
					{
						_idpRod = default(int);
					}
				}
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
				h(this, emptyChangingEventArgs);
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
	}
}
