using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class FamillesAdminViewModel: NotifyPropertyChanged
    {
        private IDataProvider DataProvider { get; set; }
        private ILocalizationService LocalizationService { get; set; }
        public ISettingsService SettingsService { get; set; }
        public ObservableCollection<FamilleVm> Familles { get; set; }
       

        [ImportingConstructor]
        public FamillesAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService, ISettingsService settingsService)
        {
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            SettingsService = settingsService;
            Familles = new ObservableCollection<FamilleVm>();
            UpdateFamilles();
            Familles.CollectionChanged += OnFamilleschanged;
        }


        private void OnFamilleschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    MessageBox.Show("add product");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    MessageBox.Show("Remove product");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateFamilles()
        {
            Familles.CollectionChanged -= OnFamilleschanged;
            Familles.Clear();
            var familles = DataProvider.GetAvailableFamilies();
            familles.ForEach(item => Familles.Add(new FamilleVm(item, DataProvider, LocalizationService, SettingsService)));
            SelectedFamille = Familles.FirstOrDefault();
            Familles.CollectionChanged += OnFamilleschanged;
        }

        private FamilleVm _selectedFamille;
        public FamilleVm SelectedFamille
        {
            get { return _selectedFamille; }
            set
            {
                _selectedFamille = value;
                NotifyChanged("SelectedFamille");
            }
        }
    }
}