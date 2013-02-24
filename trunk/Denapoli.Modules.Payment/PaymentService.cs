using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.Payment
{
    [Export(typeof(IPaymentService))]
    public class PaymentService : NotifyPropertyChanged,IPaymentService
    {

        private string _message;
        public string Message
        {
            get { return _message; }
            private set
            {
                _message = value;
                NotifyChanged("Message");
            }
        }

        private bool _state;
        public bool State
        {
            get { return _state; }
            private set
            {
                _state = value;
                NotifyChanged("State");
            }
        }

        public SerialPort SerialPortt { get; set; }

        public string Ticket { get; set; }
        public string Info { get; set; }


        private bool Pay(double price)
        {
            var b = DemandeSolvabilite(price);
            MessageBox.Show(Message);
            MessageBox.Show(Info);
            var b2 = Enregistrement(price);
            MessageBox.Show(Message);
            MessageBox.Show(Info);
            MessageBox.Show(Ticket);
            return b && b2;
        }

        public bool DemandeSolvabilite(double price)
        {
            try
            {
                SerialPortt = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
                if (!SerialPortt.IsOpen) SerialPortt.Open();
                for (var i = 0; i < 1; i++)
                {
                    if (SendRequest(BuildSolvabilityRequest(price)) && RecieveResponse())
                    {
                        SerialPortt.Close();
                        State = true;
                        OnFinishEvent(null);
                        return true;
                    }
                }
                SerialPortt.Close();
                State = false;
                OnFinishEvent(null);
                return false;
            }
            catch (Exception)
            {
                State = false;
                Message = "ERREUR PORT COM1";
                OnFinishEvent(null);
                return false;
            }
        }

        public bool LancerTelecollecte()
        {
            try
            {
                SerialPortt = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
                if (!SerialPortt.IsOpen) SerialPortt.Open();
                for (var i = 0; i < 1; i++)
                {
                    if (SendRequest(BuildTelecollecteRequest()) && RecieveResponse())
                    {
                        SerialPortt.Close();
                        return true;
                    }
                }
                SerialPortt.Close();
                return false;
            }
            catch (Exception)
            {
                State = false;
                Message = "ERREUR PORT COM1";
                OnFinishEvent(null);
                return false;
            }
        }

        public bool Enregistrement(double price)
        {
            try
            {
                SerialPortt = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
                if (!SerialPortt.IsOpen) SerialPortt.Open();
                for (var i = 0; i < 3; i++)
                {
                    if (SendRequest(BuildEnregistrementRequest(price)) && RecieveResponse())
                    {
                        SerialPortt.Close();
                        return true;
                    }
                }
                SerialPortt.Close();
                return false;
            }
            catch (Exception)
            {
                State = false;
                Message = "ERREUR PORT COM1";
                OnFinishEvent(null);
                return false;
            }
        }

        public bool LancerInfoTelecollecte()
        {
            try
            {
                SerialPortt = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
                if (!SerialPortt.IsOpen) SerialPortt.Open();
                for (var i = 0; i < 1; i++)
                {
                    if (SendRequest(BuildInfoTelecollecteRequest()) && RecieveResponse())
                    {
                        SerialPortt.Close();
                        const string folder = "./Telecollectes";
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);
                        File.WriteAllText(
                            folder + "/telecollect" + String.Format("{0:yyyy-MM-dd HH-mm}", DateTime.Now), Ticket);
                        return true;
                    }
                }
                SerialPortt.Close();
                return false;
            }
            catch (Exception)
            {
                State = false;
                Message = "ERREUR PORT COM1";
                return false;
            }
        }


        #region I/O
        private byte[] BuildSolvabilityRequest(double montant)
        {
            var mnt = String.Format("{0:000000.00}", montant).Remove(6, 1); //FormatMontant(montant); //montant
            const string dev = "978"; //code numerique de la devise
            const string A = "A";
            const string K = "K";
            const string LG = "016"; //longueur
            const string MODE = "0"; // 1 : carte de test, 0: reelle
            const string TEMPO = "30"; //tempo de la transaction
            const string CLASSE = "2";  // montant donné = montant éstimé
            const string FONCTION = "D"; //debit

            var msg = A + K + LG + MODE + mnt + TEMPO + dev + CLASSE + FONCTION;
            var tram = new byte[msg.Length + 4];
            var encoding = new ASCIIEncoding();
            var msgBytes = encoding.GetBytes(msg);
            tram[0] = Const.STX;

            msgBytes.CopyTo(tram, 1);
            tram[tram.Length - 2] = Const.ETX;
            tram[tram.Length - 3] = Const.DLE;

            byte lrc = 0x00;
            for (var i = 1; i < tram.Length - 1; i++)
                lrc ^= tram[i];

            tram[tram.Length - 1] = lrc;
            return tram;
        }

        private byte[] BuildEnregistrementRequest(double montant)
        {
            var mnt = String.Format("{0:000000.00}", montant).Remove(6, 1); //FormatMontant(montant); //montant
            const string devise = "978"; //code numerique de la devise
            const string A = "A";
            const string L = "L";
            const string LG = "011"; //longueur

            var msg = A + L + LG + mnt + devise;
            var tram = new byte[msg.Length + 4];
            var encoding = new ASCIIEncoding();
            var msgBytes = encoding.GetBytes(msg);
            tram[0] = Const.STX;

            msgBytes.CopyTo(tram, 1);
            tram[tram.Length - 2] = Const.ETX;
            tram[tram.Length - 3] = Const.DLE;

            byte lrc = 0x00;
            for (var i = 1; i < tram.Length - 1; i++)
                lrc ^= tram[i];

            tram[tram.Length - 1] = lrc;
            return tram;
        }

        private byte[] BuildInfoTelecollecteRequest()
        {
            const string A = "A";
            const string J = "J";
            const string LG = "003"; //longueur
            const string CodeSousFct = "03";

            var msg = A + J + LG + CodeSousFct;
            var tram = new byte[msg.Length + 4];
            var encoding = new ASCIIEncoding();
            var msgBytes = encoding.GetBytes(msg);
            tram[0] = Const.STX;

            msgBytes.CopyTo(tram, 1);
            tram[tram.Length - 2] = Const.ETX;
            tram[tram.Length - 3] = Const.DLE;

            byte lrc = 0x00;
            for (var i = 1; i < tram.Length - 1; i++)
                lrc ^= tram[i];

            tram[tram.Length - 1] = lrc;
            return tram;
        }


        private byte[] BuildTelecollecteRequest()
        {
            const string A = "A";
            const string J = "J";
            const string LG = "002"; //longueur
            const string CodeSousFct = "04";

            var msg = A + J + LG + CodeSousFct;
            var tram = new byte[msg.Length + 4];
            var encoding = new ASCIIEncoding();
            var msgBytes = encoding.GetBytes(msg);
            tram[0] = Const.STX;

            msgBytes.CopyTo(tram, 1);
            tram[tram.Length - 2] = Const.ETX;
            tram[tram.Length - 3] = Const.DLE;

            byte lrc = 0x00;
            for (var i = 1; i < tram.Length - 1; i++)
                lrc ^= tram[i];

            tram[tram.Length - 1] = lrc;
            return tram;
        }

        private bool SendRequest(byte[] request)
        {
            var buf = new byte[512];
            MemSet(buf, 0);
            buf[0] = Const.ENQ;
            SerialPortt.Write(buf, 0, 1);
            MemSet(buf, 0);
            SerialPortt.Read(buf, 0, buf.Length);

            var b = buf[0];
            if (b == Const.ACK)
            {

                SerialPortt.Write(request, 0, request.Length);

                MemSet(buf, 0);
                SerialPortt.Read(buf, 0, 1);
                var r = buf[0];
                if (r == Const.ACK)
                {
                    buf[0] = Const.EOT;
                    SerialPortt.Write(buf, 0, 1);
                    return true;
                }
                buf[0] = Const.EOT;
                SerialPortt.Write(buf, 0, 1);
                return false;
            }
            buf[0] = Const.EOT;
            SerialPortt.Write(buf, 0, 1);
            Message = "Echec de paiement";
            return false;
        }

        private byte[] ReadTram()
        {
            var buf = new byte[2048];
            var tram = new byte[10000];
            MemSet(buf, 0);
            var tramIndex = 0;
            do
            {
                var index = 0;
                MemSet(buf, 0);
                SerialPortt.Read(buf, index++, 1);
                if (buf[0] == Const.STX)
                {
                    do
                    {
                        SerialPortt.Read(buf, index++, 1);
                    } while (buf[index - 1] != Const.ETX && buf[index - 1] != Const.ETB);
                    var b = buf[index - 1];
                    SerialPortt.Read(buf, index, 1);

                    Array.Copy(buf, 1, tram, tramIndex, index - 3);
                    tramIndex += index - 3;
                    index++;
                    buf[0] = Const.ACK;
                    SerialPortt.Write(buf, 0, 1);
                }
                else if (buf[0] == Const.ENQ)
                {
                    buf[0] = Const.ACK;
                    SerialPortt.Write(buf, 0, 1);
                }
            } while (buf.All(c => c != Const.ETX));

            var t = new byte[tramIndex];
            Array.Copy(tram, t, tramIndex);
            if (buf[0] != Const.EOT)
                SerialPortt.Read(buf, 0, 1);
            return buf[0] == Const.EOT ? t : null;
        }

        private bool RecieveResponse()
        {
            var buf = new byte[512];
            SerialPortt.Read(buf, 0, 1);
            var v = buf[0];
            if (v == Const.ENQ)
            {
                buf[0] = Const.ACK;
                SerialPortt.Write(buf, 0, 1);
                var tram = ReadTram();
                return ProcessTram(tram);
            }
            Message = buf[0] + " : caractère inattendu";
            return false;
        }

        private bool ProcessTram(byte[] tram)
        {
            var index = 0;
            var A = Encoding.ASCII.GetString(tram, index, 1); index += 1;
            var TYPE = Encoding.ASCII.GetString(tram, index, 1); index += 1;
            var lg1 = Encoding.ASCII.GetString(tram, index, 3); index += 3;
            var cr = Encoding.ASCII.GetString(tram, index, 1); index += 1;
            switch (TYPE.ToLower())
            {
                case "l":
                    {
                        var libelle = Encoding.ASCII.GetString(tram, index, 6); index += 6;
                        var lg = int.Parse(Encoding.ASCII.GetString(tram, index, 3)); index += 3;
                        var infos = Encoding.ASCII.GetString(tram, index, lg);

                        switch (cr)
                        {
                            case "0":
                                Message = "Paiement accepté";
                                break;
                            case "6":
                                Message = "Paiement annulé";
                                break;
                            default:
                                Message = "Paiement refusé";
                                break;
                        }
                        Info = infos;
                        Ticket = "";
                        if (libelle == "CBEMV ")
                            Ticket = PrintTicket(infos);
                        return cr == "0" || cr == "6";
                    }
                case "k":
                    {
                        var diag = Encoding.ASCII.GetString(tram, index, 2); index += 2;
                        var type = Encoding.ASCII.GetString(tram, index, 1); index += 1;
                        var pres = Encoding.ASCII.GetString(tram, index, 1); index += 1;
                        var papier = Encoding.ASCII.GetString(tram, index, 1); index += 1;
                        var libelle = Encoding.ASCII.GetString(tram, index, 6); index += 6;
                        var lg = int.Parse(Encoding.ASCII.GetString(tram, index, 3)); index += 3;
                        var infos = Encoding.ASCII.GetString(tram, index, lg);

                        Message = Const.Diag[Int32.Parse(diag)];
                        Info = infos;

                        if (libelle == "CBEMV ")
                            InfoCbemv(infos);
                        return cr == "0";
                    }
                case "j":
                    {
                        var codeSousFct = Encoding.ASCII.GetString(tram, index, 2); index += 2;
                        var nbApp = Encoding.ASCII.GetString(tram, index, 2); index += 2;
                        var lg = int.Parse(lg1) - 5;
                        if (lg > 0)
                        {
                            switch (codeSousFct)
                            {
                                case "04":
                                    var libelle = Encoding.ASCII.GetString(tram, index, 6); index += 6;
                                    var cr2 = Encoding.ASCII.GetString(tram, index, 1); index += 1;
                                    break;
                                case "03":
                                    if (cr == "0")
                                    {
                                        var libellet3 = Encoding.ASCII.GetString(tram, index, 6); index += 6;
                                        var cr3 = Encoding.ASCII.GetString(tram, index, 1); index += 1;
                                        var lg3 = Encoding.ASCII.GetString(tram, index, 3); index += 3;
                                        var tete = Encoding.ASCII.GetString(tram, index, 50); index += 50;
                                        var date = Encoding.ASCII.GetString(tram, index, 6); index += 6;
                                        var heure = Encoding.ASCII.GetString(tram, index, 6); index += 6;
                                        var numcontrat = Encoding.ASCII.GetString(tram, index, 7); index += 7;
                                        var siret = Encoding.ASCII.GetString(tram, index, 14); index += 14;
                                        var typeactivite = Encoding.ASCII.GetString(tram, index, 4); index += 4;
                                        var typepaiement = Encoding.ASCII.GetString(tram, index, 2); index += 2;
                                        var typesite = Encoding.ASCII.GetString(tram, index, 8); index += 8;
                                        var numLogiqueDuSystemAcceptation = Encoding.ASCII.GetString(tram, index, 3); index += 3;
                                        var libReponse = Encoding.ASCII.GetString(tram, index, 32); index += 32;
                                        var pied = Encoding.ASCII.GetString(tram, index, 50); index += 50;
                                        var nbrem = Int32.Parse(Encoding.ASCII.GetString(tram, index, 1)); index += 1;


                                        var ticket = "-------------- Telecollecte du " + DateTime.Now + "-------------------";
                                        ticket += tete + "\n";
                                        ticket += "Etat: ";

                                        switch (cr3)
                                        {
                                            case "0":
                                                ticket += "OK";
                                                break;
                                            case "1":
                                                ticket += "telecollecte incomplète";
                                                break;
                                            case "2":
                                                ticket += "appel erroné ou inefficace";
                                                break;
                                            case "3":
                                                ticket += "pas d'appel";
                                                break;

                                        }
                                        ticket += "\n";
                                        ticket += "Date: " + date + " heure: " + heure + "\n";
                                        ticket += "N° de contrat: " + numcontrat + "\n";
                                        ticket += "SIRET: " + siret + "\n";
                                        ticket += "N° logique du systeme d'acceptation: " + numLogiqueDuSystemAcceptation + "\n";
                                        ticket += "Libelle reponse: " + libReponse + "\n";
                                        ticket += "Nombre de remises effectuées: " + nbrem + "\n";

                                        for (int i = 0; i < nbrem; i++)
                                        {
                                            var numSequenceAcquereurDuneRemiseDeTransact = Encoding.ASCII.GetString(tram, index, 6); index += 6;
                                            var numRemise = Encoding.ASCII.GetString(tram, index, 6); index += 6;
                                            var nbTransacDebitDuneRemise = Encoding.ASCII.GetString(tram, index, 6); index += 6;
                                            var codeMonnaie = Encoding.ASCII.GetString(tram, index, 3); index += 3;
                                            var montant = Double.Parse(Encoding.ASCII.GetString(tram, index, 12)) / 100; index += 12;
                                            var partiFrac = Encoding.ASCII.GetString(tram, index, 1); index += 1;
                                            var nbTransacNonAboutie = Encoding.ASCII.GetString(tram, index, 6); index += 6;

                                            ticket += "Remise N°: " + numRemise + "\n";
                                            ticket += "      N° Sequence acquereur d'une remise de transact: " + numSequenceAcquereurDuneRemiseDeTransact + "\n";
                                            ticket += "      Nombre de transactions de debit: " + nbTransacDebitDuneRemise + "\n";
                                            ticket += "      Monnaie: " + codeMonnaie + "\n";
                                            ticket += "      Montant: " + montant + "   " + codeMonnaie + "\n";
                                            ticket += "      Nombre de transactions non abouties: " + nbTransacNonAboutie + "\n";
                                        }
                                        ticket += pied + "\n";
                                        ticket += "-------------------------------------------------" + "\n";
                                        Ticket = ticket;
                                    }
                                    break;
                            }

                        }

                        return cr == "0";
                    }
            }
            return true;
        }

        private string PrintTicket(string info)
        {
            var index = 0;
            if (info.Length == 345)
            {
                var fomatTicket = info.Substring(index, 50); index += 50;
                var date = info.Substring(index, 6); index += 6;
                var heure = info.Substring(index, 6); index += 6;
                var enseigneAccepteur = info.Substring(index, 60); index += 60;
                var typeDeTransaction = info.Substring(index, 2); index += 2;
                var numContrat = info.Substring(index, 7); index += 7;
                var siret = info.Substring(index, 14); index += 14;
                var typeDactiviteCommercon = info.Substring(index, 4); index += 4;
                var typePaiement = info.Substring(index, 2); index += 2;
                var typeSite = info.Substring(index, 8); index += 8;
                var numComptePrimairePorteur = info.Substring(index, 19); index += 19;
                var tasa = info.Substring(index, 4); index += 4;
                var dateFinValiditeApplicationCarte = info.Substring(index, 4); index += 4;
                var codeService = info.Substring(index, 3); index += 3;
                var cryptogramme = info.Substring(index, 16); index += 16;
                var codeDevise = info.Substring(index, 3); index += 3;
                var numLogiqueSystemeAcceptioation = info.Substring(index, 3); index += 3;
                var numTrasactionGenereeParSystemeDacceptation = info.Substring(index, 6); index += 6;
                var numRemise = info.Substring(index, 6); index += 6;
                var modeLectureNumCarte = info.Substring(index, 1); index += 1;
                var numeroAutorisationTransaction = info.Substring(index, 6); index += 6;
                var codeForcage = info.Substring(index, 1); index += 1;
                var montantEuro = info.Substring(index, 8); index += 8;
                var codeMonnaieEuro = info.Substring(index, 3); index += 3;
                var partieFractionnaireEuro = info.Substring(index, 1); index += 1;
                var montantFr = info.Substring(index, 8); index += 8;
                var codeMonnaieFr = info.Substring(index, 3); index += 3;
                var partieFractionnaireFr = info.Substring(index, 1); index += 1;
                var montantEstime = info.Substring(index, 8); index += 8;
                var piedTicket = info.Substring(index, 50); index += 50;
                var aid = info.Substring(index, 16); index += 16;
                var label = info.Substring(index, 16);

                var mntE = Double.Parse(montantEuro) / 100;
                var mntF = Double.Parse(montantFr) / 100;

                var date2 = date.Substring(0, 2) + "/" + date.Substring(2, 2) + "/" + date.Substring(4, 2);
                var heure2 = heure.Substring(0, 2) + ":" + heure.Substring(2, 2) + ":" + heure.Substring(4, 2);
                return " == CARTE BANCAIRE ==\n" +
                       aid + "\n"
                       + label + "\n"
                       + "le : " + date2 + " à " + heure2 + "\n"
                       + enseigneAccepteur.Substring(0, 15) + "\n"
                       + enseigneAccepteur.Substring(40, 12) + "\n"
                       + numContrat + "\n"
                       + "******" + numComptePrimairePorteur.Substring(6, 9) + "*\n"
                       + cryptogramme + "\n"
                       + numLogiqueSystemeAcceptioation + " " + numTrasactionGenereeParSystemeDacceptation + " " +
                       numRemise + " " + modeLectureNumCarte + "\n"
                       + "MONTANT : " + String.Format("{0:#.00}", mntE) + " EUR\n"
                       + "Pour information : " + String.Format("{0:#.00}", mntF) + " FRF\n"
                       + "DEBIT\n"
                    ;
            }

            return "";
        }

        private static void InfoCbemv(string info)
        {
            var index = 0;
            var lg = info.Length;
            if (lg == 12)
            {
                var mntMax = info.Substring(index, 8); index += 8;
                var monnaie = info.Substring(index, 3); index += 3;
                var partiFractionnaireDeMonnaie = info.Substring(index, 1); index += 1;
            }
            else if (lg == 298)
            {
                var fomatTicket = info.Substring(index, 50); index += 50;
                var date = info.Substring(index, 6); index += 6;
                var heure = info.Substring(index, 6); index += 6;
                var enseigneAccepteur = info.Substring(index, 60); index += 60;
                var typeDeTransaction = info.Substring(index, 2); index += 2;
                var numContrat = info.Substring(index, 7); index += 7;
                var siret = info.Substring(index, 14); index += 14;
                var numComptePrimairePorteur = info.Substring(index, 19); index += 19;
                var dateFinValiditeApplicationCarte = info.Substring(index, 4); index += 4;
                var monnaie = info.Substring(index, 3); index += 3;
                var codeService = info.Substring(index, 3); index += 3;
                var numLogiqueSystemeAcceptioation = info.Substring(index, 3); index += 3;
                var numTrasactionGenereeParSystemeDacceptation = info.Substring(index, 6); index += 6;
                var numRemise = info.Substring(index, 6); index += 6;
                var modeLectureNumCarte = info.Substring(index, 1); index += 1;
                var montantEuro = info.Substring(index, 8); index += 8;
                var codeMonnaieEuro = info.Substring(index, 3); index += 3;
                var partieFractionnaireEuro = info.Substring(index, 1); index += 1;
                var montantFr = info.Substring(index, 8); index += 8;
                var codeMonnaieFr = info.Substring(index, 3); index += 3;
                var partieFractionnaireFr = info.Substring(index, 1); index += 1;
                var resultatDesVerifications = info.Substring(index, 2); index += 2;
                var piedTicket = info.Substring(index, 50); index += 50;
                var aid = info.Substring(index, 16); index += 16;
                var label = info.Substring(index, 16);
            }

        }

        private void MemSet(byte[] tab, byte val)
        {
            for (var i = 0; i < tab.Length; i++)
                tab[i] = val;
        }

        #endregion

        public event PropertyChangedEventHandler FinishEvent;

        public void OnFinishEvent(PropertyChangedEventArgs e)
        {
            var handler = FinishEvent;
            if (handler != null) handler(this, e);
        }
    }
}
