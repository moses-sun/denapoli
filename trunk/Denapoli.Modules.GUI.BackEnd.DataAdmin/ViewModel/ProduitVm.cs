using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    public class ProduitVm : NotifyPropertyChanged, IEditableObject
    {
        public Produit Prod { get; set; }
        public static IEnumerable<Famille> Famileis { get; set; }
        public static IDataProvider DataProvider { get; set; }
        public static ILocalizationService LocalizationService { get; set; }
        public ObservableCollection<Traduction> Traductions { get; set; } 

        public ProduitVm()
        {
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            FamiliesNames = new ObservableCollection<string>(Famileis.Select(item => item.Nom));
            ReSetProperties();
        }

        public ProduitVm(Produit p, IEnumerable<Famille> famileis,IDataProvider dataProvider,  ILocalizationService localizationService)
        {
            Prod = p;
            Famileis = famileis;
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            FamiliesNames = new ObservableCollection<string>(Famileis.Select(item => item.Nom));
            ReSetProperties();
        }

        private void ReSetProperties()
        {
            Nom = Prod.Nom;
            Description = Prod.Description;
            Famille = Prod.Famille.Nom;
            Prix = Prod.Prix;
            _imageURL = Prod.ImageURL;

            Traductions.Clear();
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

       

        private string _oldNom;
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

        private string _oldDescription;
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

        private float _oldPrix;
        private float _prix;
        public float Prix
        {
            get { return _prix; }
            set
            {
                _prix = value;
                NotifyChanged("Prix");
            }
        }

        private string _oldFamille;
        private string _famille;
        public string Famille
        {
            get {
                return _famille;
            }
            set {
                _famille = value;
                NotifyChanged("SelectedFamilleName");
            }
        }

        private string _oldImageURL;
        private string _imageURL;
        public string ImageURL
        {
            get { return _imageURL; }
            set
            {
                _imageURL = value;
                NotifyChanged("ImageURL");
                IsImageLoaded = Visibility.Visible;
                IsPodImage = Visibility.Collapsed;
            }
        }


        private Visibility _isImageLoaded;
        public Visibility IsImageLoaded
        {
            get { return _isImageLoaded; }
            set
            {
                _isImageLoaded = value;
                NotifyChanged("IsImageLoaded");
            }
        }

        private Visibility _isPodImage;
        public Visibility IsPodImage
        {
            get { return _isPodImage; }
            set
            {
                _isPodImage = value;
                NotifyChanged("IsPodImage");
            }
        }

        public ObservableCollection<string> FamiliesNames { get; set; }

        public ActionCommand BrowseImageCommand { get; set; }
       

        private void BrowseImage()
        {
            var chooser = new OpenFileDialog {Filter = "Image files (*.png, *.jpg)|*.png;*.jpg"};
            var res = chooser.ShowDialog();
            if (DialogResult.Cancel.Equals(res))
                return;
            ImageURL = chooser.FileName;
        }

        public void BeginEdit()
        {
            _oldNom = Nom;
            _oldDescription = Description;
            _oldFamille = Famille;
            _oldPrix = Prix;
            _oldImageURL = ImageURL;
        }

        private void UpdateProduit()
        {
           /* Prod.Nom = Nom;
            Prod.Description = Description;
            Prod.Prix = Prix;
            var family = Famileis.FirstOrDefault(item => item.Nom == Famille);
            Prod.IDFaMil = family == null ? 1 : family.IDFaMil;
            Prod.Famille = family;*/
        }

        public void EndEdit()
        {
            UpdateProduit();
            DataProvider.InsertIfNotExists(Prod);
        }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Description = _oldDescription;
            Famille = _oldFamille;
            Prix = _oldPrix;
            _imageURL = _oldImageURL;
        }
    }
}