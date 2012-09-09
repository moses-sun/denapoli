using System.ComponentModel;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.GUI.BackEnd.DataAdmin.ViewModel
{
    public class DicoEntry : NotifyPropertyChanged,  IEditableObject
    {
        private string _key = "";
        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                NotifyChanged("Key");
            }
        }

        private string _oldValue = "";
        private string _value = "";
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyChanged("Value");
            }
        }

        public void BeginEdit()
        {
            _oldValue = Value;
        }

        public void EndEdit()
        {
           
        }

        public void CancelEdit()
        {
            Value = _oldValue;
        }
    }
}