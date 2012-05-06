using System.Collections.Generic;
using Denapoli.Modules.Data.Entities;

namespace Denapoli.Modules.Data
{
    public interface IDataProvider
    {
        List<Famille> GetAvailableFamilies();
        List<Produit> GetAllProducts();
        List<Produit> GetFamilyProducts(Famille famille);
        List<Famille> GetMenuComposition(Produit menu);
        List<Commande> GetMenuAllCommandes();
        List<Livreur> GetAllLivreurs();
        List<Borne> GetAllBornes();
        List<Langue> GetAvailableLanguages();

        List<Commande> GetMenuTodayCommandes();
        Borne GetBorne(int i);

        void AddCommande(Commande com);

        Client InsertIfNotExists(Client client);
        Adresse InsertIfNotExists(Adresse addAdresse);
        Produit InsertIfNotExists(Produit p);
        Famille InsertIfNotExists(Famille p);
        Livreur InsertIfNotExists(Livreur l);
        Borne InsertIfNotExists(Borne b);

    }
}