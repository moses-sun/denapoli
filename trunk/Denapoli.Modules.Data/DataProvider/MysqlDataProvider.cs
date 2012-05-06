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
        public ISettingsService SettingsService { get; set; }

        [ImportingConstructor]
        public MysqlDataProvider(ISettingsService  settingsService)
        {
            SettingsService = settingsService;
            Connect();
        }

        private void Connect()
        {
            DAO = new DenapoliDTO(new MySqlConnection(SettingsService.GetDbConnextionParameters()));
        }

        private DenapoliDTO DAO { get; set; }

        public List<Famille> GetAvailableFamilies()
        {
            
            return new List<Famille>( DAO.Famille);
        }

        public List<Produit> GetAllProducts()
        {
            
            return new List<Produit>(DAO.Produit);
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

        public List<Commande> GetMenuAllCommandes()
        {
            
            var list = new List<Commande>();
            DAO.Commande.ForEach(list.Add);
            return list;
        }

        public List<Livreur> GetAllLivreurs()
        {
            var list = new List<Livreur>();
            DAO.Livreur.ForEach(list.Add);
            return list;
        }

        public List<Borne> GetAllBornes()
        {
            var list = new List<Borne>();
            DAO.Borne.ForEach(list.Add);
            return list;
        }

        public List<Langue> GetAvailableLanguages()
        {
            var list = new List<Langue>();
            DAO.Langue.ForEach(list.Add);
            return list;
        }

        public List<Commande> GetMenuTodayCommandes()
        {
            
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
            var add =  DAO.Adresse.FirstOrDefault(item => 
                   item.Num == addr.Num
                && item.Voie == addr.Voie
                && item.Ville == addr.Ville
                && item.Complement == addr.Complement
                && item.NumCHamBRe == addr.NumCHamBRe);
            if(add == null )
            {
                add = new Adresse { 
                    Num = addr.Num,
                    Voie = addr.Voie,
                    Ville = addr.Ville,
                    Complement = addr.Complement,
                    NumCHamBRe = addr.NumCHamBRe};
                DAO.Adresse.InsertOnSubmit(add);
                DAO.SubmitChanges();
                add =  DAO.Adresse.FirstOrDefault(item => 
                   item.Num == addr.Num
                && item.Voie == addr.Voie
                && item.Ville == addr.Ville
                && item.Complement == addr.Complement
                && item.NumCHamBRe == addr.NumCHamBRe);
            }
            return add;
        }

        public Famille InsertIfNotExists(Famille p)
        {
            throw new NotImplementedException();
        }

        public Livreur InsertIfNotExists(Livreur l)
        {
            throw new NotImplementedException();
        }

        public Borne InsertIfNotExists(Borne b)
        {
            throw new NotImplementedException();
        }

        public Borne GetBorne(int id)
        {
          
            return DAO.Borne.FirstOrDefault(item => item.IDBorn == id);
        }

        public Produit InsertIfNotExists(Produit p)
        {
            /*if(p.IDProd == 0)
            {
                DAO.Produit.InsertOnSubmit(p);
            }*/
            DAO.SubmitChanges();
            return p;
        }
    }
}