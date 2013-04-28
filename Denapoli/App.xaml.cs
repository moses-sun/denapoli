using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Denapoli
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("fr-FR");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");

            FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage("fr-FR")));
            var dir = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(dir);
            var splashScreen = new SplashScreen(Assembly.GetExecutingAssembly(), "Resources/pizza_splash.png");
            splashScreen.Show(true);

#if (DEBUG)
            RunInDebugMode();
#else
            RunInReleaseMode();
#endif
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            if (e.Args.Length > 0)
            {
                Properties["ContentToLoad"] = e.Args[0];
            }
        }

        private static void RunInDebugMode()
        {
            var bootstrapper = new DenapoliBootstrapper();
            bootstrapper.Run();
        }

        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                var bootstrapper = new DenapoliBootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null)
                return;

            var writer = new StreamWriter("denapoli_log.txt");
            writer.WriteLine(ex.StackTrace);
            writer.Close();

            ExceptionPolicy.HandleException(ex, "Default Policy"); 
            MessageBox.Show("UnhandledException");
            Environment.Exit(1);
        }
    }
}
