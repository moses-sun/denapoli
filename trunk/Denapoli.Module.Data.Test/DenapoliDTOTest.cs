using System;
using System.Linq;
using Denapoli.Modules.Data;
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
                Console.WriteLine(v.Num + " - " + v.Voie+" - "+v.Ville);
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
                Console.WriteLine("Commande num:"+com.Num+" Client:"+com.Client.Nom);
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
    }
}
