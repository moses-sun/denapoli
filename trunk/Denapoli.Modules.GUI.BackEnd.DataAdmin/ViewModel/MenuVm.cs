using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
        public ObservableCollection<Traduction> Traductions { get; set; }
        public ObservableCollection<MenuComposition> MenuComposition { get; set; }

        public MenuVm()
        {
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            MenuComposition = new ObservableCollection<MenuComposition>();
            ReSetProperties();
        }


        public MenuVm(Produit menu, IDataProvider dataProvider, ILocalizationService localizationService)
        {
            Menu = menu;
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            MenuComposition = new ObservableCollection<MenuComposition>();
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
            _imageURL = Menu.ImageURL;

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
             Menu.ProduitComposition.ForEach(comp => MenuComposition.Add(new MenuComposition{Famille = comp.Famille, Quantite = comp.Quantite ?? 1}));
             MenuComposition.CollectionChanged += OnMenusChanged;
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
            DataProvider.InsertIfNotExists(Menu);
        }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Description = _oldDescription;
            Prix = _oldPrix;
            _imageURL = _oldImageURL;
        }
    }

    public class MenuComposition : NotifyPropertyChanged
    {
        public Famille Famille { get; set; }
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
    }
}