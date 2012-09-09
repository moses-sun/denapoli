using System.Windows.Input;
using Denapoli.Modules.Infrastructure.ViewModel;

namespace Denapoli.Modules.Infrastructure.Controls.Keyboard
{
    public class VirtualKey : NotifyPropertyChanged
    {
        public VirtualKey(string noshift, string shift, string alt)
        {
            ShiftValue = shift;
            NoShiftValue = noshift;
            AltValue = alt;
            Value = NoShiftValue;
            Command = new RoutedUICommand();
        }

        public VirtualKey(string noshift)
            : this(noshift, "", "")
        {

        }

        public VirtualKey(string noshift, string shift)
            : this(noshift, shift, "")
        {

        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyChanged("Value");
            }
        }

        private string _shiftValue;
        public string ShiftValue
        {
            get { return _shiftValue; }
            set
            {
                _shiftValue = value;
                NotifyChanged("ShiftValue");
            }
        }

        private string _noShiftValue;
        public string NoShiftValue
        {
            get { return _noShiftValue; }
            set
            {
                _noShiftValue = value;
                NotifyChanged("NoShiftValue");
            }
        }

        private string _altValue;
        public string AltValue
        {
            get { return _altValue; }
            set
            {
                _altValue = value;
                NotifyChanged("AltValue");
            }
        }

        public RoutedUICommand Command { get; set; }

        private double _noShiftSize;
        public double NoShiftSize
        {
            get { return _noShiftSize; }
            set
            {
                _noShiftSize = value;
                NotifyChanged("NoShiftSize");
            }
        }


        private double _shiftSize;
        public double ShiftSize
        {
            get { return _shiftSize; }
            set
            {
                _shiftSize = value;
                NotifyChanged("ShiftSize");
            }
        }


        private double _altSize;
        public double AltSize
        {
            get { return _altSize; }
            set
            {
                _altSize = value;
                NotifyChanged("AltSize");
            }
        }
    }
}