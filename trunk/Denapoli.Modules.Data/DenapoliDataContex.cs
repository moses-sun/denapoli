using System.Data;
using DbLinq.Data.Linq;
using DbLinq.Vendor;
using System.Data.Linq.Mapping;
using Denapoli.Modules.Data.Entities;

namespace Denapoli.Modules.Data
{
   public class DenapoliDTO : DataContext
	{
		
		public DenapoliDTO(string connectionString) : base(connectionString)
		{
		}
		
		public DenapoliDTO(string connection, MappingSource mappingSource) : base(connection, mappingSource){
		}
		
		public DenapoliDTO(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

        public DenapoliDTO(IDbConnection connection): base(connection, new DbLinq.MySql.MySqlVendor())
        {
        }

        public DenapoliDTO(IDbConnection connection, IVendor sqlDialect): base(connection, sqlDialect)
        {
        }

        public DenapoliDTO(IDbConnection connection, MappingSource mappingSource, IVendor sqlDialect): base(connection, mappingSource, sqlDialect)
        {
        }
		
		public Table<Adresse> Adresse
		{
			get
			{
				return GetTable<Adresse>();
			}
		}

       public Table<Client> Client
		{
			get
			{
				return GetTable<Client>();
			}
		}
		
		public Table<Commande> Commande
		{
			get
			{
				return this.GetTable<Commande>();
			}
		}
		
		public Table<Famille> Famille
		{
			get
			{
				return this.GetTable<Famille>();
			}
		}
		
		public Table<Menu> Menu
		{
			get
			{
				return this.GetTable<Menu>();
			}
		}
		
		public Table<Produit> Produit
		{
			get
			{
				return this.GetTable<Produit>();
			}
		}
		
		public Table<ProduitComposition> ProduitComposition
		{
			get
			{
				return this.GetTable<ProduitComposition>();
			}
		}
		
		public Table<ProduitsCommande> ProduitsCommande
		{
			get
			{
				return this.GetTable<ProduitsCommande>();
			}
		}
		
		public Table<ProduitsMenu> ProduitsMenu
		{
			get
			{
				return GetTable<ProduitsMenu>();
			}
		}
		
       public Table<Borne> Borne
       {
           get { return GetTable<Borne>(); }
       }
	}
}


