using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Forms;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    public class FamilleVm : NotifyPropertyChanged, IEditableObject
    {
        public Famille Family { get; set; }
        public static IDataProvider DataProvider { get; set; }
        public static ILocalizationService LocalizationService { get; set; }
        public ObservableCollection<Traduction> Traductions { get; set; }
        public ObservableCollection<Produit> FamilyProducts { get; set; }
        public static ISettingsService SettingsService { get; set; }

        public FamilleVm(Famille f, IDataProvider dataProvider, ILocalizationService localizationService, ISettingsService settingsService)
        {
            Family = f;
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            SettingsService = settingsService;
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            FamilyProducts = new ObservableCollection<Produit>();
            ReSetProperties();
        }


        public FamilleVm()
        {
            Family = new Famille();
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            FamilyProducts = new ObservableCollection<Produit>();
            ReSetProperties();
        }

        private void ReSetProperties()
        {
            Nom = Family.Nom;
            Description = Family.Description;
            ImageURL = Family.ImageURL;

            Traductions.Clear();
            foreach (var language in LocalizationService.AvailableLangages)
            {
                Traductions.Add(new Traduction
                {
                    Langue = language.Name,
                    Nom = LocalizationService.Localize(Family.Nom, language),
                    Description = LocalizationService.Localize(Family.Description, language)
                });
            }

            FamilyProducts.Clear();
            if(Family.Produitss != null)
                Family.Produitss.ForEach(item => FamilyProducts.Add(item));
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

        public ActionCommand BrowseImageCommand { get; set; }

        private string _imageLocalURL;
        public string ImageLocalURL
        {
            get
            {
                return _imageLocalURL;
            }
            set
            {
                _imageLocalURL = value;
                NotifyChanged("ImageLocalURL");
            }
        }

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
            _oldImageURL = ImageURL;
        }

        private void UpdateProduit()
        {
           Family.Nom = Nom;
           Family.Description = Description;
           Family.ImageURL = ImageURL;
        }

        public void EndEdit()
        {
            UpdateProduit();
            DataProvider.InsertIfNotExists(Family);
            if (IsImageLoaded == Visibility.Visible)
                UploadFile();
        }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Description = _oldDescription;
            ImageURL = _oldImageURL;
            IsImageLoaded = Visibility.Collapsed;
            IsPodImage = Visibility.Visible;
        }

        private void UploadFile()
        {
            var client = new WebClient();
            client.UploadFile(SettingsService.GetDataRepositoryRootPath() + "images/upload.php", "POST", ImageLocalURL);
        }
    }
}