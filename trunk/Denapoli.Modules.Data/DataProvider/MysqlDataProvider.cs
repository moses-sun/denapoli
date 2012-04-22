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
           
        }

        private void Connect()
        {
            var connStr = String.Format("server={0};user id={1}; password={2}; database={3}", "localhost", "root", "", "denapoli");
            DAO = new DenapoliDTO(new MySqlConnection(connStr));
        }

        private DenapoliDTO DAO { get; set; }

        public List<Famille> GetAvailableFamilies()
        {
            Connect();
            return new List<Famille>( DAO.Famille);
        }

        public List<Produit> GetFamilyProducts(Famille famille)
        {
            Connect();
            return new List<Produit>(famille.Produits);
        }

        public List<Famille> GetMenuComposition(Produit menu)
        {
            Connect();
            var list = new List<Famille>();
            menu.ProduitComposition.Select(item => item.Famille).ForEach(list.Add);
            return list;
        }

        public List<Commande> GetMenuAllCommandes()
        {
            Connect();
            var list = new List<Commande>();
            DAO.Commande.ForEach(list.Add);
            return list;
        }

        public List<Commande> GetMenuTodayCommandes()
        {
            Connect();
            var list = new List<Commande>();
            DAO.Commande.ForEach(list.Add);
            return list;
        }

        public void AddCommande(Commande com)
        {
            DAO.Commande.InsertOnSubmit(com);
            DAO.SubmitChanges();
        }

        public Client InsertIfNotExists(Client client)
        {
            var c = DAO.Client.FirstOrDefault(item =>item.Nom == client.Nom && item.Prenom == client.Prenom);
            if(c==null)
            {
                DAO.Client.InsertOnSubmit(client);
                DAO.SubmitChanges();
                c = DAO.Client.FirstOrDefault(item =>item.Nom == client.Nom && item.Prenom == client.Prenom);
            }
            c.Email = client.Email;
            c.Tel = client.Tel;
            DAO.SubmitChanges();
            return c;
        }

        public Adresse InsertIfNotExists(Adresse addr)
        {
            Connect();
            DAO.Adresse.InsertOnSubmit(addr);
            DAO.SubmitChanges();
            Connect();
            return DAO.Adresse.FirstOrDefault(item => 
                   item.Num == addr.Num
                && item.Voie == addr.Voie
                && item.Ville == addr.Ville
                && item.Complement == addr.Complement);
        }

        public Borne GetBorne(int id)
        {
            Connect();
            return DAO.Borne.FirstOrDefault(item => item.IDBorn == id);
        }
    }
}