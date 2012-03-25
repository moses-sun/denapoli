using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class MenuViewModel : ProductViewModel
    {
        public MenuViewModel(Produit menu, List<Famille> menuCompostion) : base(menu)
        {
            MenuProducts = new List<MenuProductViewModel>();
            menuCompostion.ForEach(item =>
                                       {
                                           var menuProduct = new MenuProductViewModel(item, item.Produits);
                                           MenuProducts.Add(menuProduct);
                                       });
            ValidateCommand = new ActionCommand(() => NotifyChanged("Validate"));
            CancelCommand = new ActionCommand(() => NotifyChanged("Cancel"));
            EditCommand = new ActionCommand(()=>NotifyChanged("Edit"));
            IsMenu = true;
        }

        public List<MenuProductViewModel> MenuProducts { get; set; }
        public ICommand ValidateCommand { get; protected set; }
        public ICommand CancelCommand { get; protected set; }
        public ICommand EditCommand { get; protected set; }

       
    }


    public class ProductViewModel : NotifyPropertyChanged, ICommandView
    {
        public  ProductViewModel(Produit prod)
        {
            Produit = prod;
            DeleteCommand = new ActionCommand(() => NotifyChanged("Delete"));
            AddUnitCommand = new ActionCommand(() => Quantite++);
            RemoveUnitCommand = new ActionCommand(() => Quantite--);
            IsMenu = false;
        }

        private int _quantite;
        public int Quantite
        {
            get { return _quantite; }
            set
            {
                _quantite = value;
                NotifyChanged("Quantite");
                PrixTotal = Produit.Prix * _quantite;
            }
        }

        private float _prixTotal;
        public float PrixTotal
        {
            get { return _prixTotal; }
            set
            {
                _prixTotal = value;
                NotifyChanged("PrixTotal");
            }
        }

        public Produit Produit { get; set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddUnitCommand { get; protected set; }
        public ICommand RemoveUnitCommand { get; protected set; }

        public bool IsMenu { get; set; }

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