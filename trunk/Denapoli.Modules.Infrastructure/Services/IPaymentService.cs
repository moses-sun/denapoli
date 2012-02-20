using System.ComponentModel;

namespace Denapoli.Modules.Infrastructure.Services
{
    public interface IPaymentService : INotifyPropertyChanged
    {
        bool Pay(double price);

        string Message { get; }

        bool State { get; }

    }
}