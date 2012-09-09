using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Denapoli.Modules.Infrastructure.Controls.Keyboard
{
    /// <summary>
    /// Interaction logic for VirtualKeyboard.xaml
    /// </summary>
    public partial class VirtualKeyboard : INotifyPropertyChanged
    {
        public VirtualKeyboard()
        {
            WidthTouchKeyboard = 830;
            Width = WidthTouchKeyboard;
            SetCommandBinding();
            InitializeComponent();
            DataContext = this;
            UpdateCase();
        }


        #region Properties

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                NotifyChanged("Text");
            }
        }

       
        public static readonly VirtualKey key_2 = new VirtualKey("","²");
        public static readonly VirtualKey Key1 = new VirtualKey("1", "&");
        public static readonly VirtualKey Key2 = new VirtualKey("2", "é", "~");
        public static readonly VirtualKey Key3 = new VirtualKey("3", "\"", "#");
        public static readonly VirtualKey Key4 = new VirtualKey("4", "'", "{");
        public static readonly VirtualKey Key5 = new VirtualKey("5", "(", "[");
        public static readonly VirtualKey Key6 = new VirtualKey("6", "-", "|");
        public static readonly VirtualKey Key7 = new VirtualKey("7", "è", "`");
        public static readonly VirtualKey Key8 = new VirtualKey("8", "_", "\\");
        public static readonly VirtualKey Key9 = new VirtualKey("9", "ç", "^");
        public static readonly VirtualKey Key0 = new VirtualKey("0", "à", "@");
        public static readonly VirtualKey KeyDeg = new VirtualKey("°", ")", "]");
        public static readonly VirtualKey KeyPlus = new VirtualKey("+", "=", "}");
        public static readonly VirtualKey KeyBackspace = new VirtualKey("Backspace");

        public static readonly List<VirtualKey> FirstRow = new List<VirtualKey> { key_2, Key1, Key2, Key3, Key4, Key5, Key6, Key7, Key8, Key9, Key0, KeyDeg, KeyPlus, KeyBackspace }; 


        public static readonly VirtualKey KeyTab = new VirtualKey("Tab");
        public static readonly VirtualKey KeyA = new VirtualKey("A");
        public static readonly VirtualKey KeyZ = new VirtualKey("Z");
        public static readonly VirtualKey KeyE = new VirtualKey("E");
        public static readonly VirtualKey KeyR = new VirtualKey("R");
        public static readonly VirtualKey KeyT = new VirtualKey("T");
        public static readonly VirtualKey KeyY = new VirtualKey("Y");
        public static readonly VirtualKey KeyU = new VirtualKey("U");
        public static readonly VirtualKey KeyI = new VirtualKey("I");
        public static readonly VirtualKey KeyO = new VirtualKey("O");
        public static readonly VirtualKey KeyP = new VirtualKey("P");
        public static readonly VirtualKey KeyChap = new VirtualKey("¨","^" );
        public static readonly VirtualKey KeyDollar = new VirtualKey("£","$", "¤");

        public static readonly List<VirtualKey> SecondRow = new List<VirtualKey> { KeyTab, KeyA, KeyZ, KeyE, KeyR, KeyT, KeyY, KeyU, KeyI, KeyO, KeyP, KeyChap, KeyDollar, KeyPlus }; 



        public static readonly VirtualKey KeyCapsLock = new VirtualKey("CapsLock");
        public static readonly VirtualKey KeyQ = new VirtualKey("Q");
        public static readonly VirtualKey KeyS = new VirtualKey("S");
        public static readonly VirtualKey KeyD = new VirtualKey("D");
        public static readonly VirtualKey KeyF = new VirtualKey("F");
        public static readonly VirtualKey KeyG = new VirtualKey("G");
        public static readonly VirtualKey KeyH = new VirtualKey("H");
        public static readonly VirtualKey KeyJ = new VirtualKey("J");
        public static readonly VirtualKey KeyK = new VirtualKey("K");
        public static readonly VirtualKey KeyL = new VirtualKey("L");
        public static readonly VirtualKey KeyM = new VirtualKey("M");
        public static readonly VirtualKey KeyPercent = new VirtualKey("%","ù");
        public static readonly VirtualKey Keymico = new VirtualKey("µ","*");
        public static readonly VirtualKey KeyEnter = new VirtualKey("Enter");

        public static readonly List<VirtualKey> ThirdRow = new List<VirtualKey> { KeyCapsLock, KeyQ, KeyS, KeyD, KeyF, KeyG, KeyH, KeyJ, KeyK, KeyL, KeyM, KeyPercent, Keymico, KeyEnter }; 



        public static readonly VirtualKey KeyShift = new VirtualKey("Shift");
        public static readonly VirtualKey KeyBalise = new VirtualKey(">","<");
        public static readonly VirtualKey KeyW = new VirtualKey("W");
        public static readonly VirtualKey KeyX = new VirtualKey("X");
        public static readonly VirtualKey KeyC = new VirtualKey("C");
        public static readonly VirtualKey KeyV = new VirtualKey("V");
        public static readonly VirtualKey KeyB = new VirtualKey("B");
        public static readonly VirtualKey KeyN = new VirtualKey("N");
        public static readonly VirtualKey KeyQuestion = new VirtualKey("?",",");
        public static readonly VirtualKey KeyDot = new VirtualKey(".",";");
        public static readonly VirtualKey KeySlash = new VirtualKey("/",":");
        public static readonly VirtualKey KeyParagraph = new VirtualKey("§","!");

        public static readonly List<VirtualKey> FourthRow = new List<VirtualKey> { KeyShift, KeyBalise, KeyW, KeyX, KeyC, KeyV, KeyB, KeyN, KeyQuestion, KeyDot, KeySlash, KeyParagraph }; 

        public static readonly VirtualKey KeySpaceBar = new VirtualKey("");
        public static readonly VirtualKey KeyClear = new VirtualKey("Clear");
        public static readonly VirtualKey KeyAlt = new VirtualKey("AltGr");

        public static readonly List<VirtualKey> FifthRow = new List<VirtualKey> {KeySpaceBar, KeyClear, KeyAlt}; 

        public static readonly List<VirtualKey> Alpha = new List<VirtualKey> {KeyA, KeyZ, KeyE, KeyR, KeyT, KeyY, KeyU, KeyI, KeyO, KeyP, KeyQ, KeyS, KeyD, KeyF, KeyG, KeyH, KeyJ, KeyK, KeyL, KeyM, KeyW, KeyX, KeyC, KeyV, KeyB, KeyN};
        public static readonly List<VirtualKey> ShiftAndAlt = new List<VirtualKey> { key_2, Key0, Key1, Key2, Key3, Key4, Key5, Key6, Key7, Key8, Key9, KeyDeg, Key0, KeyPlus, KeyChap, KeyDollar, KeyPercent, Keymico, KeyQuestion, KeyDot, KeySlash, KeyParagraph, KeyBalise };

        public static readonly List<VirtualKey> All = MergeAll();

        private static List<VirtualKey> MergeAll()
        {
            var list = new List<VirtualKey>();
            list.AddRange(FirstRow);
            list.AddRange(SecondRow);
            list.AddRange(ThirdRow);
            list.AddRange(FourthRow);
            list.AddRange(FifthRow);
            return list;
        }


        public double WidthTouchKeyboard { get; set; }

        private bool _shiftFlag;
        public bool ShiftFlag
        {
            get { return _shiftFlag; }
            set
            {
                if (value == _shiftFlag) return;
                _shiftFlag = value;
                UpdateCase();
                NotifyChanged("ShiftFlag");

            }
        }

        private bool _capsLockFlag;
        public bool CapsLockFlag
        {
            get { return _capsLockFlag; }
            set
            {
                if (value == _capsLockFlag) return;
                _capsLockFlag = value;
                UpdateCase();
                NotifyChanged("CapsLockFlag");
            }
        }

        private const double Small = 14;
        private const double Big = 18;

        private bool _altFlag;
        public bool AltFlag
        {
            get { return _altFlag; }
            set
            {
                if (value == _altFlag) return;
                _altFlag = value;
                UpdateAlt();
                NotifyChanged("AltFlag");
            }
        }

        

        #endregion

        #region CommandRelatedCode

        private void SetCommandBinding()
        {
            All.ForEach(key => CommandManager.RegisterClassCommandBinding(typeof (VirtualKeyboard), new CommandBinding(key.Command, RunCommand)));
        }

        private void RunCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == KeyCapsLock.Command)CapsLockFlag = !CapsLockFlag;
            else if(e.Command == KeyShift.Command)ShiftFlag = !ShiftFlag;
            else if(e.Command == KeyAlt.Command)AltFlag = !AltFlag;
            else if(e.Command == KeyTab.Command)Text += "     ";
            else if(e.Command == KeyBackspace.Command)
            {
                if (!string.IsNullOrEmpty(Text))
                    Text = Text.Substring(0, Text.Length - 1);
            }
            else if(e.Command == KeyEnter.Command)Hide();
            else if(e.Command == KeyClear.Command)Text = "";
            else
            {
                var key = All.First(item => item.Command == e.Command);
                Text += key.Value ;
                AltFlag = false;
                ShiftFlag = false;
            }
        }

        private void UpdateCase()
        {
            if(CapsLockFlag || ShiftFlag)
            {
                Alpha.ForEach(a => a.Value = a.NoShiftValue.ToUpper());
                ShiftAndAlt.ForEach(sa =>
                {
                    sa.NoShiftSize = Small;
                    sa.AltSize = Small;
                    sa.ShiftSize = Big;
                    sa.Value = sa.ShiftValue;
                });
            }
            else
            {
                Alpha.ForEach(a => a.Value = a.NoShiftValue.ToLower());
                ShiftAndAlt.ForEach(sa =>
                {
                    sa.NoShiftSize = Big;
                    sa.Value = sa.NoShiftValue;
                    sa.ShiftSize = Small;
                    sa.AltSize = Small;
                });
            }
        }

        private void UpdateAlt()
        {
            if (_altFlag)
            {
                ShiftAndAlt.ForEach(sa =>
                {
                    sa.NoShiftSize = Small;
                    sa.AltSize = Big;
                    sa.Value = sa.AltValue;
                    sa.ShiftSize = Small;
                });
            }
            else
            {
                ShiftAndAlt.ForEach(sa =>
                {
                    sa.NoShiftSize = Big;
                    sa.Value = sa.NoShiftValue;
                    sa.ShiftSize = Small;
                    sa.AltSize = Small;
                });
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
