using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Timers;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.GUI.BackEnd.OrderProcessing.View;
using Denapoli.Modules.GUI.CommandScreen.ViewModel;
using Denapoli.Modules.Infrastructure.Command;
using Denapoli.Modules.Infrastructure.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace Denapoli.Modules.GUI.BackEnd.OrderProcessing.ViewModel
{
    [Export]
    public class OrdersProcessingViewModel : NotifyPropertyChanged
    {
        private IDataProvider DataProvider { get; set; }
        public OrdersProcessingView View { get; set; }

        [ImportingConstructor]
        public OrdersProcessingViewModel(IDataProvider dataProvider)
        {
            DataProvider = dataProvider;
            Orders = new ObservableCollection<Commande>();
           
            Products = new ObservableCollection<ProductViewModel>();
            Livreurs = new ObservableCollection<Livreur>();
            SubmitButtonCommand = new ActionCommand(Submit);

           var timer = new Timer {Interval = 6000};
            timer.Elapsed += (sender, args) =>
                                 {
                                     if (View == null) return; 
                                     View.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                                                                 new System.Windows.Threading.
                                                                     DispatcherOperationCallback(delegate
                                                                                                     {
                                                                                                         Orders.ForEach(
                                                                                                             item =>
                                                                                                             item.Chrono
                                                                                                                 --);
                                                                                                         UpdateCommandes
                                                                                                             ();
                                                                                                         return null;
                                                                                                     }), null);
                                 };
            UpdateLivreurs();
            UpdateCommandes();
            timer.Start();
        }

        private Livreur _selectedLivreur;
        public Livreur SelectedLivreur
        {
            get { return _selectedLivreur; }
            set
            {
                _selectedLivreur = value;
                NotifyChanged("SelectedLivreur");
                if (value != null && SelectedCommand != null)
                {
                    SelectedCommand.IDLiVReUR = value.IDLiVReUR;
                    SelectedCommand.Livreur = value;
                } 
            }
        }

        public ObservableCollection<Livreur> Livreurs { get; set; }

        private void UpdateLivreurs ()
        {
           var livreurs =  DataProvider.GetAllLivreurs();
            Livreurs.Clear();
            livreurs.ForEach(item => Livreurs.Add(item));
        }

        private void Submit()
        {
            switch (SelectedCommand.Statut)
            {
                case Attente:   SelectedCommand.Statut = Preperee; break;
                case Preperee:  SelectedCommand.Statut = Prete; break;
                case Prete:     SelectedCommand.Statut = Livree; break;
                case Livree: SelectedCommand.Statut = Livree; break;
            }
            SetSubmitButtonText();
            DataProvider.AddCommande(SelectedCommand);
        }

        private string _preparer = "Préparer";
        private string _prete = "Prête";
        private string _livrer = "Livrer";

        private const string Preperee = "PREPAREE";
        private const string Prete = "PRETE";
        private const string Livree = "LIVREE";
        private const string Attente = "ATTENTE";
        
        private string _submitButtonText;
        public string SubmitButtonText
        {
            get { return _submitButtonText; }
            set
            {
                _submitButtonText = value;
                NotifyChanged("SubmitButtonText");
            }
        }

        public ActionCommand SubmitButtonCommand { get; set; }

        public ObservableCollection<ProductViewModel> Products { get; set; } 

        public ObservableCollection<Commande> Orders { get; set; }

        private void UpdateCommandes()
        {
            var old = SelectedCommand == null ? -1  : SelectedCommand.Num;
            //Orders.Clear();
            DataProvider.GetMenuAllCommandes().ForEach(item =>
                                                           {
                                                               var exist = Orders.FirstOrDefault(e => e.Num == item.Num);
                                                               var diff = DateTime.Now - item.Date;
                                                               var minDiff = diff != null ? diff.Value.Minutes + diff.Value.Hours * 60 + diff.Value.Days * 24 * 60 : 45;
                                                               item.Chrono = 45 - minDiff;
                                                               if(exist != null)
                                                               {
                                                                   exist.Chrono = item.Chrono;
                                                                   exist.Livreur = item.Livreur;
                                                                   exist.IDLiVReUR = item.IDLiVReUR;
                                                                   exist.Statut = item.Statut;
                                                                   if (item.Statut == Livree) Orders.Remove(exist);
                                                               }
                                                               else if(item.Statut != Livree)
                                                                    Orders.Add(item);
                                                           });
            SelectedCommand = Orders.FirstOrDefault(item => item.Num == old);
            SelectedCommand = SelectedCommand ?? Orders.FirstOrDefault();
        }

        private Commande _selectedCommand;
        public Commande SelectedCommand
        {
            get { return _selectedCommand; }
            set
            {
                _selectedCommand = value;
                NotifyChanged("SelectedCommand");
                Updateproducts();
                if (value == null) return;
                SelectedLivreur = DataProvider.GetLivreurById(value.IDLiVReUR ?? 0);
                SetSubmitButtonText();
            }
        }

        private void Updateproducts()
        {
            Products.Clear();
            if (SelectedCommand == null) return;
            foreach (var produit in SelectedCommand.ProduitsCommande)
            {
                if (produit.Produit.IsMenu)
                {
                    var menu = new MenuVM(produit.Produit){Quantite = produit.Quantite};
                    Products.Add(menu);
                }
                Products.Add(new ProductViewModel(produit.Produit) { Quantite = produit .Quantite});
            }
            foreach (var menu in SelectedCommand.Menus)
            {
                var m = new MenuVM(menu.Produit) { Quantite = 1 };
                foreach (var comp in menu.ProduitsMenu)
                {
                    m.Composition.Add(new ProductViewModel(comp.Produit){Quantite = comp.Quantite});
                }
                Products.Add(m);
            }
        }

        private void SetSubmitButtonText()
        {
            switch (SelectedCommand.Statut)
            {
                case Attente: SubmitButtonText = _preparer; break;
                case Preperee: SubmitButtonText = _prete; break;
                case Prete: SubmitButtonText = _livrer; break;
                case Livree: SubmitButtonText = _livrer; break;
            }
        }
    }

  

    public class MenuVM : ProductViewModel
    {
        public MenuVM(Produit prod ) : base(prod)
        {
           Composition = new ObservableCollection<ProductViewModel>();
           
        }

        public ObservableCollection<ProductViewModel> Composition { get; private set; } 
    }
}
