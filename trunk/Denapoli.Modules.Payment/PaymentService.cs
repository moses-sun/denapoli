using System.ComponentModel.Composition;
using System.Timers;
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
            var timer = new Timer(wait) { Enabled = true };

            timer.Elapsed += (sender, args) =>
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
                                                                               State = true;
                                                                           };
                                                          t2.Start();
                                                      };
                                     t.Start();
                                 };
            timer.Start();
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
    }
}
