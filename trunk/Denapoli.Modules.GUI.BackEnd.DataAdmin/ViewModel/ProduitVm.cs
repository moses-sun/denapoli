using System;
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
using Denapoli.Modules.Infrastructure.Events;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
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
        public static IUpdatebale Parent { get; set; }


        public ProduitVm()
        {
            Prod = new Produit{IsActif = true,IsApp = true,IsWEB = true, Nom = "", Description = ""};
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
            FamiliesNames = new ObservableCollection<string>(Famileis.Where(item=>item.IsDeleted==0).Select(item=>item.Nom));
            ReSetProperties();
        }

        private void ReSetProperties()
        {
            Nom = Prod.Nom;
            Description = Prod.Description;
            Tva = Prod.Tva;
            IsApp = Prod.IsApp;
            IsWeb = Prod.IsWEB;
            IsActif = Prod.IsActif;
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
                    Langage = language,
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
            _oldTva = Tva;
            _oldIsApp = IsApp;
            _oldIsWeb = IsWeb;
            _oldIsActif = IsActif;
            Traductions.ForEach(item=>item.BeginEdit());
        }

        private void UpdateProduit()
        {
           Prod.Nom = Nom;
           Prod.Description = Description;
           Prod.Prix = Prix;
           Prod.Tva = Tva;
           Prod.IsWEB = IsWeb;
           Prod.IsApp = IsApp;
           Prod.IsActif = IsActif;
           var family = Famileis.FirstOrDefault(item => item.Nom == Famille);
           Prod.IDFaMil = family == null ? 1 : family.IDFaMil;
           Prod.Famille = family;
           Prod.ImageURL = ImageURL;
           Traductions.ForEach(item=>
                                   {
                                       LocalizationService.ModifyLocaLization(Prod.Nom, item.Nom, item.Langage);
                                       LocalizationService.ModifyLocaLization(Prod.Description, item.Description, item.Langage);
                                   });
        }

        public void EndEdit()
        {
            UpdateProduit();
            DataProvider.InsertIfNotExists(Prod);
            LocalizationService.SendDocs();
            if (IsImageLoaded == Visibility.Visible && !string.IsNullOrEmpty(ImageLocalURL))
                UploadFile();
            DataAdminViewModel.EventAggregator.GetEvent<UpdateEvent>().Publish(Parent);
        }

        private void UploadFile()
        {
            if (!File.Exists(ImageLocalURL)) return;
            try
            {
                var client = new WebClient();
                client.UploadFile(SettingsService.GetDataRepositoryRootPath() + "images/upload.php", "POST", ImageLocalURL);
     
            }
            catch (Exception)
            {
                var client = new WebClient();
                client.UploadFile(SettingsService.GetDataRepositoryRootPath() + "images/upload.php", "POST", ImageLocalURL);
            }
         }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Description = _oldDescription;
            Famille = _oldFamille;
            Prix = _oldPrix;
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