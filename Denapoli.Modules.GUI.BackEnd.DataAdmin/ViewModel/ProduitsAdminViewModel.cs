using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    [Export]
    public class ProduitsAdminViewModel : NotifyPropertyChanged
    {
        public IDataProvider DataProvider { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        public ObservableCollection<ProduitVm> Produits { get; set; }
        public ActionCommand RemoveProduitCommand { get; set; }
        public ActionCommand EditProduitCommand { get; set; }
        public ActionCommand AddProduitCommand { get; set; }

        [ImportingConstructor]
        public ProduitsAdminViewModel(IDataProvider dataProvider, ILocalizationService localizationService)
        {
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            Produits = new ObservableCollection<ProduitVm>();
            AddProduitCommand = new ActionCommand(AddProduct);
            RemoveProduitCommand = new ActionCommand(RemoveProduct);
            EditProduitCommand = new ActionCommand(EditProduct);
            UpdatePrduits();
        }

        private void UpdatePrduits()
        {
            Produits.Clear();
            var produits = DataProvider.GetAllProducts();
            produits.ForEach(item=>Produits.Add(new ProduitVm(item,DataProvider.GetAvailableFamilies(), LocalizationService)));
            SelectedProduit = Produits.FirstOrDefault();
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

        private void EditProduct()
        {
            throw new System.NotImplementedException();
        }

        private void RemoveProduct()
        {
            throw new System.NotImplementedException();
        }

        private void AddProduct()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ProduitVm : NotifyPropertyChanged
    {
        public ProduitVm(Produit p, List<Famille> famileis,  ILocalizationService localizationService)
        {
            Prod = p;
            LocalizationService = localizationService;
            BrowseImageCommand = new ActionCommand(BrowseImage);
            UpdateTraductions();
            Families = new ObservableCollection<Famille>(famileis);
        }

        public ObservableCollection<Famille> Families { get; set; }

        public ActionCommand BrowseImageCommand { get; set; }

        private string _imageURL;
        public string ImageURL
        {
            get { return _imageURL; }
            set
            {
                _imageURL = value;
                NotifyChanged("ImageURL");
            }
        }

        private void BrowseImage()
        {
            var chooser = new OpenFileDialog {Filter = "Image files (*.png, *.jpg)|*.png;*.jpg"};
            var res = chooser.ShowDialog();
            if (DialogResult.Cancel.Equals(res))
                return;
            ImageURL = chooser.FileName;
        }

        private void UpdateTraductions()
        {
           Traductions = new ObservableCollection<Traduction>();
           foreach (var language in LocalizationService.AvailableLangages)
            {
                Traductions.Add(new Traduction
                                    { 
                    Langue = language.Name,
                    Nom = LocalizationService.Localize(Prod.Nom, language),
                    Description = LocalizationService.Localize(Prod.Description, language)
                });
            }
        }

        public Produit Prod { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        public ObservableCollection<Traduction> Traductions { get; set; } 
    }

    public class Traduction : NotifyPropertyChanged
    {
        private string _nom;
        public string Nom
        {
            get { return _nom; }
            set
            {
                _nom = value;
                NotifyChanged("Nom");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyChanged("Description");
            }
        }

        private string _langue;
        public string Langue
        {
            get { return _langue; }
            set
            {
                _langue = value;
                NotifyChanged("Langue");
            }
        }
    }
}