using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    public class MenuVm  : NotifyPropertyChanged, IEditableObject
    {
        public Produit Menu { get; set; }
        public static IDataProvider DataProvider { get; set; }
        public static ILocalizationService LocalizationService { get; set; }
        public static ISettingsService SettingsService { get; set; }
        public ObservableCollection<Traduction> Traductions { get; set; }
       

        public MenuVm()
        {
            Menu = new Produit();
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            MenuComposition = new ObservableCollection<MenuCompositionn>();
            ReSetProperties();
        }


        public MenuVm(Produit menu, IDataProvider dataProvider, ILocalizationService localizationService, ISettingsService settingsService)
        {
            Menu = menu;
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            SettingsService = settingsService;
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            MenuComposition = new ObservableCollection<MenuCompositionn>();
            ReSetProperties();
        }

        private void OnMenusChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    MessageBox.Show("menu comp added");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    MessageBox.Show("menu comp removed");
                    break;
            }
        }

        private void ReSetProperties()
        {
            MenuComposition.CollectionChanged -= OnMenusChanged;

            Nom = Menu.Nom;
            Description = Menu.Description;
            Prix = Menu.Prix;
            ImageURL = Menu.ImageURL;
            FamiliesNames = new ObservableCollection<string>(DataProvider.GetAvailableFamilies().Select(item=>item.Nom));
            Traductions.Clear();
            foreach (var language in LocalizationService.AvailableLangages)
            {
                Traductions.Add(new Traduction
                {
                    Langue = language.Name,
                    Nom = LocalizationService.Localize(Menu.Nom, language),
                    Description = LocalizationService.Localize(Menu.Description, language)
                });
            }
             MenuComposition.Clear();
             Menu.ProduitComposition.ForEach(comp => MenuComposition.Add(new MenuCompositionn { Famille = comp.Famille, Quantite = comp.Quantite ?? 1, FamiliesNames = FamiliesNames }));
             MenuComposition.CollectionChanged += OnMenusChanged;
        }

        private List<MenuCompositionn> _oldMenuComposition = new List<MenuCompositionn>();
        public ObservableCollection<MenuCompositionn> MenuComposition { get; set; }

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
            _oldPrix = Prix;
            _oldImageURL = ImageURL;
            _oldMenuComposition = new List<MenuCompositionn>(MenuComposition);
        }

        private void UpdateProduit()
        {
            Menu.Nom = Nom;
            Menu.Description = Description;
            Menu.Prix = Prix;
            Menu.ImageURL = ImageURL;           
            Menu.ProduitComposition.Clear();
            var families = DataProvider.GetAvailableFamilies();

            MenuComposition.ForEach(item=>
                                        {
                                            var famille = families.FirstOrDefault(e => e.Nom == item.FamilyName);
                                            if (famille == null) return;
                                            Menu.ProduitComposition.Add(new ProduitComposition
                                            {
                                                IDProd = Menu.IDProd,
                                                IDFaMil = famille.IDFaMil,
                                                Quantite = item.Quantite
                                            });
                                        });

           
        }

        public void EndEdit()
        {
            UpdateProduit();
            DataProvider.InsertMenuIfNotExists(Menu);
            if (IsImageLoaded == Visibility.Visible)
                UploadFile();
        }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Description = _oldDescription;
            Prix = _oldPrix;
            ImageURL = _oldImageURL;
            IsImageLoaded = Visibility.Collapsed;
            IsPodImage = Visibility.Visible;
            MenuComposition.Clear();
            _oldMenuComposition.ForEach(item => MenuComposition.Add(item));
        }

        private void UploadFile()
        {
            var client = new WebClient();
            var result = client.UploadFile(SettingsService.GetDataRepositoryRootPath() + "images/upload.php", "POST", ImageLocalURL);
            var s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
            MessageBox.Show(s);
        }
    }

    public class MenuCompositionn : NotifyPropertyChanged
    {
        private Famille _famille;
        public Famille Famille
        {
            get { return _famille; }
            set
            {
                _famille = value;
                FamilyName = value.Nom;
            }
        }

        private string _familyName;
        public string FamilyName
        {
            get { return _familyName; }
            set
            {
                _familyName = value;
                NotifyChanged("FamilyName");
            }
        }

        private int _quantite;
        public int Quantite
        {
            get { return _quantite; }
            set
            {
                _quantite = value;
                NotifyChanged("Quantite");
            }
        }


        public ObservableCollection<String> FamiliesNames { get; set; } 
    }
}