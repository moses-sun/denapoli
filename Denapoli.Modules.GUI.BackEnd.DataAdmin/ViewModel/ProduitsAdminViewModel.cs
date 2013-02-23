using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Denapoli.Modules.Data;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.Prism.Events;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class ProduitsAdminViewModel : NotifyPropertyChanged, IUpdatebale, IEditableObject
    {
        private IDataProvider DataProvider { get; set; }
        private ILocalizationService LocalizationService { get; set; }
        public ISettingsService SettingsService { get; set; }
        public IWebService WEBService { get; set; }
        public ObservableCollection<ProduitVm> Produits { get; set; }
      

        [ImportingConstructor]
        public ProduitsAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService, ISettingsService settingsService, IWebService webService)
        {
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            SettingsService = settingsService;
            WEBService = webService;
            ProduitVm.Parent = this;
            ProduitVm.DataProvider = DataProvider;
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
                    DataAdminViewModel.EventAggregator.GetEvent<UpdateEvent>().Publish(this);
                    break;
            }
        }

        private void UpdatePrduits()
        {
            var old = SelectedProduit == null ? -1 : SelectedProduit.Prod.IDProd;
            Produits.CollectionChanged -= OnProduitschanged;
            Produits.Clear();
            var produits = DataProvider.GetAllProducts();
            var availableFamilies = DataProvider.GetAvailableFamilies();
            ProduitVm.Famileis = availableFamilies;
            produits.ForEach(item=>Produits.Add(new ProduitVm(item,availableFamilies,DataProvider, LocalizationService, SettingsService, WEBService)));
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