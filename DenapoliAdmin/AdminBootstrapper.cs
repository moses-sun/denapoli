using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Denapoli;
using Denapoli.Modules.Data;
using Denapoli.Modules.GUI.BackEnd.DataAdmin;
using Denapoli.Modules.GUI.BackEnd.OrderProcessing;
using Denapoli.Modules.GUI.BackEnd.Stats;
using Denapoli.Modules.I18n;
using Denapoli.Modules.Infrastructure.Behavior;
using Denapoli.Modules.WebService;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;

namespace DenapoliAdmin
{
    public class AdminBootstrapper: MefBootstrapper
    {
        private readonly CallbackLogger _callbackLogger = new CallbackLogger();

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AdminBootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AutoPopulateExportedViewsBehavior).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DataModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(WebServiceModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(I18NModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(OrdersProcessingModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(StatsModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DataAdminModule).Assembly));
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (AdminShell)Shell;
            Application.Current.MainWindow.Closing += ClosingHandler;
            Application.Current.MainWindow.Show();
        }

        protected override Microsoft.Practices.Prism.Regions.IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();
            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));
            return factory;
        }



        private void ClosingHandler(object sender, CancelEventArgs e)
        {
        }

        protected override DependencyObject CreateShell()
        {
            var shell = Container.GetExportedValue<AdminShell>();
            var toto = Container.GetExportedValue<IDataProvider>();
            var t = toto.GetMenuAllCommandes().Count;
            return shell;
        }

        protected override ILoggerFacade CreateLogger()
        {
            return _callbackLogger;
        }
    }
}
