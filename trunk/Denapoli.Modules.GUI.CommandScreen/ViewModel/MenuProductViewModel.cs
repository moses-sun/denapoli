using System.Collections.Generic;
using System.Windows;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class MenuProductViewModel  : NotifyPropertyChanged, ICommandView
    {
        public MenuProductViewModel(Famille famille, IEnumerable<Produit> produits)
        {
            Family = famille;
            Produits = new List<Produit>(produits);
            produits.ForEach(item => Produits.Add(item));
            produits.ForEach(item => Produits.Add(item));
            produits.ForEach(item => Produits.Add(item));
            produits.ForEach(item => Produits.Add(item));
            produits.ForEach(item => Produits.Add(item));
            produits.ForEach(item => Produits.Add(item));

        }

        public ILocalizationService LocalizationService { get; set; }
        public Famille Family { get; set; }
        public List<Produit> Produits { get; set; }

        private Produit _selectedProduct;
        public Produit Produit
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyChanged("Produit");
            }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyChanged("Visibility");
            }
        }
        public Visibility Visibility
        {
            get { return IsVisible ? Visibility.Visible : Visibility.Collapsed; }

        }
    }
}