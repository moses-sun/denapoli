using System;
using System.ComponentModel;
using Denapoli.Modules.Data;
using Denapoli.Modules.Data.Entities;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    public class LivreurVm : NotifyPropertyChanged, IEditableObject
    {
        public Livreur Livreur { get; set; }
        public static IDataProvider DataProvider { get; set; }

        public LivreurVm()
        {
            Livreur = new Livreur();
            ReSetProperties();
        }

        public LivreurVm(Livreur l, IDataProvider dataProvider)
        {
            Livreur = l;
            DataProvider = dataProvider;
            ReSetProperties();
        }

        private void ReSetProperties()
        {
            Nom = Livreur.NoM;
            Prenom = Livreur.PreNoM;
        }

       

        private string _oldNom;
        private string _nom;
        public string Nom
        {
            get { return _nom; }
            set
            {
                _nom = value;
                NotifyChanged("Nom");
            }
        }

        private string _oldPrenom;
        private string _prenom;
        public string Prenom
        {
            get { return _prenom; }
            set
            {
                _prenom = value;
                NotifyChanged("Prenom");
            }
        }

     

        public void BeginEdit()
        {
            _oldNom = Nom;
            _oldPrenom = Prenom;
        }

        private void UpdateProduit()
        {
           /* Prod.Nom = Nom;
            Prod.Description = Description;
            Prod.Prix = Prix;
            var family = Famileis.FirstOrDefault(item => item.Nom == Famille);
            Prod.IDFaMil = family == null ? 1 : family.IDFaMil;
            Prod.Famille = family;*/
        }

        public void EndEdit()
        {
            UpdateProduit();
            DataProvider.InsertIfNotExists(Livreur);
        }

        public void CancelEdit()
        {
            Nom = _oldNom;
            Prenom = _oldPrenom;
        }
    }
}