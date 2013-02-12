using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Denapoli.Modules.Data;
using Denapoli.Modules.GUI.BackEnd.DataAdmin.View;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.Prism.Events;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class DataAdminViewModel : AbstractScreenViewModel
    {
        public Window Window { get; set; }
        private IDataProvider DataProvider { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        public static IEventAggregator EventAggregator { get; set; }
        public ICommand ShowDataAdminCommand { get; set; }
        public DataAdminView View { get; set; }

        [ImportingConstructor]
        public DataAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService, IEventAggregator eventAggregator)
        {
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            EventAggregator = eventAggregator;
            IsVisible = true;
            ShowDataAdminCommand = new ActionCommand(Show);
            EventAggregator.GetEvent<UpdateEvent>().Subscribe(o =>
                                                                                     {
                                                                                         LocalizationService.Reset();
                                                                                         if(o != ProduitsViewModel) ProduitsViewModel.Update();
                                                                                         if (o != FamillesViewModel) FamillesViewModel.Update();
                                                                                         if (o != LanguagesViewModel) LanguagesViewModel.Update();
                                                                                         if (o != MenusViewModel) MenusViewModel.Update();
                                                                                         if (o != BornesViewModel) BornesViewModel.Update();
                                                                                         if (o != LivreursViewModel) LivreursViewModel.Update();
                                                                                     });


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