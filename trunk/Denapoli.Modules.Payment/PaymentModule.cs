using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Denapoli.Modules.Payment
{
    [ModuleExport(typeof(PaymentModule))]
    public class PaymentModule : IModule
    {
        public void Initialize()
        {
        }
    }
}