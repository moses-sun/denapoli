using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Linq;
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
            Connect();
          
            if(addr.IdaDr == 0 )
                DAO.Adresse.InsertOnSubmit(addr);
            else
            {
                var add = DAO.Adresse.First(item=>item.IdaDr==addr.IdaDr);
                add.Num = addr.Num;
                add.Voie = addr.Voie;
                add.Ville = addr.Ville;
                add.Complement = addr.Complement;
                add.NumCHamBRe = addr.NumCHamBRe;
            }
            DAO.SubmitChanges();
            return addr;
        }

        public Famille InsertIfNotExists(Famille p)
        {
            throw new NotImplementedException();
        }

        public Livreur InsertIfNotExists(Livreur l)
        {
            Connect();
            if (l.IDLiVReUR == 0)
                DAO.Livreur.InsertOnSubmit(l);
            else
            {
                var livreur = DAO.Livreur.First(item => item.IDLiVReUR == l.IDLiVReUR);
                livreur.NoM = l.NoM;
                livreur.PreNoM = l.PreNoM;
            }
            DAO.SubmitChanges();
            return l;
        }

        public Borne InsertIfNotExists(Borne b)
        {
            var addr = InsertIfNotExists(b.Adresse);
            b.Adresse = addr;
            b.IdaDr = addr.IdaDr;
            Connect();
            if (b.IDBorn == 0)
                DAO.Borne.InsertOnSubmit(b);
            else
            {
                var borne = DAO.Borne.First(item => item.IDBorn == b.IDBorn);
                borne.Adresse = b.Adresse;
                borne.IdaDr = b.IdaDr;
            }
            DAO.SubmitChanges();
            return b;
        }

        public Borne GetBorne(int id)
        {
          
            return DAO.Borne.FirstOrDefault(item => item.IDBorn == id);
        }

        public Produit InsertIfNotExists(Produit p)
        {
            Connect();
            if (p.IDProd == 0)
                DAO.Produit.InsertOnSubmit(p);
            else
            {
                var produit = DAO.Produit.First(item => item.IDProd == p.IDProd);
                produit.Nom = p.Nom;
                produit.Prix = p.Prix;
                produit.Description = p.Description;
                produit.IDFaMil = p.IDFaMil;
            }
            DAO.SubmitChanges();
            return p;
        }
    }
}