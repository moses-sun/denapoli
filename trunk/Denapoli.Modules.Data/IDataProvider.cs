using System.Collections.Generic;
using Denapoli.Modules.Data.Entities;

namespace Denapoli.Modules.Data
{
    public interface IDataProvider
    {
        List<Famille> GetAvailableFamilies();
        List<Produit> GetFamilyProducts(Famille famille);
        List<Famille> GetMenuComposition(Produit menu);
        void AddCommande(Commande com);
        Client InsertIfNotExists(Client client);
        Adresse InsertIfNotExists(Adresse addAdresse);
        Borne GetBorne(int i);
    }
}