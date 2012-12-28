using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Diagnostics;

namespace Denapoli.Modules.Data.Entities
{
    [Table(Name = "langue")]
    public class Langue : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static readonly PropertyChangingEventArgs EmptyChangingEventArgs = new PropertyChangingEventArgs("");
        private string _code;
        private int _idlAng;
        private string _noM;

        [Column(Storage = "_code", Name = "CODE", DbType = "varchar(5)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                if ((_code == value)) return;
                SendPropertyChanging();
                _code = value;
                SendPropertyChanged("Code");
            }
        }

        [Column(Storage = "_idlAng", Name = "ID_LANG", DbType = "int", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public int IDLang
        {
            get
            {
                return _idlAng;
            }
            set
            {
                if ((_idlAng == value)) return;
                SendPropertyChanging();
                _idlAng = value;
                SendPropertyChanged("IDLang");
            }
        }

        [Column(Storage = "_noM", Name = "NOM", DbType = "varchar(50)", AutoSync = AutoSync.Never, CanBeNull = false)]
        [DebuggerNonUserCode]
        public string NoM
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
                SendPropertyChanged("NoM");
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            var h = PropertyChanging;
            if ((h != null))
            {
                h(this, EmptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(string propertyName)
        {
            var h = PropertyChanged;
            if ((h != null))
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
	
}