using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using DbLinq.Data.Linq;

namespace Denapoli.Modules.Data.Entities
{
    [Table(Name="denapoli.famille")]
    public sealed class Famille : INotifyPropertyChanging, INotifyPropertyChanged
    {
		
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private int _idfAMil;
        private string _imageUrl;
        private string _noM;
        private EntitySet<ProduitComposition> _proDuiTcOmposition;
        private EntitySet<Produit> _produits;
		
        public Famille()
        {
            _proDuiTcOmposition = new EntitySet<ProduitComposition>(ProDuiTCompositionAttach, ProDuiTCompositionDetach);
            _produits = new EntitySet<Produit>(ProDuiTAttach, ProDuiTDetach);
        }
		
        [Column(Storage="_idfAMil", Name="ID_FAMIL", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int IDFaMil
        {
            get
            {
                return _idfAMil;
            }
            set
            {
                if ((_idfAMil == value)) return;
                SendPropertyChanging();
                _idfAMil = value;
                SendPropertyChanged("IDFaMil");
            }
        }
		
        [Column(Storage="_imageUrl", Name="IMAGE_URL", DbType="varchar(100)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string ImageURL
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                if ((_imageUrl == value)) return;
                SendPropertyChanging();
                _imageUrl = value;
                SendPropertyChanged("ImageURL");
            }
        }
		
        [Column(Storage="_noM", Name="NOM", DbType="varchar(100)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string Nom
        {
            get
            {
                return _noM;
            }
            set
            {
                if ((_noM == value)) return;
                SendPropertyChanging();
                _noM = value;
                SendPropertyChanged("Nom");
            }
        }
		
        #region Children
        [Association(Storage="_proDuiTcOmposition", OtherKey="IDFaMil", ThisKey="IDFaMil", Name="familles_produit")]
        [DebuggerNonUserCode]
        public EntitySet<ProduitComposition> ProduitComposition
        {
            get
            {
                return _proDuiTcOmposition;
            }
            set
            {
                _proDuiTcOmposition = value;
            }
        }
		
        [Association(Storage="_proDuiT", OtherKey="IDFaMil", ThisKey="IDFaMil", Name="famille_de_produit")]
        [DebuggerNonUserCode]
        public EntitySet<Produit> Produits
        {
            get
            {
                return _produits;
            }
            set
            {
                _produits = value;
            }
        }
        #endregion
		
        public event PropertyChangingEventHandler PropertyChanging;
		
        public event PropertyChangedEventHandler PropertyChanged;

        private void SendPropertyChanging()
        {
            var h = PropertyChanging;
            if ((h != null))
            {
                h(this, EmptyChangingEventArgs);
            }
        }

        private void SendPropertyChanged(string propertyName)
        {
            var h = PropertyChanged;
            if ((h != null))
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }
		
        #region Attachment handlers
        private void ProDuiTCompositionAttach(ProduitComposition entity)
        {
            SendPropertyChanging();
            entity.Famille = this;
        }
		
        private void ProDuiTCompositionDetach(ProduitComposition entity)
        {
            SendPropertyChanging();
            entity.Famille = null;
        }
		
        private void ProDuiTAttach(Produit entity)
        {
            SendPropertyChanging();
            entity.Famille = this;
        }
		
        private void ProDuiTDetach(Produit entity)
        {
            SendPropertyChanging();
            entity.Famille = null;
        }
        #endregion
    }
}