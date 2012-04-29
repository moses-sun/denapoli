using System.ComponentModel.Composition;
using Denapoli.Modules.GUI.BackEnd.Stats.View;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Denapoli.Modules.GUI.BackEnd.Stats
{

    [ModuleExport(typeof(StatsModule), DependsOnModuleNames = new[] { "DataModule" })]
    public class StatsModule : IModule
    {
        [ImportingConstructor]
        public StatsModule(StatisticsView v)
        {
            View = v;
        }

        private StatisticsView View { get; set; }  
        
        [Import]
        private StatisticsMenuView MenuView { get; set; }

        public void Initialize()
        {
        }
    }
}
