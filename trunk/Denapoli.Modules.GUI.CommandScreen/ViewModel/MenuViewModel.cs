using System.Collections.Generic;
using System.Windows.Input;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;

namespace Denapoli.Modules.GUI.CommandScreen.ViewModel
{
    public class MenuViewModel : ProductViewModel
    {
        public MenuViewModel(Produit menu, List<ProduitComposition> menuCompostion) : base(menu)
        {
            MenuProducts = new List<MenuProductViewModel>();
            menuCompostion.ForEach(item =>
                                       {
                                           if (item.Famille.Produitss.Count <= 0) return;
                                           var quantite = item.IsMeme ? 1 : item.Quantite;
                                           for (var i = 0; i < quantite; i++)
                                           {
                                               var menuProduct = new MenuProductViewModel(item);
                                               MenuProducts.Add(menuProduct);
                                           }
                                       });
            ValidateCommand = new ActionCommand(() => NotifyChanged("Validate"));
            CancelCommand = new ActionCommand(() => NotifyChanged("Cancel"));
            EditCommand = new ActionCommand(()=>NotifyChanged("Edit"));

            AddUnitCommand = new ActionCommand(() =>
                                                   {
                                                       Quantite++;
                                                       MenuProducts.ForEach(p => p.Quantite = p.QuantiteUnitaire*Quantite);
                                                   });
            RemoveUnitCommand = new ActionCommand(() =>
                                                      {
                                                          Quantite--;
                                                          if (Quantite < 1) Quantite = 1;
                                                          MenuProducts.ForEach(p => p.Quantite = p.QuantiteUnitaire * Quantite);

                                                      });
            IsMenu = true;
        }

        public List<MenuProductViewModel> MenuProducts { get; set; }
        public ICommand ValidateCommand { get; protected set; }
        public ICommand CancelCommand { get; protected set; }
        public ICommand EditCommand { get; protected set; }

       
    }
}