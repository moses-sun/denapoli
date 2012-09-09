using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Timers;
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
            var timer = new Timer {Interval = 6000};
            timer.Elapsed += (sender, args) => Connect();
            timer.Enabled = true;
            timer.Start();
        }

        public void Connect()
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

        public List<ProduitComposition> GetMenuComposition(Produit menu)
        {
            var list = new List<ProduitComposition>();
            menu.ProduitComposition.ForEach(list.Add);
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

        public Commande AddCommande(Commande com)
        {
            Connect();
            if (com.Num == 0)
                DAO.Commande.InsertOnSubmit(com);
            else
            {
                var command = DAO.Commande.First(item => item.Num == com.Num);
                command.Statut = com.Statut;
                command.IDLiVReUR = com.IDLiVReUR;
                com = command;
            }
            DAO.SubmitChanges();
            return com;
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
                var langue = DAO.Langue.FirstOrDefault(item => item.IDLang == l.IDLang);
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
                var add = DAO.Adresse.FirstOrDefault(item => item.IdaDr == addr.IdaDr && item.NumCHamBRe == addr.NumCHamBRe);
                if (add == null)
                {
                    var a = new Adresse{
                         IdaDr = 0,
                         Num = addr.Num,
                         Voie = addr.Voie,
                         Ville = addr.Ville,
                         CP = addr.CP,
                         Complement = addr.Complement,
                         NumCHamBRe = addr.NumCHamBRe
                    };
                    DAO.Adresse.InsertOnSubmit(a);
                }
                else
                {
                    add.Num = addr.Num;
                    add.Voie = addr.Voie;
                    add.Ville = addr.Ville;
                    add.CP = addr.CP;
                    add.Complement = addr.Complement;
                    add.NumCHamBRe = addr.NumCHamBRe;
                }
            }
            DAO.SubmitChanges();
            return addr;
        }

        public Famille InsertIfNotExists(Famille f)
        {
            Connect();
            if (f.IDFaMil == 0)
                DAO.Famille.InsertOnSubmit(f);
            else
            {
                var famille = DAO.Famille.First(item => item.IDFaMil == f.IDFaMil);
                famille.Nom = f.Nom;
                famille.Tva = f.Tva;
                famille.IsaPp = f.IsaPp;
                famille.IsWeb = f.IsWeb;
                famille.IsaCtIf = f.IsaCtIf;
                famille.Description = f.Description;
                famille.ImageURL = f.ImageURL;
            }
            DAO.SubmitChanges();
            return f;
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
                produit.Tva = menu.Tva;
                produit.IsaPp = menu.IsaPp;
                produit.IsWeb = menu.IsWeb;
                produit.IsaCtIf = menu.IsaCtIf;
                produit.Description = menu.Description;
                produit.ImageURL = menu.ImageURL;

                var toremove = new List<ProduitComposition>();
                produit.ProduitComposition.ForEach(item =>
                {
                    var exists = menu.ProduitComposition.FirstOrDefault(e => e.IDFaMil == item.IDFaMil);
                    if (exists == null)
                        toremove.Add(item);
                    else item.Quantite = exists.Quantite;
                });
                toremove.ForEach(e =>
                                     {
                                         /*var r =DAO.ProduitComposition.FirstOrDefault(i => i.IDFaMil == e.IDFaMil && i.IDProd == e.IDProd);
                                         r.Famille.ProduitComposition.De
                                         DAO.ProduitComposition.DeleteOnSubmit(r);

                                       */
                                         produit.ProduitComposition.Remove(e);
                                         e.Famille.ProduitComposition.Remove(e);
                                         DAO.ProduitComposition.DeleteOnSubmit(e);
                                     });
                menu.ProduitComposition.Where(item => produit.ProduitComposition.FirstOrDefault(e => e.IDFaMil == item.IDFaMil) == null)
                    .ForEach(toadd => produit.ProduitComposition.Add(new ProduitComposition
                                                                         {
                                                                             IDProd = produit.IDProd,
                                                                             IDFaMil = toadd.IDFaMil,
                                                                             Quantite = toadd.Quantite,
                                                                             IsMeme = toadd.IsMeme
                                                                         }));

                 produit.ProduitComposition.ForEach(item=>
                                                        {
                                                            var r = menu.ProduitComposition.FirstOrDefault(i => i.IDFaMil == item.IDFaMil && i.IDProd == item.IDProd);
                                                            if (r != null) item.IsMeme = r.IsMeme;
 
                                                        });
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
                borne.HeureOuvertureJour = b.HeureOuvertureJour;
                borne.HeureFermetureJour = b.HeureFermetureJour;
                borne.HeureOuvertureSoir = b.HeureOuvertureSoir;
                borne.HeureFermetureSoir = b.HeureFermetureSoir;
                borne.IsaCtIf = b.IsaCtIf;
                borne.IsoUVert = b.IsoUVert;
                borne.Message = b.Message;
                borne.MessageInActIf = b.MessageInActIf;
            }
            DAO.SubmitChanges();
            return b;
        }

        public void UpdateBornes(List<Borne> bornes)
        {
            Connect();
            foreach (var b in bornes)
            {
                var borne = DAO.Borne.First(item => item.IDBorn == b.IDBorn);
                borne.Adresse = b.Adresse;
                borne.IdaDr = b.IdaDr;
                borne.HeureOuvertureJour = b.HeureOuvertureJour;
                borne.HeureFermetureJour = b.HeureFermetureJour;
                borne.HeureOuvertureSoir = b.HeureOuvertureSoir;
                borne.HeureFermetureSoir = b.HeureFermetureSoir;
                borne.IsaCtIf = b.IsaCtIf;
                borne.IsoUVert = b.IsoUVert;
                borne.Message = b.Message;
                borne.MessageInActIf = b.MessageInActIf;
            }
            DAO.SubmitChanges();
        }


        public void Delete(Client client)
        {
            Connect();
            var c = DAO.Client.FirstOrDefault(item => item.IDCLien == client.IDCLien);
            if (c == null) return;
            foreach (var commande in c.Commandes)
            {
                commande.Client = null;
                commande.IDCLien = 0;
            }
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

            foreach (var borne in a.Borne)
            {
                borne.IdaDr = 0;
                borne.Adresse = null;
            }

            foreach (var commande in a.Commandes)
            {
                commande.IdaDr = 0;
                commande.Adresse = null;
            }

            DAO.Adresse.DeleteOnSubmit(a);
            DAO.SubmitChanges();
        }

        public void Delete(Produit p)
        {
            Connect();
            var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
            if (prod == null) return;
            prod.IDFaMil = 0;

            foreach (var pc in prod.ProduitComposition)
                DAO.ProduitComposition.DeleteOnSubmit(pc);

            foreach (var pc in prod.ProduitsCommande)
                DAO.ProduitsCommande.DeleteOnSubmit(pc);

            foreach (var m in prod.Menu)
                DAO.Menu.DeleteOnSubmit(m);

            foreach (var pm in prod.ProduitsMenu)
                DAO.ProduitsMenu.DeleteOnSubmit(pm);

            DAO.Produit.DeleteOnSubmit(prod);
            DAO.SubmitChanges();
        }

        public void Delete(Famille famille)
        {
            Connect();
            var f = DAO.Famille.FirstOrDefault(item => item.IDFaMil == famille.IDFaMil);
            if (f == null) return;

            foreach (var p in f.Produits.ToList())
            {
                p.Famille = null;
                p.IDFaMil = null;
            }
            DAO.SubmitChanges();

            foreach (var pc in f.ProduitComposition)
                DAO.ProduitComposition.DeleteOnSubmit(pc);

            DAO.Famille.DeleteOnSubmit(f);
            DAO.SubmitChanges();
        }

        public void Delete(Livreur l)
        {
            Connect();
            var c = DAO.Livreur.FirstOrDefault(item => item.IDLiVReUR == l.IDLiVReUR);
            if (c == null) return;

            foreach (var commande in DAO.Commande)
            {
                if (commande.IDLiVReUR == l.IDLiVReUR)
                {
                    commande.IDLiVReUR = null;
                }
            }
            DAO.SubmitChanges();
            DAO.Livreur.DeleteOnSubmit(c);
            DAO.SubmitChanges();
        }

        public void DeleteMenu(Produit p)
        {
            Connect();
            var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
            if (prod == null) return;
            prod.IDFaMil = 0;

            foreach (var pc in prod.ProduitComposition)
                DAO.ProduitComposition.DeleteOnSubmit(pc);

            foreach (var pc in prod.ProduitsCommande)
                DAO.ProduitsCommande.DeleteOnSubmit(pc);

            foreach (var m in prod.Menu)
                DAO.Menu.DeleteOnSubmit(m);

            foreach (var pm in prod.ProduitsMenu)
                DAO.ProduitsMenu.DeleteOnSubmit(pm);

            DAO.Produit.DeleteOnSubmit(prod);
            DAO.SubmitChanges();
        }


        public void Delete(Borne borne)
        {
            Connect();
            var b = DAO.Borne.FirstOrDefault(item => item.IDBorn == borne.IDBorn);
            if (b == null) return;
            b.Adresse = null;
            b.IdaDr = null; 
             foreach (var commande in DAO.Commande)
            {
                if (commande.IDBorn == borne.IDBorn)
                {
                    commande.IDBorn = null;
                }
            }
             DAO.SubmitChanges();
             b = DAO.Borne.FirstOrDefault(item => item.IDBorn == borne.IDBorn);
             DAO.Borne.DeleteOnSubmit(b);
             DAO.SubmitChanges();
        }

        public void Delete(Commande commande)
        {
            Connect();
            var c = DAO.Commande.FirstOrDefault(item => item.Num == commande.Num);
            if (c == null) return;
            c.Borne = null;
            c.IDBorn = 0;
            c.Livreur = null;
            c.IDLiVReUR = 0;
            c.IdaDr = 0;
            c.Adresse = null;
            foreach(var pc in c.ProduitsCommande.ToList() )
            {
                DAO.ProduitsCommande.DeleteOnSubmit(pc);
            }
                
            foreach (var m in c.Menus.ToList())
            {
                foreach (var pm in m.ProduitsMenu.ToList())
                {
                    DAO.ProduitsMenu.DeleteOnSubmit(pm);
                }
                DAO.Menu.DeleteOnSubmit(m);
            }
            DAO.Commande.DeleteOnSubmit(c);
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
                produit.Tva = p.Tva;
                produit.IsaPp = p.IsaPp;
                produit.IsWeb = p.IsWeb;
                produit.IsaCtIf = p.IsaCtIf;
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