using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO.Ports;
using System.Text;
using System.Timers;
using System.Windows;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.Payment
{
    [Export(typeof(IPaymentService))]
    public class PaymentService : NotifyPropertyChanged,IPaymentService
    {
        private string _message;

        private bool _state;

        public bool Pay(double price)
        {
            Message = "inserer Carte";
            var wait = 5000;
           // var timer = new Timer(wait) { Enabled = true };
            State = true;
            OnFinishEvent(new PropertyChangedEventArgs(""));
           /* timer.Elapsed += (sender, args) =>
                                 {
                                     Message = "Tapez votre code";
                                     timer.Stop();
                                     var t = new Timer(wait);
                                     t.Elapsed += (o, eventArgs) =>
                                                      {
                                                          Message = "Patientez";
                                                          t.Stop();
                                                          var t2 = new Timer(wait);
                                                          t.Stop();
                                                          t2.Elapsed += (o1, eventArgs1) =>
                                                                           {
                                                                               Message = "Paiement eccpté, retirez votre carte";
                                                                               t2.Stop();
                                                                              
                                                                               NotifyChanged("State");
                                                                           };
                                                          t2.Start();
                                                      };
                                     t.Start();
                                 };
            timer.Start();*/
            return true;
        }

        public string Message
        {
            get { return _message; }
            private set { 
                _message = value;
                NotifyChanged("Message");
            }
        }

        public bool State
        {
            get { return _state; }
            private set
            {
                _state = value;
                NotifyChanged("State");
            }
        }

         public event PropertyChangedEventHandler FinishEvent;

        public void OnFinishEvent(PropertyChangedEventArgs e)
        {
            var handler = FinishEvent;
            if (handler != null) handler(this, e);
        }
    }

    public class Print
    {
        public SerialPort SerialPort { get; set; }
        public const byte ENQ = 0x05;
        public const byte ACK = 0x06;
        public const byte NAK = 0x15;
        public const byte STX = 0x02;
        public const byte ETX = 0x03;
        public const byte EOT = 0x04;

        public Print()
        {
            SerialPort = new SerialPort("COM1", 19200, Parity.None, 8, StopBits.One) {Handshake = Handshake.None};
        }

        public bool Payer(double montant)
        {
             if (!SerialPort.IsOpen) SerialPort.Open();
            return Send(montant) && Recieve();
        }

        private bool Send(double montant)
        {
            var buf = new byte[512];
            buf[0] = ENQ;
            SerialPort.Write(buf, 0, 1);
            SerialPort.Read(buf, 0, 1);
            if (buf[0] == ACK)
            {
                var mnt = FormatMontant(montant); //montant
                const string ecr = "01"; //numero de caisse
                const string ind = "1"; //presence de message de reponse
                const string emetteur = "0"; //mode de reglement indeffirent 
                const string type = "0"; // type de transaction : Debit
                const string dev = "EUR"; //code numerique de la devise
                const string priv = " "; //données privées a destination de l'application ex : données à imprimer au dos du chèque

                var msg = ecr + mnt + ind + emetteur + type + dev + priv;
                var tram = new byte[msg.Length + 3];
                var encoding = new ASCIIEncoding();
                var msgBytes = encoding.GetBytes(msg);
                tram[0] = STX;
                tram[tram.Length - 2] = ETX;
               
                byte lrc = 0;
                for (var i = 1; i < tram.Length-1; i++)
                    lrc ^= tram[i];
                lrc ^= 0;
                tram[tram.Length - 1] = lrc;

                SerialPort.Write(msgBytes, 0, tram.Length);

                SerialPort.Read(buf, 0, 1);
                if (buf[0] == ACK)
                {
                    buf[0] = EOT;
                    SerialPort.Write(buf, 0, 1);
                    return true;
                }
            }
            return false;
        }

        private bool Recieve()
        {
            var buf = new byte[512];
            SerialPort.Read(buf, 0, 1);
            if (buf[0] == ENQ)
            {
                buf[0] = ACK;
                SerialPort.Write(buf, 0, 1);
                SerialPort.Read(buf, 0, buf.Length);
                if (buf[0] == STX)
                {
                    var statut = buf[3];
                    buf[0] = ACK;
                    SerialPort.Write(buf, 0, 1);
                    SerialPort.Read(buf, 0, 1);
                    if (buf[0] == EOT && statut == '0')
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        private string FormatMontant(double montant)
        {
            return montant.ToString(CultureInfo.InvariantCulture);
        }
    }
}
