using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Denapoli.Modules.I18n
{
    [ModuleExport(typeof(I18NModule))]
    public class I18NModule : IModule
    {
        public void Initialize()
        {
        }
    }
}
