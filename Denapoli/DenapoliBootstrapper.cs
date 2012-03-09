using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using Denapoli.Modules.Data;
using Denapoli.Modules.GUI.CommandScreen;
using Denapoli.Modules.GUI.MainScreen;
using Denapoli.Modules.I18n;
using Denapoli.Modules.Infrastructure.Behavior;
using Denapoli.Modules.Payment;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;

namespace Denapoli
{
    public class DenapoliBootstrapper : MefBootstrapper
    {
        private readonly CallbackLogger _callbackLogger = new CallbackLogger();

        private readonly string _repartLog = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Denapoli.log");


        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DenapoliBootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AutoPopulateExportedViewsBehavior).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(I18NModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MainScreenModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DataModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(PaymentModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CommandScreenModule).Assembly));
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Shell)Shell;
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
            _callbackLogger.DumpLog(_repartLog);
        }

        protected override DependencyObject CreateShell()
        {
            var shell = Container.GetExportedValue<Shell>();
            var toto = Container.GetExportedValue<IDataProvider>();
            var t = toto.GetAvailableFamilies().Count;
            return shell;
        }

        protected override ILoggerFacade CreateLogger()
        {
            return _callbackLogger;
        }
    }

}