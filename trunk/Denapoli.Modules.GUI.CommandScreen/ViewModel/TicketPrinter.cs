using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Windows.Controls;
using Denapoli.Modules.Data.Entities;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class TicketPrinter
    {
        public void Print(Commande commande)
        {
            var doc = new PrintDocument();
            doc.PrintPage += (sender, e) =>
                                 {
                                     var font = new Font("Helvetica", 10);
                                     float x = e.MarginBounds.Left;
                                     float y = e.MarginBounds.Top;
                                     var xQuantite = x;
                                     var xProduit = xQuantite + 30;
                                     var xPrix = xProduit + 200;
                                     float lineHeight = font.GetHeight(e.Graphics);
                                     var nbArticles = 0;

                                     e.Graphics.DrawString("DINAPOLI PIZZA", font, Brushes.Black, xProduit, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("Borne N°" + commande.IDBorn, font, Brushes.Black, xQuantite, y);
                                     y += 2 * lineHeight;
                                     e.Graphics.DrawString("---------------------------------------------------------", font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     foreach (var produit in commande.ProduitsCommande)
                                     {
                                         e.Graphics.DrawString("" + String.Format("{0,2}", produit.Quantite), font, Brushes.Black, xQuantite, y);
                                         e.Graphics.DrawString(produit.Produit.Nom.ToUpper(), font, Brushes.Black, xProduit, y);
                                         e.Graphics.DrawString("" + String.Format("{0,5:##0.00}", (produit.Quantite * produit.Produit.Prix)), font, Brushes.Black, xPrix, y);
                                         y += lineHeight;
                                         nbArticles += produit.Quantite;
                                     }
                                     foreach (var menu in commande.Menus) 
                                     {
                                         e.Graphics.DrawString("" + String.Format("{0,2}", menu.Quantite), font, Brushes.Black, xQuantite, y);
                                         e.Graphics.DrawString(menu.Produit.Nom.ToUpper(), font, Brushes.Black, xProduit, y);
                                         e.Graphics.DrawString("" + String.Format("{0,5:##0.00}", (menu.Produit.Prix * menu.Quantite)), font, Brushes.Black, xPrix, y);
                                         y += lineHeight;
                                         foreach (var comp in menu.ProduitsMenu)
                                         {
                                             e.Graphics.DrawString("" + String.Format("{0,2}", comp.Quantite), font, Brushes.Black, xProduit, y);
                                             e.Graphics.DrawString(comp.Produit.Nom.ToUpper(), font, Brushes.Black, xProduit + 30, y);
                                             y += lineHeight;
                                             nbArticles += comp.Quantite;
                                         }
                                     }
                                     e.Graphics.DrawString("---------------------------------------------------------", font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString(nbArticles + " articles", font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("TOTAL", font, Brushes.Black, xProduit, y);
                                     e.Graphics.DrawString(" : " + commande.Total.ToString("C2"), font, Brushes.Black, xProduit + 60, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("TVA", font, Brushes.Black, xProduit, y);
                                     e.Graphics.DrawString(" : " + commande.Tva.ToString("C2"), font, Brushes.Black, xProduit + 60, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("---------------------------------------------------------", font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("Client : " + commande.Client.Nom + " " + commande.Client.Prenom, font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("Tel : " + commande.Client.Tel, font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("Adresse de livraison : ", font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString(commande.Adresse.Num + " " + commande.Adresse.Voie, font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("Chambre " + commande.Adresse.NumCHamBRe, font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString(commande.Adresse.Complement, font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString(commande.Adresse.CP + " " + commande.Adresse.Ville, font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("---------------------------------------------------------", font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("Livreur : " + (commande.Livreur != null ? commande.Livreur.NoM : ""), font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("---------------------------------------------------------", font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString(commande.Num + "  " + commande.Date.GetValueOrDefault().ToString("HH:mm:ss d/MM/yy"), font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("Votre commande sera livrée ", font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("dans moins de 45min", font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("---------------------------------------------------------", font, Brushes.Black, xQuantite, y);


                                 };

            var dlgSettings = new PrintDialog();
            doc.DefaultPageSettings.Margins.Top = 20;
            doc.DefaultPageSettings.Margins.Bottom = 20;
            doc.DefaultPageSettings.Margins.Left = 20;
            doc.DefaultPageSettings.Margins.Right = 20;
            //dlgSettings.Document = doc;
            doc.Print();
        }
    }

    
}