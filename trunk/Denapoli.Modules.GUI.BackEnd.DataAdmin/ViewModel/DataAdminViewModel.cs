using System.ComponentModel.Composition;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Denapoli.Modules.Data;
using Denapoli.Modules.GUI.BackEnd.DataAdmin.View;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class DataAdminViewModel : AbstractScreenViewModel
    {
        public Window Window { get; set; }
        private IDataProvider DataProvider { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        public ICommand ShowDataAdminCommand { get; set; }
        public DataAdminView View { get; set; }

        [ImportingConstructor]
        public DataAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService)
        {
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            IsVisible = true;
            ShowDataAdminCommand = new ActionCommand(Show);

            var timer = new Timer { Interval = 120000 };
            timer.Elapsed += (sender, args) =>
                                 {
                                     if (View == null) return;
                                     View.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                                 new System.Windows.Threading.
                                                                     DispatcherOperationCallback(delegate
                                                                                                     {
                                                                                                         ProduitsViewModel.Update();
                                                                                                         FamillesViewModel.Update();
                                                                                                         LanguagesViewModel.Update();
                                                                                                         MenusViewModel.Update();
                                                                                                         BornesViewModel.Update();
                                                                                                         LivreursViewModel.Update();
                                                                                                         return null;
                                                                                                     }),
                                                                     null);
                                    
                                 };
            timer.Enabled = true;
            timer.Start();
        }

        [Import]
        public ProduitsAdminViewModel ProduitsViewModel { get; private set; }

        [Import]
        public FamillesAdminViewModel FamillesViewModel { get; private set; }

        [Import]
        public LanguagesAdminViewModel LanguagesViewModel { get; private set; }

        [Import]
        public MenusAdminViewModel MenusViewModel { get; private set; }

        [Import]
        public BornesAdminViewModel BornesViewModel { get; private set; }

        [Import]
        public LivreurAdminViewModel LivreursViewModel { get; private set; }



        private delegate void OpenCallback();
        private void Show()
        {
            if (Window.Dispatcher.CheckAccess() == false)
            {
                var uCallBack = new OpenCallback(Show);
                Window.Dispatcher.Invoke(uCallBack, new object());
            }
            else
            {
                Window.ShowDialog();
            }
        }
    }
}