using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Forms;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
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
        public static IWebService WEBService { get; set; }
        public static IUpdatebale Parent { get; set; }


        public FamilleVm(Famille f, IDataProvider dataProvider, ILocalizationService localizationService, ISettingsService settingsService, IWebService webService)
        {
            Family = f;
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            SettingsService = settingsService;
            WEBService = webService;
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            FamilyProducts = new ObservableCollection<Produit>();
            ReSetProperties();
        }


        public FamilleVm()
        {
            Family = new Famille { IsActif = true, IsApp = true, IsWEB = true, Nom = "", Description = "" };
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
            Tva = Family.Tva;
            IsWeb = Family.IsWEB;
            IsApp = Family.IsApp;
            IsActif = Family.IsActif;
            IsImageLoaded = Visibility.Collapsed;
            IsPodImage = Visibility.Visible;
            Traductions.Clear();
            foreach (var language in LocalizationService.AvailableLangages)
            {
                Traductions.Add(new Traduction
                {
                    Langue = language.Name,
                    Langage = language,
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

        private float _oldTva;
        private float _tva;
        public float Tva
        {
            get { return _tva; }
            set
            {
                _tva = value;
                NotifyChanged("Tva");
            }
        }

        private bool _oldIsApp;
        private bool _isApp;
        public bool IsApp
        {
            get { return _isApp; }
            set
            {
                _isApp = value;
                NotifyChanged("IsApp");
            }
        }

        private bool _oldIsWeb;
        private bool _isWeb;
        public bool IsWeb
        {
            get { return _isWeb; }
            set
            {
                _isWeb = value;
                NotifyChanged("IsWeb");
            }
        }

        private bool _oldIsActif;
        private bool _isActif;
        public bool IsActif
        {
            get { return _isActif; }
            set
            {
                _isActif = value;
                NotifyChanged("IsActif");
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
            _oldTva = Tva;
            _oldIsApp = IsApp;
            _oldIsWeb = IsWeb;
            _oldImageURL = ImageURL;
            _oldIsActif = IsActif;
            Traductions.ForEach(item => item.BeginEdit());
        }

        private void UpdateProduit()
        {
           Family.Nom = Nom;
           Family.Tva = Tva;
           Family.IsWEB = IsWeb;
           Family.IsApp = IsApp;
           Family.IsActif = IsActif;
           Family.Description = Description;
           Family.ImageURL = ImageURL;
           Traductions.ForEach(item =>
           {
               LocalizationService.ModifyLocaLization(Family.Nom, item.Nom, item.Langage);
               LocalizationService.ModifyLocaLization(Family.Description, item.Description, item.Langage);
           });
        }

        public void EndEdit()
        {
            UpdateProduit();
            DataProvider.InsertIfNotExists(Family);
            LocalizationService.SendDocs();
            if (IsImageLoaded == Visibility.Visible)
                WEBService.UploadFile(SettingsService.GetDataRepositoryRootPath() + "images/upload.php", ImageLocalURL);

            DataAdminViewModel.EventAggregator.GetEvent<UpdateEvent>().Publish(Parent);

        }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Description = _oldDescription;
            Tva = _oldTva;
            IsApp = _oldIsApp;
            IsWeb = _oldIsWeb;
            IsActif = _oldIsActif;
            ImageURL = _oldImageURL;
            IsImageLoaded = Visibility.Collapsed;
            IsPodImage = Visibility.Visible;
            Traductions.ForEach(item => item.CancelEdit());
        }

       
    }
}