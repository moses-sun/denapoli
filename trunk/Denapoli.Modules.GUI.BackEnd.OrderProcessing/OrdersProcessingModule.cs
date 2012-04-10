using System.ComponentModel.Composition;
using Denapoli.Modules.GUI.BackEnd.OrderProcessing.View;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Denapoli.Modules.GUI.BackEnd.OrderProcessing
{


    [ModuleExport(typeof(OrdersProcessingModule), DependsOnModuleNames = new[] { "DataModule" })]
    public class OrdersProcessingModule : IModule
    {
        [Import]
        private OrdersProcessingView View { get; set; }

        public void Initialize()
        {
        }
    }
}
