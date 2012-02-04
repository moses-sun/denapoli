using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using DbLinq.Data.Linq;

namespace Denapoli.Modules.Data.Entities
{
    [Table(Name="denapoli.client")]
    public sealed class Client : INotifyPropertyChanging, INotifyPropertyChanged
    {
		
        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private string _email;
        private int _idclIen;
        private string _nom;
        private string _prenom;
        private string _tel;
        private EntitySet<Commande> _commandes;
		
        public Client()
        {
            _commandes = new EntitySet<Commande>(CommandEAttach, CommandEDetach);
        }
		
        [Column(Storage="_email", Name="EMAIL", DbType="varchar(100)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (((_email == value) == false))
                {
                    SendPropertyChanging();
                    _email = value;
                    SendPropertyChanged("Email");
                }
            }
        }
		
        [Column(Storage="_idclIen", Name="ID_CLIEN", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
        [DebuggerNonUserCode]
        public int IDCLien
        {
            get
            {
                return _idclIen;
            }
            set
            {
                if ((_idclIen == value)) return;
                SendPropertyChanging();
                _idclIen = value;
                SendPropertyChanged("IDCLien");
            }
        }
		
        [Column(Storage="_noM", Name="NOM", DbType="varchar(50)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string Nom
        {
            get
            {
                return _nom;
            }
            set
            {
                if (((_nom == value) == false))
                {
                    SendPropertyChanging();
                    _nom = value;
                    SendPropertyChanged("Nom");
                }
            }
        }
		
        [Column(Storage="_preNoM", Name="PRENOM", DbType="varchar(50)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string Prenom
        {
            get
            {
                return _prenom;
            }
            set
            {
                if ((_prenom == value)) return;
                SendPropertyChanging();
                _prenom = value;
                SendPropertyChanged("Prenom");
            }
        }
		
        [Column(Storage="_teL", Name="TEL", DbType="varchar(15)", AutoSync=AutoSync.Never)]
        [DebuggerNonUserCode]
        public string Tel
        {
            get
            {
                return _tel;
            }
            set
            {
                if ((_tel == value)) return;
                SendPropertyChanging();
                _tel = value;
                SendPropertyChanged("Tel");
            }
        }
		
        #region Children
        [Association(Storage="_commandes", OtherKey="IDCLien", ThisKey="IDCLien", Name="client_qui_commande")]
        [DebuggerNonUserCode]
        public EntitySet<Commande> Commandes
        {
            get
            {
                return _commandes;
            }
            set
            {
                _commandes = value;
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
        private void CommandEAttach(Commande entity)
        {
            SendPropertyChanging();
            entity.Client = this;
        }
		
        private void CommandEDetach(Commande entity)
        {
            SendPropertyChanging();
            entity.Client = null;
        }
        #endregion
    }
}