using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class MenusAdminViewModel : NotifyPropertyChanged, IUpdatebale, IEditableObject
    {
        private IDataProvider DataProvider { get; set; }
        private ILocalizationService LocalizationService { get; set; }
        public ISettingsService SettingsService { get; set; }
        public IWebService WEBService { get; set; }
        public ObservableCollection<MenuVm> Menus { get; set; }


        [ImportingConstructor]
        public MenusAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService, ISettingsService settingsService, IWebService webService)
        {
            MenuVm.Parent = this;
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            SettingsService = settingsService;
            WEBService = webService;
            MenuVm.DataProvider = DataProvider;
            MenuVm.LocalizationService = LocalizationService;
            MenuVm.SettingsService = SettingsService;
            Menus = new ObservableCollection<MenuVm>();
            Updatemenus();
            Menus.CollectionChanged += OnMenuschanged;
        }


        private void OnMenuschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
               
                case NotifyCollectionChangedAction.Remove:
                   var deletedMenu = (MenuVm)e.OldItems[0];
                    DataProvider.DeleteMenu(deletedMenu.Menu);
                    DataAdminViewModel.EventAggregator.GetEvent<UpdateEvent>().Publish(this);
                    break;
            }
        }

        private void Updatemenus()
        {
            var old = SelectedMenu == null ? -1 : SelectedMenu.Menu.IDProd;
            Menus.CollectionChanged -= OnMenuschanged;
            Menus.Clear();
            var menus = DataProvider.GetAllProducts().Where(item=>item.IsMenu);
            menus.ForEach(item => Menus.Add(new MenuVm(item, DataProvider, LocalizationService, SettingsService,WEBService)));
            SelectedMenu = Menus.FirstOrDefault(item => item.Menu.IDProd == old);
            SelectedMenu = SelectedMenu ?? Menus.FirstOrDefault();
            Menus.CollectionChanged += OnMenuschanged;
        }

        private MenuVm _selectedMenu;
        public MenuVm SelectedMenu
        {
            get { return _selectedMenu; }
            set
            {
                _selectedMenu = value;
                NotifyChanged("SelectedMenu");
            }
        }

        public void Update()
        {
            Updatemenus();
        }

        public void BeginEdit()
        {
            
        }

        public void EndEdit()
        {
            NotifyChanged("Update");
        }

        public void CancelEdit()
        {
        }
    }
}