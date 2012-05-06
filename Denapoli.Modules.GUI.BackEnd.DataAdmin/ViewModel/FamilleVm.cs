using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
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

        public FamilleVm(Famille f, IDataProvider dataProvider, ILocalizationService localizationService)
        {
            Family = f;
            DataProvider = dataProvider;
            LocalizationService = localizationService;
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            FamilyProducts = new ObservableCollection<Produit>();
            ReSetProperties();
        }


        public FamilleVm()
        {
            BrowseImageCommand = new ActionCommand(BrowseImage);
            Traductions = new ObservableCollection<Traduction>();
            ReSetProperties();
        }

        private void ReSetProperties()
        {
            Nom = Family.Nom;
            Description = Family.Description;
            _imageURL = Family.ImageURL;

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
            DataProvider.InsertIfNotExists(Family);
        }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Description = _oldDescription;
            _imageURL = _oldImageURL;
        }
    }
}