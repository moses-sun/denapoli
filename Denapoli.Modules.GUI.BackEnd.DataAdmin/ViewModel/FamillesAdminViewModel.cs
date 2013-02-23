using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class FamillesAdminViewModel : NotifyPropertyChanged, IUpdatebale, IEditableObject
    {
        private IDataProvider DataProvider { get; set; }
        private ILocalizationService LocalizationService { get; set; }
        public ISettingsService SettingsService { get; set; }
        public IWebService WEBService { get; set; }
        public ObservableCollection<FamilleVm> Familles { get; set; }
       

        [ImportingConstructor]
        public FamillesAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService, ISettingsService settingsService, IWebService webService)
        {
            FamilleVm.Parent = this;
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            SettingsService = settingsService;
            WEBService = webService;
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
                    DataAdminViewModel.EventAggregator.GetEvent<UpdateEvent>().Publish(this);
                    break;
            }
        }

        private void UpdateFamilles()
        {
            var old = SelectedFamille == null ? -1 : SelectedFamille.Family.IDFaMil;
            Familles.CollectionChanged -= OnFamilleschanged;
            Familles.Clear();
            var familles = DataProvider.GetAvailableFamilies();
            familles.ForEach(item => Familles.Add(new FamilleVm(item, DataProvider, LocalizationService, SettingsService, WEBService)));
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