using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.Infrastructure.Services
{
    public class Dico : NotifyPropertyChanged
    {
        public Dico()
        {
            foreach (var prop in GetType().GetProperties())
            {
                prop.SetValue(this,prop.Name,null);
            }
        }
        public void Notify()
        {
            foreach (var prop in GetType().GetProperties())
            {
                NotifyChanged(prop.Name);
            }
           
        }

        public string TouchezLecranPourCommander { get; private set; }
        public string Produit { get; private set; }
        public string Produits { get; private set; }
        public string Quantite { get; private set; }
        public string Prix { get; private set; }
        public string PrixTotal { get; private set; }
        public string Valider { get; private set; }
        public string Annuler { get; private set; }
        public string Retour { get; private set; }
        public string SelectionnezUneFamille { get; private set; }
        public string Total { get; private set; }
        public string NumChambre { get; private set; }
        public string Num { get; private set; }
        public string Voie { get; private set; }
        public string ComplementAdresse { get; private set; }
        public string Ville { get; private set; }
        public string CodePostal { get; private set; }
        public string Nom { get; private set; }
        public string Prenom { get; private set; }
        public string Email { get; private set; }
        public string Tel { get; private set; }
        public string ComposezVotreMenu { get; private set; }
        public string ClientEtAdresseDeLivraison { get; private set; }
        public string AdresseDeLivraison { get; private set; }
        public string CoordoneesClient { get; private set; }
    }
}