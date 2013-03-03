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
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    return new List<Famille>(DAO.Famille.Where(item => item.IsDeleted == 0));
                }
            }
            catch (Exception)
            {
                return new List<Famille>();
            }
        }

        public List<Produit> GetAllProducts()
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    return new List<Produit>(DAO.Produit.Where(item => item.IsDeleted == 0));
                }
            }
            catch (Exception)
            {
                return new List<Produit>();
            }
        }

        public List<Produit> GetFamilyProducts(Famille famille)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    return new List<Produit>(famille.Produits.Where(item => item.IsDeleted == 0));
                }
            }
            catch (Exception)
            {
                return  new List<Produit>();
            }
        }

        public List<ProduitComposition> GetMenuComposition(Produit menu)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var list = new List<ProduitComposition>();
                    menu.ProduitComposition.ForEach(item =>
                    {
                        if (!item.Produit.IsRemoved) list.Add(item);
                    });
                    return list;
                }
            }
            catch (Exception)
            {
                return new List<ProduitComposition>();
            }
        }

        public List<Commande> GetMenuAllCommandes()
        {
            try
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
            catch (Exception)
            {
               return new List<Commande>();
            }
        }

        public List<Livreur> GetAllLivreurs()
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var list = new List<Livreur>();
                    DAO.Livreur.ForEach(item =>
                    {
                        if (item.IsDeleted == 0) list.Add(item);
                    });
                    return list;
                }
            }
            catch (Exception)
            {
                return  new List<Livreur>();
            }
        }

        public List<Borne> GetAllBornes()
        {

            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var list = new List<Borne>();
                    DAO.Borne.ForEach(item =>
                    {
                        if (item.IsDeleted == 0) list.Add(item);
                    });
                    return list;
                }
            }
            catch (Exception)
            {
                return new List<Borne>();
            }
        }

        public List<Langue> GetAvailableLanguages()
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var list = new List<Langue>();
                    DAO.Langue.ForEach(item =>
                    {
                        if (item.IsDeleted == 0) list.Add(item);
                    });
                    return list;
                }
            }
            catch (Exception)
            {
                return new List<Langue>();
            }
        }

        public List<Commande> GetMenuTodayCommandes()
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var list = new List<Commande>();
                    DAO.Commande.ForEach(item =>
                    {
                        if (item.IsDeleted == 0) list.Add(item);
                    });
                    return list;
                }
            }
            catch (Exception)
            {
                return new List<Commande>();
            }
           
        }

        public Commande AddCommande(Commande com)
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }

        public Client InsertIfNotExists(Client client)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    if (client.IDCLien == 0)
                        DAO.Client.InsertOnSubmit(client);
                    else
                    {
                        var c = DAO.Client.First(item => item.IDCLien == client.IDCLien);
                        c.Nom = client.Nom;
                        c.Prenom = client.Prenom;
                        c.Email = client.Email;
                        c.Tel = client.Tel;
                    }
                    DAO.SubmitChanges();
                    return client;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Langue InsertIfNotExists(Langue l)
        {

            try
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
            catch (Exception)
            {
                return null;
            }
        }

        public Adresse InsertIfNotExists(Adresse addr)
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }

        public Famille InsertIfNotExists(Famille f)
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }

        public Produit InsertMenuIfNotExists(Produit menu)
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }

        public Livreur InsertIfNotExists(Livreur l)
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }

        public Borne InsertIfNotExists(Borne b)
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateBornes(List<Borne> bornes)
        {
            try
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
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool Delete(Client client)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var c = DAO.Client.FirstOrDefault(item => item.IDCLien == client.IDCLien);
                    if (c == null) return false;
                    DAO.Client.DeleteOnSubmit(c);
                    DAO.SubmitChanges();
                    return true;
                }

            }
            catch
            {
                try
                {
                    using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                    {
                        DAO = new DenapoliDTO(mySqlConnection);
                        var c = DAO.Client.FirstOrDefault(item => item.IDCLien == client.IDCLien);
                        if (c == null) return false;
                        c.IsRemoved = true;
                        DAO.SubmitChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool Delete(Langue langue)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var l = DAO.Langue.FirstOrDefault(item => item.IDLang == langue.IDLang);
                    if (l == null) return false;
                    DAO.Langue.DeleteOnSubmit(l);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                try
                {
                    using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                    {
                        DAO = new DenapoliDTO(mySqlConnection);
                        var l = DAO.Langue.FirstOrDefault(item => item.IDLang == langue.IDLang);
                        if (l == null) return false;
                        l.IsRemoved = true;
                        DAO.SubmitChanges();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Delete(Adresse addr)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var a = DAO.Adresse.FirstOrDefault(item => item.IdaDr == addr.IdaDr);
                    if (a == null) return false;
                    DAO.Adresse.DeleteOnSubmit(a);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                try
                {
                    using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                    {
                        DAO = new DenapoliDTO(mySqlConnection);
                        var a = DAO.Adresse.FirstOrDefault(item => item.IdaDr == addr.IdaDr);
                        if (a == null) return false;
                        a.IsRemoved = true;
                        DAO.SubmitChanges();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Delete(Produit p)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
                    if (prod == null) return false;
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
                    if (prod == null) return false;
                    DAO.Produit.DeleteOnSubmit(prod);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                try
                {
                    using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                    {
                        DAO = new DenapoliDTO(mySqlConnection);
                        var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
                        if (prod == null) return false;
                        prod.IsRemoved = true;
                        DAO.SubmitChanges();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Delete(Famille famille)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var f = DAO.Famille.FirstOrDefault(item => item.IDFaMil == famille.IDFaMil);
                    if (f == null) return false;
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
                    if (f == null) return false;
                    DAO.Famille.DeleteOnSubmit(f);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                try
                {
                    using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                    {
                        DAO = new DenapoliDTO(mySqlConnection);
                        var f = DAO.Famille.FirstOrDefault(item => item.IDFaMil == famille.IDFaMil);
                        if (f == null) return false;
                        f.IsRemoved = true;
                        DAO.SubmitChanges();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Delete(Livreur l)
        {
                try
                {
                     using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                     {
                          DAO = new DenapoliDTO(mySqlConnection);
                          var c = DAO.Livreur.FirstOrDefault(item => item.IDLiVReUR == l.IDLiVReUR);
                          if (c == null) return false;
                          DAO.Livreur.DeleteOnSubmit(c);
                          DAO.SubmitChanges();
                     }
                }
                catch
                {
                    try
                    {
                        using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                        {
                            DAO = new DenapoliDTO(mySqlConnection);
                            var livreur = DAO.Livreur.FirstOrDefault(item => item.IDLiVReUR == l.IDLiVReUR);
                            if (livreur == null) return false;
                            livreur.IsRemoved = true;
                            DAO.SubmitChanges();
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            return true;
        }

        public bool DeleteMenu(Produit p)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
                    if (prod == null) return false;
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
                    if (prod == null) return false;
                    DAO.Produit.DeleteOnSubmit(prod);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                try
                {
                    using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                    {
                        DAO = new DenapoliDTO(mySqlConnection);
                        var prod = DAO.Produit.FirstOrDefault(item => item.IDProd == p.IDProd);
                        if (prod == null) return false;
                        prod.IsRemoved = true;
                        DAO.SubmitChanges();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }


        public bool Delete(Borne borne)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var b = DAO.Borne.FirstOrDefault(item => item.IDBorn == borne.IDBorn);
                    if (b == null) return false;
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
                    if (b == null) return false;
                    DAO.Borne.DeleteOnSubmit(b);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                try
                {
                    using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                    {
                        DAO = new DenapoliDTO(mySqlConnection);
                        var b = DAO.Borne.FirstOrDefault(item => item.IDBorn == borne.IDBorn);
                        if (b == null) return false;
                        b.IsRemoved = true;
                        DAO.SubmitChanges();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }   

        public bool Delete(Commande commande)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    var c = DAO.Commande.FirstOrDefault(item => item.Num == commande.Num);
                    if (c == null) return false;
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
                    if (c == null) return false;
                    DAO.Commande.DeleteOnSubmit(c);
                    DAO.SubmitChanges();
                }
            }
            catch
            {
                try
                {
                    using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                    {
                        DAO = new DenapoliDTO(mySqlConnection);
                        var c = DAO.Commande.FirstOrDefault(item => item.Num == commande.Num);
                        if (c == null) return false;
                        c.IsRemoved = true;
                        DAO.SubmitChanges();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public Livreur GetLivreurById(int idliVreUr)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    return DAO.Livreur.FirstOrDefault(item => item.IsDeleted == 0 && item.IDLiVReUR == idliVreUr);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Borne GetBorne(int id)
        {
            try
            {
                using (var mySqlConnection = new MySqlConnection(SettingsService.GetDbConnextionParameters()))
                {
                    DAO = new DenapoliDTO(mySqlConnection);
                    return DAO.Borne.FirstOrDefault(item => item.IsDeleted == 0 && item.IDBorn == id);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Produit InsertIfNotExists(Produit p)
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }




    }
}