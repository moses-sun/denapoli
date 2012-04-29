using System.ComponentModel.Composition;
using Denapoli.Modules.GUI.BackEnd.DataAdmin.View;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin
{

    [ModuleExport(typeof(DataAdminModule), DependsOnModuleNames = new[] { "DataModule" })]
    public class DataAdminModule : IModule
    {
        [Import]
        private DataAdminView View { get; set; }

        [Import]
        private DataAdminMenuView MenuView { get; set; }

        public void Initialize()
        {
        }
    }
}
