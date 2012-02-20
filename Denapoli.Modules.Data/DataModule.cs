using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Denapoli.Modules.Data
{
    [ModuleExport(typeof(DataModule))]
    public class DataModule : IModule
    {
        public void Initialize()
        {
        }
    }

    
}