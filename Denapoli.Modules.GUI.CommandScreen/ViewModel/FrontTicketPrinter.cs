using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Services;
using NiiPrinterCLib;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class FrontTicketPrinter
    {
        public ILocalizationService LocalizationService { get; set; }

        public FrontTicketPrinter(ILocalizationService localizationService)
        {
            LocalizationService = localizationService;
            Printer = new NIIClassLib();
            PrinterName = "NII ExD NP-K205";
        }

        protected string PrinterName { get; set; }

        protected NIIClassLib Printer { get; set; }
       
        public void Print(Commande commande)
        {
            const int length = 48;
            const int marge = 4;

            long jid = 0;
            var nbArticles = 0;

            if (Printer.NiiStartDoc(PrinterName, out jid) < 0)
                MessageBox.Show("Error");

            var list = new List<string>
                           {
                               Encode("DINAPOLI PIZZA"), 
                               Encode("1bis rue de la réunion"),
                               Encode("75020 Paris"),
                               Encode(LocalizationService.Localize("Borne Num")+": " + commande.IDBorn), 
                               Spaces(length - 2*marge, '-'), 
                               Encode("  #Euro ")
                           };

            foreach (var produit in commande.ProduitsCommande)
            {
                var str = "" + String.Format("{0,2}", produit.Quantite) + " " + LocalizationService.Localize(produit.Produit.Nom).ToUpper() + "#" +
                          String.Format("{0,5:##0.00}", (produit.Quantite * produit.Produit.Prix));

                list.Add(Encode(str));
                nbArticles += produit.Quantite;
            }
            foreach (var menu in commande.Menus)
            {
                var str = "" + String.Format("{0,2}", menu.Quantite) + " " + LocalizationService.Localize(menu.Produit.Nom).ToUpper() + "#" + String.Format("{0,5:##0.00}", (menu.Produit.Prix * menu.Quantite));
                list.Add(Encode(str));
                foreach (var comp in menu.ProduitsMenu)
                {
                    list.Add(Encode("    " + String.Format("{0,2}", comp.Quantite) + " " + LocalizationService.Localize(comp.Produit.Nom).ToUpper()));
                }
                nbArticles += menu.Quantite;
            }

            list.Add(Spaces(length - 2 * marge, '-'));

            list.Add(Encode("                      " + nbArticles + " "+LocalizationService.Localize("articles")));
            list.Add(Encode("                      "+LocalizationService.Localize("TOTAL")+" : #" + String.Format("{0,5:##0.00}", commande.Total)));
            list.Add(Encode("                      "+LocalizationService.Localize("TVA")+"   : #" + String.Format("{0,5:##0.00}", commande.Tva)));

            list.Add(Spaces(length - 2 * marge, '-'));

            list.Add(Encode(LocalizationService.Localize("Client")+" : " + commande.Client.Nom + " " + commande.Client.Prenom));
            list.Add(Encode(LocalizationService.Localize("Tel")+" : " + commande.Client.Tel));
            list.Add(Encode(LocalizationService.Localize("Adresse de livraison")+" : "));
            list.Add(Encode("   " + commande.Adresse.Num + " " + commande.Adresse.Voie));
            list.Add(Encode("   "+LocalizationService.Localize("Chambre")+" " + commande.Adresse.NumCHamBRe));
            list.Add(Encode("   " + commande.Adresse.Complement));
            list.Add(Encode("   " + commande.Adresse.CP + " " + commande.Adresse.Ville));

            list.Add(Spaces(length - 2 * marge, '-'));

            //list.Add(Encode("Livreur : " + (commande.Livreur != null ? commande.Livreur.NoM : "")));

            //list.Add(Spaces(length - 2 * marge, '-'));

            list.Add(Encode(commande.Num + "  " + commande.Date.GetValueOrDefault().ToString("HH:mm:ss d/MM/yy")));
            list.Add(Encode(LocalizationService.Localize("Votre commande sera livrée")));
            list.Add(Encode(LocalizationService.Localize("dans moins de 45 minutes")));

            list.Add(Spaces(length - 2 * marge, '-'));
            list.Add(Encode("       "+LocalizationService.Localize("Bonne journée")));

            var writer = new StreamWriter("toto.txt");
            foreach (var line in list)
            {
                foreach (var s in Format(line, length, marge))
                {
                    writer.WriteLine(s);
                }
            }
            writer.Close();
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

            //long id = 0;
            //var end = "1B4A781b69";
            //if (Printer.NiiPrint(PrinterName, end, end.Length, out id) < 0) MessageBox.Show("Error print");
            //end = "1D\"G\"10";
            //if (Printer.NiiPrint(PrinterName, end, end.Length, out id) < 0) MessageBox.Show("Error print");
            //if (Printer.NiiEndDoc(PrinterName) < 0) MessageBox.Show("Error end doc");
        }

        public void Print(string ticket)
        {
            var lines = ticket.Split('\n');
            long jid;
            if (Printer.NiiStartDoc(PrinterName, out jid) < 0) MessageBox.Show("Error start doc");
            foreach (var line in lines)
            {
                var str = "1B6100\"" + line + "\"0a";
                long sjid = 0;
                if (Printer.NiiPrint(PrinterName, str, str.Length, out sjid) < 0) MessageBox.Show("Error print");
            }
            long id = 0;
            var end = "1B4A781b69";
            if (Printer.NiiPrint(PrinterName, end, end.Length, out id) < 0) MessageBox.Show("Error print");
            end = "1D\"G\"10";
            if (Printer.NiiPrint(PrinterName, end, end.Length, out id) < 0) MessageBox.Show("Error print");
            if (Printer.NiiEndDoc(PrinterName) < 0) MessageBox.Show("Error end doc");

            var writer = new StreamWriter("toto2.txt");
            writer.WriteLine(ticket);
            writer.Close();
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

    }

}