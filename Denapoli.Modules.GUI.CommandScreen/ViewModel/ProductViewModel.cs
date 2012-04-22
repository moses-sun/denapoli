using System.Windows;
using System.Windows.Input;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.Services;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class ProductViewModel : NotifyPropertyChanged, ICommandView
    {
        public ILocalizationService LocalizationService { get; set; }

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