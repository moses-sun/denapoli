using System;
using System.Linq;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace Denapoli.Module.Data.Test
{
    [TestFixture]
    public class DenapoliDTOTest
    {
        private DenapoliDTO _dto;

        [SetUp]
        public void Setup()
        {
            Console.WriteLine(" env");
            const string dbServer = "localhost";
            var connStr = String.Format("server={0};user id={1}; password={2}; database={3}", dbServer, "root", "", "denapoli");
            Console.WriteLine(" instantiation");
            _dto = new DenapoliDTO(new MySqlConnection(connStr));
        }


        [Test]
        public void ProduitTableTest()
        {
            Console.WriteLine(" req");
            Console.WriteLine("count=" + _dto.Produit.Count());
            foreach (var v in _dto.Produit)
            {
                Console.WriteLine(v.Nom + " - " + v.Famille.Nom + " : " + v.Prix);
                foreach (var composition in v.ProduitComposition)
                {
                    Console.WriteLine("       ="+composition.Famille.Nom);
                }
            }
        }

        [Test]
        public void ClientTableTest()
        {
            Console.WriteLine("count=" + _dto.Client.Count());
            foreach (var v in _dto.Client)
            {
                Console.WriteLine(v.Nom + " - " + v.Prenom);
                foreach(var com in v.Commandes)
                    Console.WriteLine(com.Num);
            }
        }

        [Test]
        public void AdresseTableTest()
        {
            Console.WriteLine("count=" + _dto.Adresse.Count());
            foreach (var v in _dto.Adresse)
            {
                Console.WriteLine(v.Num + " - " + v.Voie+" - "+v.Ville+" numChambre:"+v.NumCHamBRe);
                foreach (var com in v.Commandes)
                    Console.WriteLine("     "+com.Num);
            }
        }

        [Test]
        public void BorneTableTest()
        {
            Console.WriteLine("count=" + _dto.Borne.Count());
            foreach (var v in _dto.Borne)
            {
                Console.WriteLine(v.Adresse.Num + " - " + v.Adresse.Voie + " - " + v.Adresse.Ville);
                foreach (var com in v.Commandes)
                    Console.WriteLine("     " + com.Num);
            }
        }

        [Test]
        public void FamilleTableTest()
        {
            Console.WriteLine("count=" + _dto.Famille.Count());
            foreach (var v in _dto.Famille)
            {
                Console.WriteLine(v.Nom + " - " + v.ImageURL);
                foreach (var com in v.Produits)
                    Console.WriteLine("     " + com.Nom);
            }
        }


        [Test]
        public void LivreurTableTest()
        {
            Console.WriteLine("count=" + _dto.LiVReUR.Count());

            foreach (var v in _dto.LiVReUR)
            {
                Console.WriteLine(v.NoM + " - " + v.PreNoM );
            }
        }

        [Test]
        public void CommandeTableTest()
        {
            Console.WriteLine("count=" + _dto.Commande.Count());
            foreach (var v in _dto.Commande)
            {
                Console.WriteLine("------------------------- ");
                Console.WriteLine(v.Num + " - client=" + v.Client.Nom+" borne="+v.Borne.IDBorn+"  stat="+v.Statut);
                Console.WriteLine("les produits : ");
                foreach (var com in v.ProduitsCommande)
                    Console.WriteLine("     " + com.Produit.Nom);
                Console.WriteLine("les menus : ");
                foreach (var com in v.Menus)
                {
                    Console.WriteLine("     " + com.Produit.Nom);
                    foreach (var prod in com.ProduitsMenu)
                        Console.WriteLine("                   " + prod.Produit.Nom);
                }
            }
        }

        [Test]
        public void MenuTableTest()
        {
            Console.WriteLine("count=" + _dto.Menu.Count());
            foreach (var v in _dto.Menu)
            {
                Console.WriteLine(v.Produit.Nom);
                foreach (var com in v.ProduitsMenu)
                    Console.WriteLine("     " + com.Produit.Famille.Nom+" : " + com.Produit.Nom);
            }
        }

        [Test]
        public void ComplexTest()
        {

            foreach (var com in _dto.Commande)
            {
                Console.WriteLine("Commande num:"+com.Num+" Client:"+com.Client.Nom+" Date:"+com.Date);
                Console.WriteLine("             Addre de livr :"+com.Adresse.Num+" "+com.Adresse.Voie+" Ville="+com.Adresse.Ville);
                Console.WriteLine("             Borne :" + com.Borne.IDBorn + " : " + com.Borne.Adresse.Voie + " Ville=" + com.Borne.Adresse.Ville);
                Console.WriteLine("             List Produits :");
               
                foreach (var prod in com.ProduitsCommande.Where(item=>item.Produit.IDFaMil!=4))
                {
                    Console.WriteLine("                           : "+prod.Produit.Nom+" x "+prod.Quantite+"="+prod.Produit.Prix);
                }
                foreach (var menu in com.Menus)
                {
                    Console.WriteLine("                           : menu :" + menu.Produit.Nom + "  = "+menu.Produit.Prix);
                    foreach (var p in menu.ProduitsMenu)
                    {
                        Console.WriteLine("                                  :" + p.Produit.Nom + "  = " + p.Quantite);
                    }
                }

                
            }
        }

        [Test]
        public void AddCommandeTest()
        {
            var product1 = _dto.Produit.First();
            var product2 = _dto.Produit.Last();
            var borne   = _dto.Borne.First();
            var adresse  = _dto.Adresse.First();
            var client = _dto.Client.First();
            var command = new Commande
                              {
                                  IdaDr = adresse.IdaDr,
                                  IDBorn = borne.IDBorn,
                                  IDCLien = client.IDCLien,
                                  Statut = "ATTENTE"
                              };
            command.ProduitsCommande.Add(new ProduitsCommande{IDProd = product1.IDProd});
            command.ProduitsCommande.Add(new ProduitsCommande{IDProd = product2.IDProd});
            var m = new Menu{IDProd = 9};
            m.ProduitsMenu.Add(new ProduitsMenu { IDProd = product2.IDProd });
            command.Menus.Add(m);
            _dto.Commande.InsertOnSubmit(command);
            _dto.SubmitChanges();

            var com = _dto.Commande.Last();
            Console.WriteLine("Commande num:" + com.Num + " Client:" + com.Client.Nom);
            Console.WriteLine("             Addre de livr :" + com.Adresse.Num + " " + com.Adresse.Voie + " Ville=" + com.Adresse.Ville);
            Console.WriteLine("             Borne :" + com.Borne.IDBorn + " : " + com.Borne.Adresse.Voie + " Ville=" + com.Borne.Adresse.Ville);
            Console.WriteLine("             List Produits :");

            foreach (var prod in com.ProduitsCommande.Where(item => item.Produit.IDFaMil != 4))
            {
                Console.WriteLine("                           : " + prod.Produit.Nom + " x " + prod.Quantite + "=" + prod.Produit.Prix);
            }
            foreach (var menu in com.Menus)
            {
                Console.WriteLine("                           : menu :" + menu.Produit.Nom + "  = " + menu.Produit.Prix);
                foreach (var p in menu.ProduitsMenu)
                {
                    Console.WriteLine("                                  :" + p.Produit.Nom + "  = " + p.Quantite);
                }
            }
        }
    }
}
