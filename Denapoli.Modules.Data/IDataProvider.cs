using System.Collections.Generic;
using Denapoli.Modules.Data.Entities;

namespace Denapoli.Modules.Data
{
    public interface IDataProvider
    {
        void Connect();
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
        void UpdateBornes(List<Borne> bornes);
        void Delete(Client client);
        void Delete(Langue client);
        void Delete(Adresse addAdresse);
        void Delete(Produit p);
        void Delete(Famille p);
        void Delete(Livreur l);
        void DeleteMenu(Produit p);
        void Delete(Borne b);
        void Delete(Commande commande);


        Livreur GetLivreurById(int idliVreUr);
    }
}