using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class ProduitsAdminViewModel : NotifyPropertyChanged
    {
        private IDataProvider DataProvider { get; set; }
        private ILocalizationService LocalizationService { get; set; }
        public ISettingsService SettingsService { get; set; }
        public ObservableCollection<ProduitVm> Produits { get; set; }
      

        [ImportingConstructor]
        public ProduitsAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService, ISettingsService settingsService)
        {
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            SettingsService = settingsService;

            ProduitVm.DataProvider = DataProvider;
            ProduitVm.Famileis = DataProvider.GetAvailableFamilies();
            ProduitVm.LocalizationService = LocalizationService;
            ProduitVm.SettingsService = SettingsService;

            Produits = new ObservableCollection<ProduitVm>();
            UpdatePrduits();
            Produits.CollectionChanged += OnProduitschanged;
        }


        private void OnProduitschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    var deletedProd = (ProduitVm)e.OldItems[0];
                    DataProvider.Delete(deletedProd.Prod);
                    break;
            }
        }

        private void UpdatePrduits()
        {
            var old = SelectedProduit == null ? -1 : SelectedProduit.Prod.IDProd;
            Produits.CollectionChanged -= OnProduitschanged;
            Produits.Clear();
            var produits = DataProvider.GetAllProducts();
            produits.ForEach(item=>Produits.Add(new ProduitVm(item,DataProvider.GetAvailableFamilies(),DataProvider, LocalizationService, SettingsService)));
            SelectedProduit = Produits.FirstOrDefault(item => item.Prod.IDProd == old);
            SelectedProduit = SelectedProduit ?? Produits.FirstOrDefault();
            Produits.CollectionChanged += OnProduitschanged;
        }

        private ProduitVm _selectedProduit;
        public ProduitVm SelectedProduit
        {
            get { return _selectedProduit; }
            set
            {
                _selectedProduit = value;
                NotifyChanged("SelectedProduit");
            }
        }

        public void Update()
        {
            UpdatePrduits();
        }
    }
}