using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using Denapoli.Modules.Data;
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
            FamilleVm.DataProvider = DataProvider;
            FamilleVm.LocalizationService = LocalizationService;
            FamilleVm.SettingsService = SettingsService;
            Familles = new ObservableCollection<FamilleVm>();
            UpdateFamilles();
            Familles.CollectionChanged += OnFamilleschanged;
        }


        private void OnFamilleschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
               
                case NotifyCollectionChangedAction.Remove:
                    var deletedFamily = (FamilleVm)e.OldItems[0];
                    DataProvider.Delete(deletedFamily.Family);
                    break;
            }
        }

        private void UpdateFamilles()
        {
            var old = SelectedFamille == null ? -1 : SelectedFamille.Family.IDFaMil;
            Familles.CollectionChanged -= OnFamilleschanged;
            Familles.Clear();
            var familles = DataProvider.GetAvailableFamilies();
            familles.ForEach(item => Familles.Add(new FamilleVm(item, DataProvider, LocalizationService, SettingsService)));
            SelectedFamille = Familles.FirstOrDefault(item => item.Family.IDFaMil == old);
            SelectedFamille = SelectedFamille ?? Familles.FirstOrDefault();
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

        public void Update()
        {
            UpdateFamilles();
        }
    }
}