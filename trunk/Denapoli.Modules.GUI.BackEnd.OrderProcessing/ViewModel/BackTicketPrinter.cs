using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using Denapoli.Modules.Data.Entities;
using NiiPrinterCLib;
using PrintDialog = System.Windows.Controls.PrintDialog;

namespace Denapoli.Modules.GUI.BackEnd.OrderProcessing.ViewModel
{
    public class BackTicketPrinter
    {

        public BackTicketPrinter()
        {
            Printer = new NIIClassLib();
            PrinterName = "NII ExD NP-K205";
        }

        protected string PrinterName { get; set; }
        protected NIIClassLib Printer { get; set; }

        public void Print(Commande commande)
        {
            Print2(commande);
            MessageBox.Show("finish");
        }
       
        void Print2(Commande commande)
        {
            new TicketPrinter2().Print(commande);
            const int length = 48;
            const int marge = 4;

            long jid = 0;
            var nbArticles = 0;

            if (Printer.NiiStartDoc(PrinterName, out jid) < 0)
                MessageBox.Show("Error");

            var list = new List<string>
                           {
                               Encode("DINAPOLI PIZZA"), 
                               Encode("Borne Num:" + commande.IDBorn), 
                               Spaces(length - 2*marge, '-'), 
                               Encode("  #Euro ")
                           };

            foreach (var produit in commande.ProduitsCommande)
            {
                var str = "" + String.Format("{0,2}", produit.Quantite) + " " +  Encode(produit.Produit.Nom.ToUpper()) + "#" +
                          String.Format("{0,5:##0.00}", (produit.Quantite * produit.Produit.Prix));
                
                list.Add(Encode(str));
                nbArticles += produit.Quantite;
            }
            foreach (var menu in commande.Menus)
            {
                var str = "" + String.Format("{0,2}", menu.Quantite) + " " + menu.Produit.Nom.ToUpper() + "#" + String.Format("{0,5:##0.00}", (menu.Produit.Prix * menu.Quantite));
                list.Add(Encode(str));
                foreach (var comp in menu.ProduitsMenu)
                {
                    list.Add(Encode("    " + String.Format("{0,2}", comp.Quantite) + " " + comp.Produit.Nom.ToUpper()));
                }
                nbArticles += menu.Quantite;
            }

            list.Add(Spaces(length - 2 * marge, '-'));

            list.Add(Encode("                      " + nbArticles + " articles"));
            list.Add(Encode("                      TOTAL : #" +  String.Format("{0,5:##0.00}",commande.Total)));
            list.Add(Encode("                      TVA   : #" +  String.Format("{0,5:##0.00}",commande.Tva)));

            list.Add(Spaces(length - 2 * marge, '-'));

            list.Add(Encode("Client : " + commande.Client.Nom + " " + commande.Client.Prenom));
            list.Add(Encode("Tel : " + commande.Client.Tel));
            list.Add(Encode("Adresse de livraison : "));
            list.Add(Encode("   "+commande.Adresse.Num + " " + commande.Adresse.Voie));
            list.Add(Encode("   Chambre " + commande.Adresse.NumCHamBRe));
            list.Add(Encode("   "+commande.Adresse.Complement));
            list.Add(Encode("   "+commande.Adresse.CP + " " + commande.Adresse.Ville));

            list.Add(Spaces(length - 2 * marge, '-'));

            list.Add(Encode("Livreur : " + (commande.Livreur != null ? commande.Livreur.NoM : "")));

            list.Add(Spaces(length - 2 * marge, '-'));

            list.Add(Encode("Commande Num:"+commande.Num + "  " + commande.Date.GetValueOrDefault().ToString("HH:mm:ss d/MM/yy")));

            list.Add(Spaces(length - 2 * marge, '-'));

            if (Printer.NiiStartDoc(PrinterName, out jid) < 0) MessageBox.Show("Error start doc");
            foreach (var line in list)
            {
                foreach (var s in Format(line, length, marge))
                {
                    var str = "1B6100\"" + s + "\"0a";
                    long sjid = 0;
                    if (Printer.NiiPrint(PrinterName, str, str.Length, out sjid) < 0) MessageBox.Show("Error print");
                }
            }

            long id = 0;
            var end = "1B4A781b69";
            if (Printer.NiiPrint(PrinterName, end, end.Length, out id) < 0) MessageBox.Show("Error print");
            end = "1D\"G\"10";
            if (Printer.NiiPrint(PrinterName, end, end.Length, out id) < 0) MessageBox.Show("Error print");
            if (Printer.NiiEndDoc(PrinterName) < 0) MessageBox.Show("Error end doc");

            var writer = new StreamWriter("toto.txt");
            foreach (var line in list)
            {
                foreach (var s in Format(line, length, marge))
                {
                    writer.WriteLine(s);
                }
            }
            writer.Close();
        }

        string Encode(string str)
        {
            var stFormD = str.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            for (var ich = 0; ich < stFormD.Length; ich++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        string[] Format(string line, int length, int marge)
        {
            var nbSpaces = 0;
            var spaces = Spaces(marge, ' ');
            var str = line.Split('#');
            if (str.Length < 2)
                return new[] { spaces + line };

            if (line.Length < length - 2 * marge)
            {
                nbSpaces = length - line.Length - 2 * marge;
                return new[] { spaces + str[0] + Spaces(nbSpaces + 1, ' ') + str[1] };
            }
            var list = new List<string>();
            var i = 0;
            var words = str[0].Split(' ');

            while (i < words.Length)
            {
                var l = "";
                while (i < words.Length && l.Length + words[i].Length <= length - 2 * marge - str[1].Length - 1)
                {
                    l += words[i] + " ";
                    i++;
                }
                list.Add(spaces + l);
            }
            var tab = list.ToArray();
            nbSpaces = length - (tab[tab.Length - 1].Length + str[1].Length) - marge;
            tab[tab.Length - 1] += Spaces(nbSpaces, ' ') + str[1];
            return tab;
        }

        string Spaces(int length, char c)
        {
            var sp = "";
            for (var i = 0; i < length; i++)
                sp += c;
            return sp;
        }
    }
    
    public class TicketPrinter2
    {
        public void Print(Commande commande)
        {
            var doc = new PrintDocument();
            doc.PrintPage += (sender, e) =>
                                 {
                                     var font = new Font("Helvetica", 10);
                                     float x = e.MarginBounds.Left;
                                     float y = e.MarginBounds.Top;
                                     var  xQuantite= x;
                                     var xProduit = xQuantite + 30;
                                     var xPrix = xProduit + 200;
                                     float lineHeight = font.GetHeight(e.Graphics);
                                     var nbArticles = 0;

                                     e.Graphics.DrawString("DINAPOLI PIZZA", font, Brushes.Black, xProduit, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("Borne N°"+commande.IDBorn, font, Brushes.Black, xQuantite, y);
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
                                     e.Graphics.DrawString( nbArticles +" articles", font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("TOTAL", font, Brushes.Black, xProduit, y);
                                     e.Graphics.DrawString(" : " + commande.Total.ToString("C2"), font, Brushes.Black, xProduit + 60, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("TVA" , font, Brushes.Black, xProduit, y);
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
                                     e.Graphics.DrawString("Livreur : "+(commande.Livreur!=null?commande.Livreur.NoM:""), font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString("---------------------------------------------------------", font, Brushes.Black, xQuantite, y);
                                     y += lineHeight;
                                     e.Graphics.DrawString(commande.Num+"  "+commande.Date.GetValueOrDefault().ToString("HH:mm:ss d/MM/yy"), font, Brushes.Black, xQuantite, y);
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