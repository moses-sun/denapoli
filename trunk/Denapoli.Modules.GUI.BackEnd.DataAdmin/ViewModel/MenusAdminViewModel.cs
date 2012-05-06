using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class MenusAdminViewModel : NotifyPropertyChanged
    {
        private IDataProvider DataProvider { get; set; }
        private ILocalizationService LocalizationService { get; set; }
        public ObservableCollection<MenuVm> Menus { get; set; }


        [ImportingConstructor]
        public MenusAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService)
        {
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            Menus = new ObservableCollection<MenuVm>();
            Updatemenus();
            Menus.CollectionChanged += OnMenuschanged;
        }


        private void OnMenuschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    MessageBox.Show("add menu");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    MessageBox.Show("Remove menu");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Updatemenus()
        {
            Menus.CollectionChanged -= OnMenuschanged;
            Menus.Clear();
            var menus = DataProvider.GetAllProducts().Where(item=>item.IsMenu);
            menus.ForEach(item => Menus.Add(new MenuVm(item, DataProvider, LocalizationService)));
            SelectedMenu = Menus.FirstOrDefault();
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
    }
}