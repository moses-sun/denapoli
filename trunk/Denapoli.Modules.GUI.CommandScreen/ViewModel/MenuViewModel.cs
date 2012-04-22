using System.Collections.Generic;
using System.Windows.Input;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.Command;

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
}