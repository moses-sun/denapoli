using System.Collections.Generic;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class MenuProductViewModel  : NotifyPropertyChanged
    {
        public MenuProductViewModel(Famille famille, IEnumerable<Produit> produits)
        {
            Family = famille;
            Produits = new List<Produit>(produits);
           
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