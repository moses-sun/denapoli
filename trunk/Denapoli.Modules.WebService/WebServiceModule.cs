using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Denapoli.Modules.WebService
{
    [ModuleExport(typeof(WebServiceModule), DependsOnModuleNames = new[] { "DataModule" })]
    public class WebServiceModule : IModule
    {
        public void Initialize()
        {
        }
    }
}
