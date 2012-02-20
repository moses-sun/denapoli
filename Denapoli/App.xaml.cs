using System;
using System.IO;
using System.Reflection;
using System.Windows;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Denapoli
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //var culture = ShellSettings.Default.DefaultCulture;
            //if (culture != null)
            //{
            //    Thread.CurrentThread.CurrentCulture = culture;
            //    Thread.CurrentThread.CurrentUICulture = culture;
            //}
            var dir = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(dir);
            //var splashScreen = new SplashScreen(Assembly.GetExecutingAssembly(), "Resources/splash_screen.png");
            //splashScreen.Show(true);

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

            ExceptionPolicy.HandleException(ex, "Default Policy");
            MessageBox.Show("UnhandledException");
            Environment.Exit(1);
        }
    }
}
