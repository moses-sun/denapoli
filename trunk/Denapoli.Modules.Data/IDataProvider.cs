using System.Collections.Generic;
using Denapoli.Modules.Data.Entities;

namespace Denapoli.Modules.Data
{
    public interface IDataProvider
    {
        List<Famille> GetAvailableFamilies();
        List<Produit> GetAllProducts();
        List<Produit> GetFamilyProducts(Famille famille);
        List<ProduitComposition> GetMenuComposition(Produit menu);
        List<Commande> GetMenuAllCommandes();
        List<Livreur> GetAllLivreurs();
        List<Borne> GetAllBornes();
        List<Langue> GetAvailableLanguages();

        List<Commande> GetMenuTodayCommandes();
        Borne GetBorne(int i);

        Commande AddCommande(Commande com);

        Client InsertIfNotExists(Client client);
        Langue InsertIfNotExists(Langue client);
        Adresse InsertIfNotExists(Adresse addAdresse);
        Produit InsertIfNotExists(Produit p);
        Famille InsertIfNotExists(Famille f);
        Livreur InsertIfNotExists(Livreur l);
        Produit InsertMenuIfNotExists(Produit p);
        Borne InsertIfNotExists(Borne b);
        bool UpdateBornes(List<Borne> bornes);
        bool Delete(Client client);
        bool Delete(Langue client);
        bool Delete(Adresse addAdresse);
        bool Delete(Produit p);
        bool Delete(Famille p);
        bool Delete(Livreur l);
        bool DeleteMenu(Produit p);
        bool Delete(Borne b);
        bool Delete(Commande commande);


        Livreur GetLivreurById(int idliVreUr);
    }
}