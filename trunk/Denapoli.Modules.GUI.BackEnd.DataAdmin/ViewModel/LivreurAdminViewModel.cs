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
    public class LivreurAdminViewModel : NotifyPropertyChanged
    {
        private IDataProvider DataProvider { get; set; }
        private ILocalizationService LocalizationService { get; set; }
        public ObservableCollection<LivreurVm> Livreurs { get; set; }

        [ImportingConstructor]
        public LivreurAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService)
        {
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            Livreurs = new ObservableCollection<LivreurVm>();
            UpdateLivreurs();
            Livreurs.CollectionChanged += OnLivreurschanged;
        }

        private void OnLivreurschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
               
                case NotifyCollectionChangedAction.Remove:
                  var deletedLivreur = (LivreurVm)e.OldItems[0];
                    DataProvider.Delete(deletedLivreur.Livreur);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateLivreurs()
        {
            Livreurs.CollectionChanged -= OnLivreurschanged;
            Livreurs.Clear();
            var livreurs = DataProvider.GetAllLivreurs();
            livreurs.ForEach(item => Livreurs.Add(new LivreurVm(item, DataProvider)));
            SelectedLivreur = Livreurs.FirstOrDefault();
            Livreurs.CollectionChanged += OnLivreurschanged;
        }

        private LivreurVm _selectedLivreur;
        public LivreurVm SelectedLivreur
        {
            get { return _selectedLivreur; }
            set
            {
                _selectedLivreur = value;
                NotifyChanged("SelectedLivreur");
            }
        }
    }
}