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
        Langue InsertIfNotExists(Langue client);
        Adresse InsertIfNotExists(Adresse addAdresse);
        Produit InsertIfNotExists(Produit p);
        Famille InsertIfNotExists(Famille p);
        Livreur InsertIfNotExists(Livreur l);
        Produit InsertMenuIfNotExists(Produit p);
        Borne InsertIfNotExists(Borne b);

        void Delete(Client client);
        void Delete(Langue client);
        void Delete(Adresse addAdresse);
        void Delete(Produit p);
        void Delete(Famille p);
        void Delete(Livreur l);
        void DeleteMenu(Produit p);
        void Delete(Borne b);


        Livreur GetLivreurById(int idliVreUr);
    }
}