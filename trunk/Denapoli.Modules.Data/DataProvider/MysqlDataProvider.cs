using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Denapoli.Modules.Data.Entities;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using MySql.Data.MySqlClient;

namespace Denapoli.Modules.Data.DataProvider
{
    [Export(typeof(IDataProvider))]
    public class MysqlDataProvider : IDataProvider
    {
        public MysqlDataProvider()
        {
            var connStr = String.Format("server={0};user id={1}; password={2}; database={3}", "localhost", "root", "", "denapoli");
            DAO = new DenapoliDTO(new MySqlConnection(connStr));
            
        }

        private DenapoliDTO DAO { get; set; }

        public List<Famille> GetAvailableFamilies()
        {
            return new List<Famille>( DAO.Famille);
        }

        public List<Produit> GetFamilyProducts(Famille famille)
        {
            return new List<Produit>(famille.Produits);
        }

        public List<Famille> GetMenuComposition(Produit menu)
        {
            var list = new List<Famille>();
            menu.ProduitComposition.Select(item => item.Famille).ForEach(list.Add);
            return list;
        }
    }
}