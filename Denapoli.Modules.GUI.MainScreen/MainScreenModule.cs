using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Denapoli.Modules.GUI.MainScreen
{
    [ModuleExport(typeof(MainScreenModule))]
    public class MainScreenModule : IModule
    {
        [Import]
        private View.MainScreen View { get; set; }

        public void Initialize()
        {
        }
    }


}