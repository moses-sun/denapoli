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
            Connect();
            if (com.Num == 0)
                DAO.Commande.InsertOnSubmit(com);
            else
            {
                var command = DAO.Commande.First(item => item.Num == com.Num);
                command.Statut = com.Statut;
                command.IDLiVReUR = com.IDLiVReUR;
            }
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

        public Langue InsertIfNotExists(Langue l)
        {
            Connect();
            if (l.IDLang == 0)
                DAO.Langue.InsertOnSubmit(l);
            else
            {
                var langue = DAO.Langue.First(item => item.IDLang == l.IDLang);
                langue.NoM = l.NoM;
                langue.Code = l.Code;
            }
            DAO.SubmitChanges();
            return l;
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
            Connect();
            if (p.IDFaMil == 0)
                DAO.Famille.InsertOnSubmit(p);
            else
            {
                var famille = DAO.Famille.First(item => item.IDFaMil == p.IDFaMil);
                famille.Nom = p.Nom;
                famille.Description = p.Description;
                famille.ImageURL = p.ImageURL;
            }
            DAO.SubmitChanges();
            return p;
        }

        public Produit InsertMenuIfNotExists(Produit menu)
        {
            Connect();
            
            if (menu.IDProd == 0)
            {
                var menuFamille = DAO.Famille.First(item => item.Nom == "menus");
                menu.Famille = menuFamille;
                DAO.Produit.InsertOnSubmit(menu);
            }
            else
            {
                var produit = DAO.Produit.First(item => item.IDProd == menu.IDProd);
                produit.Nom = menu.Nom;
                produit.Prix = menu.Prix;
                produit.Description = menu.Description;
                produit.ImageURL = menu.ImageURL;

                var toremove = new List<ProduitComposition>();
                produit.ProduitComposition.ForEach(item =>
                {
                    var exists = menu.ProduitComposition.FirstOrDefault(e => e.IDFaMil == item.IDFaMil);
                    if (exists == null) toremove.Add(item);
                    else item.Quantite = exists.Quantite;
                });
                toremove.ForEach(e =>
                                     {
                                        
                                         var r =DAO.ProduitComposition.FirstOrDefault(i => i.IDFaMil == e.IDFaMil && i.IDProd == e.IDProd);
                                         DAO.ProduitComposition.DeleteOnSubmit(r);
                                     });
                menu.ProduitComposition.Where(item => produit.ProduitComposition.FirstOrDefault(e => e.IDFaMil == item.IDFaMil) == null)
                    .ForEach(toadd => produit.ProduitComposition.Add(new ProduitComposition
                                                                         {
                                                                             IDProd = produit.IDProd,
                                                                             IDFaMil = toadd.IDFaMil,
                                                                             Quantite = toadd.Quantite
                                                                         }));
            }
            DAO.SubmitChanges();
            return menu;
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

        public void Delete(Client client)
        {
            Connect();
            var c = DAO.Client.FirstOrDefault(item => item.IDCLien == client.IDCLien);
            if (c == null) return;
            DAO.Client.DeleteOnSubmit(c);
            DAO.SubmitChanges();
        }

        public void Delete(Langue langue)
        {
            Connect();
            var l = DAO.Langue.FirstOrDefault(item => item.IDLang == langue.IDLang);
            if (l == null) return;
            DAO.Langue.DeleteOnSubmit(l);
            DAO.SubmitChanges();
        }

        public void Delete(Adresse addr)
        {
            Connect();
            var a = DAO.Adresse.FirstOrDefault(item => item.IdaDr == addr.IdaDr);
            if (a == null) return;
            DAO.Adresse.DeleteOnSubmit(a);
            DAO.SubmitChanges();
        }

        public void Delete(Produit p)
        {
            Connect();
            var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
            if (prod == null) return;
            DAO.Produit.DeleteOnSubmit(prod);
            DAO.SubmitChanges();
        }

        public void Delete(Famille famille)
        {
            Connect();
            var f = DAO.Famille.FirstOrDefault(item => item.IDFaMil == famille.IDFaMil);
            if (f == null) return;
            DAO.Famille.DeleteOnSubmit(f);
            DAO.SubmitChanges();
        }

        public void Delete(Livreur l)
        {
            Connect();
            var c = DAO.Livreur.FirstOrDefault(item => item.IDLiVReUR == l.IDLiVReUR);
            if (c == null) return;
            DAO.Livreur.DeleteOnSubmit(c);
            DAO.SubmitChanges();
        }

        public void DeleteMenu(Produit p)
        {
            Connect();
           
            var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
            if (prod == null) return;
            prod.ProduitsMenu.Clear();
            prod.ProduitComposition.Clear();
            prod.ProduitsMenu.Clear();
            DAO.Produit.DeleteOnSubmit(prod);
            DAO.SubmitChanges();
        }

        public void Delete(Borne borne)
        {
            Connect();
            var b = DAO.Borne.FirstOrDefault(item => item.IDBorn == borne.IDBorn);
            if (b == null) return;
            DAO.Borne.DeleteOnSubmit(b);
            DAO.SubmitChanges();
        }

        public Livreur GetLivreurById(int idliVreUr)
        {
            return  DAO.Livreur.FirstOrDefault(item => item.IDLiVReUR == idliVreUr);
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
                produit.Famille = DAO.Famille.FirstOrDefault(item => item.IDFaMil == p.IDFaMil);
                produit.ImageURL = p.ImageURL;
            }
            DAO.SubmitChanges();
            p.Famille = DAO.Famille.FirstOrDefault(item => item.IDFaMil == p.IDFaMil);
            return p;
        }
    }
}