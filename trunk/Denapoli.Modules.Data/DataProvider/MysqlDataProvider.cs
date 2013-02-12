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
        }

        private DenapoliDTO DAO { get; set; }

        public List<Famille> GetAvailableFamilies()
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                return new List<Famille>(DAO.Famille.Where(item=>item.IsDeleted==0));
            }
        }

        public List<Produit> GetAllProducts()
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                return new List<Produit>(DAO.Produit.Where(item => item.IsDeleted==0));
            }
        }

        public List<Produit> GetFamilyProducts(Famille famille)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                return new List<Produit>(famille.Produits.Where(item => item.IsDeleted==0));
            }
        }

        public List<ProduitComposition> GetMenuComposition(Produit menu)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                var list = new List<ProduitComposition>();
                menu.ProduitComposition.ForEach(item=>
                                                    {
                                                        if (!item.Produit.IsRemoved)list.Add(item);
                                                    });
                return list;
            }
        }

        public List<Commande> GetMenuAllCommandes()
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                var list = new List<Commande>();
                DAO.Commande.ForEach(item=>
                                         {
                                             if (item.IsDeleted==0) list.Add(item);
                                         });
                return list;
            }
        }

        public List<Livreur> GetAllLivreurs()
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                var list = new List<Livreur>();
                DAO.Livreur.ForEach(item =>
                {
                    if (item.IsDeleted==0) list.Add(item);
                });
                return list;
            }
        }

        public List<Borne> GetAllBornes()
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                var list = new List<Borne>();
                DAO.Borne.ForEach(item=>
                                      {
                                          if (item.IsDeleted==0) list.Add(item);
                                      });
                return list;
            }
        }

        public List<Langue> GetAvailableLanguages()
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                var list = new List<Langue>();
                DAO.Langue.ForEach(item =>
                {
                    if (item.IsDeleted==0) list.Add(item);
                });
                return list;
            }
        }

        public List<Commande> GetMenuTodayCommandes()
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                var list = new List<Commande>();
                DAO.Commande.ForEach(item =>
                {
                    if (item.IsDeleted==0) list.Add(item);
                });
                return list;
            }
        }

        public Commande AddCommande(Commande com)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                if (com.Num == 0)
                    DAO.Commande.InsertOnSubmit(com);
                else
                {
                    var command = DAO.Commande.First(item => item.Num == com.Num);
                    command.Statut = com.Statut;
                    command.IDLiVReUR = com.IDLiVReUR;
                    command.IsRemoved = false;
                    com = command;
                }
                DAO.SubmitChanges();
                return com;
            }
        }

        public Client InsertIfNotExists(Client client)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                var c = DAO.Client.FirstOrDefault(item => item.Nom == client.Nom && item.Prenom == client.Prenom);
                if (c == null)
                {
                    DAO.Client.InsertOnSubmit(client);
                    DAO.SubmitChanges();
                    c = DAO.Client.FirstOrDefault(item => item.Nom == client.Nom && item.Prenom == client.Prenom);
                }
                c.Email = client.Email;
                c.Tel = client.Tel;
                c.IsRemoved = false;
                DAO.SubmitChanges();
                return c;
            }
        }

        public Langue InsertIfNotExists(Langue l)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                if (l.IDLang == 0)
                    DAO.Langue.InsertOnSubmit(l);
                else
                {
                    var langue = DAO.Langue.FirstOrDefault(item => item.IDLang == l.IDLang);
                    langue.NoM = l.NoM;
                    langue.Code = l.Code;
                    langue.IsRemoved = false;
                }
                DAO.SubmitChanges();
                return l;
            }
        }

        public Adresse InsertIfNotExists(Adresse addr)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                if (addr.IdaDr == 0)
                    DAO.Adresse.InsertOnSubmit(addr);
                else
                {
                    var add = DAO.Adresse.FirstOrDefault(item => item.IdaDr == addr.IdaDr && item.NumCHamBRe == addr.NumCHamBRe);
                    if (add == null)
                    {
                        var a = new Adresse
                        {
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
                        add.IsRemoved = false;
                    }
                }
                DAO.SubmitChanges();
                return addr;
            }
        }

        public Famille InsertIfNotExists(Famille f)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
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
                    famille.IsRemoved = false;
                }
                DAO.SubmitChanges();
                return f;
            }
        }

        public Produit InsertMenuIfNotExists(Produit menu)
        {
            if (menu.IDProd == 0)
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var menuFamille = DAO.Famille.First(item => item.Nom == "menus");
                    menu.Famille = menuFamille;
                    DAO.Produit.InsertOnSubmit(menu);
                }
            }
            else
            {
                var toremove = new List<ProduitComposition>();
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var produit = DAO.Produit.First(item => item.IDProd == menu.IDProd);
                    produit.Nom = menu.Nom;
                    produit.Prix = menu.Prix;
                    produit.Tva = menu.Tva;
                    produit.IsaPp = menu.IsaPp;
                    produit.IsWeb = menu.IsWeb;
                    produit.IsaCtIf = menu.IsaCtIf;
                    produit.Description = menu.Description;
                    produit.ImageURL = menu.ImageURL;
                    produit.IsRemoved = false;

                    produit.ProduitComposition.ForEach(item =>
                                                           {
                                                               var exists = menu.ProduitComposition.FirstOrDefault(
                                                                   e => e.IDFaMil == item.IDFaMil);
                                                               if (exists == null)
                                                                   toremove.Add(item);
                                                               else item.Quantite = exists.Quantite;
                                                           });

                    menu.ProduitComposition.Where(
                        item => produit.ProduitComposition.FirstOrDefault(e => e.IDFaMil == item.IDFaMil) == null)
                        .ForEach(toadd => produit.ProduitComposition.Add(new ProduitComposition
                                                                             {
                                                                                 IDProd = produit.IDProd,
                                                                                 IDFaMil = toadd.IDFaMil,
                                                                                 Quantite = toadd.Quantite,
                                                                                 IsMeme = toadd.IsMeme
                                                                             }));

                    produit.ProduitComposition.ForEach(item =>
                                                           {
                                                               var r =
                                                                   menu.ProduitComposition.FirstOrDefault(
                                                                       i =>
                                                                       i.IDFaMil == item.IDFaMil &&
                                                                       i.IDProd == item.IDProd);
                                                               if (r != null) item.IsMeme = r.IsMeme;

                                                           });
                    DAO.SubmitChanges();
                }

                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    DAO.ProduitComposition.ToList().ForEach(item =>
                                                                {
                                                                    if (
                                                                        toremove.Exists(
                                                                            e =>
                                                                            e.IDFaMil == item.IDFaMil &&
                                                                            e.IDProd == item.IDProd))
                                                                    {
                                                                        DAO.ProduitComposition.DeleteOnSubmit(item);
                                                                    }
                                                                });
                    DAO.SubmitChanges();
                }

            }
            return menu;
        }

        public Livreur InsertIfNotExists(Livreur l)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                if (l.IDLiVReUR == 0)
                    DAO.Livreur.InsertOnSubmit(l);
                else
                {
                    var livreur = DAO.Livreur.First(item => item.IDLiVReUR == l.IDLiVReUR);
                    livreur.NoM = l.NoM;
                    livreur.PreNoM = l.PreNoM;
                    livreur.IsRemoved = false;
                }
                DAO.SubmitChanges();
                return l;
            }
        }

        public Borne InsertIfNotExists(Borne b)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                var addr = InsertIfNotExists(b.Adresse);
                b.Adresse = addr;
                b.IdaDr = addr.IdaDr;
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
                    borne.IsRemoved = false;
                }
                DAO.SubmitChanges();
                return b;
            }
        }

        public void UpdateBornes(List<Borne> bornes)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                foreach (var b in bornes)
                {
                    var borne = DAO.Borne.First(item => item.IDBorn == b.IDBorn);
                    borne.Adresse = b.Adresse;
                    borne.IdaDr = b.IdaDr;
                    borne.IsRemoved = b.IsRemoved;
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
        }


        public void Delete(Client client)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var c = DAO.Client.FirstOrDefault(item => item.IDCLien == client.IDCLien);
                    if (c == null) return;
                    DAO.Client.DeleteOnSubmit(c);
                    DAO.SubmitChanges();
                }

            }
            catch
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var c = DAO.Client.FirstOrDefault(item => item.IDCLien == client.IDCLien);
                    if (c == null) return;
                    c.IsRemoved = true;
                    DAO.SubmitChanges();
                }
            }
        }

        public void Delete(Langue langue)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var l = DAO.Langue.FirstOrDefault(item => item.IDLang == langue.IDLang);
                    if (l == null) return;
                    DAO.Langue.DeleteOnSubmit(l);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var l = DAO.Langue.FirstOrDefault(item => item.IDLang == langue.IDLang);
                    if (l == null) return;
                    l.IsRemoved = true;
                    DAO.SubmitChanges();
                }
            }
        }

        public void Delete(Adresse addr)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var a = DAO.Adresse.FirstOrDefault(item => item.IdaDr == addr.IdaDr);
                    if (a == null) return;
                    DAO.Adresse.DeleteOnSubmit(a);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var a = DAO.Adresse.FirstOrDefault(item => item.IdaDr == addr.IdaDr);
                    if (a == null) return;
                    a.IsRemoved = true;
                    DAO.SubmitChanges();
                }
            }
        }

        public void Delete(Produit p)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
                    if (prod == null) return;
                    prod.IsRemoved = true;
                    prod.IDFaMil = 0;
                    foreach (var pc in prod.ProduitComposition)
                        DAO.ProduitComposition.DeleteOnSubmit(pc);

                    foreach (var pc in prod.ProduitsCommande)
                        DAO.ProduitsCommande.DeleteOnSubmit(pc);

                    foreach (var m in prod.Menu)
                        DAO.Menu.DeleteOnSubmit(m);

                    foreach (var pm in prod.ProduitsMenu)
                        DAO.ProduitsMenu.DeleteOnSubmit(pm);
                    DAO.SubmitChanges();
                }
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
                    if (prod == null) return;
                    DAO.Produit.DeleteOnSubmit(prod);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
                    if (prod == null) return;
                    prod.IsRemoved = true;
                    DAO.SubmitChanges();
                }
            }
        }

        public void Delete(Famille famille)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var f = DAO.Famille.FirstOrDefault(item => item.IDFaMil == famille.IDFaMil);
                    if (f == null) return;
                    f.IsRemoved = true;
                    foreach (var p in f.Produits.ToList())
                    {
                        p.Famille = null;
                        p.IDFaMil = null;
                    }

                    foreach (var pc in f.ProduitComposition)
                        DAO.ProduitComposition.DeleteOnSubmit(pc);

                    DAO.SubmitChanges();
                }
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var f = DAO.Famille.FirstOrDefault(item => item.IDFaMil == famille.IDFaMil);
                    if (f == null) return;
                    DAO.Famille.DeleteOnSubmit(f);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var f = DAO.Famille.FirstOrDefault(item => item.IDFaMil == famille.IDFaMil);
                    if (f == null) return;
                    f.IsRemoved = true;
                    DAO.SubmitChanges();
                }
            }
        }

        public void Delete(Livreur l)
        {
                try
                {
                     using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                     {
                          DAO = new DenapoliDTO(mySqlConnection);
                          var c = DAO.Livreur.FirstOrDefault(item => item.IDLiVReUR == l.IDLiVReUR);
                          if (c == null) return;
                          DAO.Livreur.DeleteOnSubmit(c);
                          DAO.SubmitChanges();
                     }
                }
                catch
                {
                    using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                    {
                        DAO = new DenapoliDTO(mySqlConnection);
                        var livreur = DAO.Livreur.FirstOrDefault(item => item.IDLiVReUR == l.IDLiVReUR);
                        if (livreur == null) return;
                        livreur.IsRemoved = true;
                        DAO.SubmitChanges();
                    }
                }
        }

        public void DeleteMenu(Produit p)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
                    if (prod == null) return;
                    prod.IsRemoved = true;
                    prod.IDFaMil = 0;
                    foreach (var pc in prod.ProduitComposition)
                        DAO.ProduitComposition.DeleteOnSubmit(pc);

                    foreach (var pc in prod.ProduitsCommande)
                        DAO.ProduitsCommande.DeleteOnSubmit(pc);

                    foreach (var m in prod.Menu)
                        DAO.Menu.DeleteOnSubmit(m);

                    foreach (var pm in prod.ProduitsMenu)
                        DAO.ProduitsMenu.DeleteOnSubmit(pm);

                    DAO.SubmitChanges();
                }
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
                    if (prod == null) return;
                    DAO.Produit.DeleteOnSubmit(prod);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
                    if (prod == null) return;
                    prod.IsRemoved = true;
                    DAO.SubmitChanges();
                }
            }
        }


        public void Delete(Borne borne)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var b = DAO.Borne.FirstOrDefault(item => item.IDBorn == borne.IDBorn);
                    if (b == null) return;
                    b.IsRemoved = true;
                    b.IdaDr = null;
                    foreach (var commande in DAO.Commande)
                    {
                        commande.IDBorn = null;
                        commande.Borne = null;
                    }
                    b.Commandes.Clear();
                    DAO.SubmitChanges();
                }
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var b = DAO.Borne.FirstOrDefault(item => item.IDBorn == borne.IDBorn);
                    if (b == null) return;
                    DAO.Borne.DeleteOnSubmit(b);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var b = DAO.Borne.FirstOrDefault(item => item.IDBorn == borne.IDBorn);
                    if (b == null) return;
                    b.IsRemoved = true;
                    DAO.SubmitChanges();
                }
            }
        }   

        public void Delete(Commande commande)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var c = DAO.Commande.FirstOrDefault(item => item.Num == commande.Num);
                    if (c == null) return;
                    c.IsRemoved = true;
                    c.Borne = null;
                    c.IDBorn = 0;
                    c.Livreur = null;
                    c.IDLiVReUR = 0;
                    c.IdaDr = 0;
                    c.Adresse = null;
                    c.IsRemoved = true;
                    foreach (var pc in c.ProduitsCommande.ToList())
                        DAO.ProduitsCommande.DeleteOnSubmit(pc);

                    foreach (var m in c.Menus.ToList())
                    {
                        foreach (var pm in m.ProduitsMenu.ToList())
                            DAO.ProduitsMenu.DeleteOnSubmit(pm);
                        DAO.Menu.DeleteOnSubmit(m);
                    }
                    DAO.SubmitChanges();
                }

                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var c = DAO.Commande.FirstOrDefault(item => item.Num == commande.Num);
                    if (c == null) return;
                    DAO.Commande.DeleteOnSubmit(c);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var c = DAO.Commande.FirstOrDefault(item => item.Num == commande.Num);
                    if (c == null) return;
                    c.IsRemoved = true;
                    DAO.SubmitChanges();
                }
            }
        }

        public Livreur GetLivreurById(int idliVreUr)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                return DAO.Livreur.FirstOrDefault(item => item.IsDeleted==0 &&  item.IDLiVReUR == idliVreUr);
            }
        }

        public Borne GetBorne(int id)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
                return DAO.Borne.FirstOrDefault(item => item.IsDeleted==0 && item.IDBorn == id);
            }
        }

        public Produit InsertIfNotExists(Produit p)
        {
            using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
            {
                DAO = new DenapoliDTO(mySqlConnection);
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
                    produit.IsRemoved = false;
                }
                DAO.SubmitChanges();
                p.Famille = DAO.Famille.FirstOrDefault(item => item.IDFaMil == p.IDFaMil);
                return p;
            }
        }
    }
}