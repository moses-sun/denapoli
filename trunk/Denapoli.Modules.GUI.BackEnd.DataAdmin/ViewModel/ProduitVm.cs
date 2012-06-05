using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
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
        public static ISettingsService SettingsService { get; set; }
        public ObservableCollection<Traduction> Traductions { get; set; } 

        public ProduitVm()
        {
            Prod = new Produit();
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            FamiliesNames = new ObservableCollection<string>(Famileis.Select(item => item.Nom));
            ReSetProperties();
        }

        public ProduitVm(Produit p, IEnumerable<Famille> famileis, IDataProvider dataProvider, ILocalizationService localizationService, ISettingsService settingsService)
        {
            Prod = p;
            Famileis = famileis;
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            SettingsService = settingsService;
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            FamiliesNames = new ObservableCollection<string>(Famileis.Select(item => item.Nom));
            ReSetProperties();
        }

        private void ReSetProperties()
        {
            Nom = Prod.Nom;
            Description = Prod.Description;
            Famille = Prod.Famille != null ? Prod.Famille.Nom : "";
            Prix = Prod.Prix;
            ImageURL = Prod.ImageURL;
            ImageLocalURL = Prod.ImageURL;
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

        public string ImageLocalURL
        {
            get {
                return _imageLocalURL;
            }
            set {
                _imageLocalURL = value;
                NotifyChanged("ImageLocalURL");
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
        private string _imageLocalURL;

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
            ImageLocalURL = chooser.FileName;
            ImageURL = Path.GetFileName(ImageLocalURL);
            IsImageLoaded = Visibility.Visible;
            IsPodImage = Visibility.Collapsed;
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
           Prod.Nom = Nom;
           Prod.Description = Description;
           Prod.Prix = Prix;
           var family = Famileis.FirstOrDefault(item => item.Nom == Famille);
           Prod.IDFaMil = family == null ? 1 : family.IDFaMil;
           Prod.Famille = family;
           Prod.ImageURL = ImageURL;
        }

        public void EndEdit()
        {
            UpdateProduit();
            DataProvider.InsertIfNotExists(Prod);
            if (IsImageLoaded == Visibility.Visible)
                UploadFile();

        }

        private void UploadFile()
        {
            var  client = new WebClient();
            client.UploadFile(SettingsService.GetDataRepositoryRootPath() + "images/upload.php", "POST", ImageLocalURL);
        }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Description = _oldDescription;
            Famille = _oldFamille;
            Prix = _oldPrix;
            ImageURL = _oldImageURL;
            IsImageLoaded = Visibility.Collapsed;
            IsPodImage = Visibility.Visible;
        }
    }
}