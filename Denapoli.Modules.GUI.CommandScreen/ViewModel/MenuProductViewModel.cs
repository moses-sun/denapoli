using System.Collections.Generic;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class MenuProductViewModel  : NotifyPropertyChanged
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

    }
}