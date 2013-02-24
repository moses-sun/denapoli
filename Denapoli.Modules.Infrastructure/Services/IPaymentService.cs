using System.ComponentModel;

namespace Denapoli.Modules.Infrastructure.Services
{
    public interface IPaymentService : INotifyPropertyChanged
    {
        bool DemandeSolvabilite(double price);

        bool Enregistrement(double price);

        bool LancerTelecollecte();

        bool LancerInfoTelecollecte();

        string Message { get; }

        bool State { get; }

        string Ticket { get; set; }
        string Info { get; set; }

        event PropertyChangedEventHandler FinishEvent;

    }
}