using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Denapoli.Modules.Infrastructure.Controls
{
    /// <summary>
    /// Interaction logic for WinTest.xaml
    /// </summary>
    public partial class WinTest : Window, INotifyPropertyChanged
    {
        public WinTest()
        {
            WidthTouchKeyboard = 830;
            Width = WidthTouchKeyboard;
            SetCommandBinding();
            InitializeComponent();
            
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

        public static readonly RoutedUICommand CmdTlide = new RoutedUICommand();
        public static readonly RoutedUICommand Cmd1 = new RoutedUICommand();
        public static readonly RoutedUICommand Cmd2 = new RoutedUICommand();
        public static readonly RoutedUICommand Cmd3 = new RoutedUICommand();
        public static readonly RoutedUICommand Cmd4 = new RoutedUICommand();
        public static readonly RoutedUICommand Cmd5 = new RoutedUICommand();
        public static readonly RoutedUICommand Cmd6 = new RoutedUICommand();
        public static readonly RoutedUICommand Cmd7 = new RoutedUICommand();
        public static readonly RoutedUICommand Cmd8 = new RoutedUICommand();
        public static readonly RoutedUICommand Cmd9 = new RoutedUICommand();
        public static readonly RoutedUICommand Cmd0 = new RoutedUICommand();
        public static readonly RoutedUICommand CmdMinus = new RoutedUICommand();
        public static readonly RoutedUICommand CmdPlus = new RoutedUICommand();
        public static readonly RoutedUICommand CmdBackspace = new RoutedUICommand();


        public static readonly RoutedUICommand CmdTab = new RoutedUICommand();
        public static readonly RoutedUICommand CmdQ = new RoutedUICommand();
        public static readonly RoutedUICommand Cmdw = new RoutedUICommand();
        public static readonly RoutedUICommand CmdE = new RoutedUICommand();
        public static readonly RoutedUICommand CmdR = new RoutedUICommand();
        public static readonly RoutedUICommand CmdT = new RoutedUICommand();
        public static readonly RoutedUICommand CmdY = new RoutedUICommand();
        public static readonly RoutedUICommand CmdU = new RoutedUICommand();
        public static readonly RoutedUICommand CmdI = new RoutedUICommand();
        public static readonly RoutedUICommand CmdO = new RoutedUICommand();
        public static readonly RoutedUICommand CmdP = new RoutedUICommand();
        public static readonly RoutedUICommand CmdOpenCrulyBrace = new RoutedUICommand();
        public static readonly RoutedUICommand CmdEndCrultBrace = new RoutedUICommand();
        public static readonly RoutedUICommand CmdOr = new RoutedUICommand();

        public static readonly RoutedUICommand CmdCapsLock = new RoutedUICommand();
        public static readonly RoutedUICommand CmdA = new RoutedUICommand();
        public static readonly RoutedUICommand CmdS = new RoutedUICommand();
        public static readonly RoutedUICommand CmdD = new RoutedUICommand();
        public static readonly RoutedUICommand CmdF = new RoutedUICommand();
        public static readonly RoutedUICommand CmdG = new RoutedUICommand();
        public static readonly RoutedUICommand CmdH = new RoutedUICommand();
        public static readonly RoutedUICommand CmdJ = new RoutedUICommand();
        public static readonly RoutedUICommand CmdK = new RoutedUICommand();
        public static readonly RoutedUICommand CmdL = new RoutedUICommand();
        public static readonly RoutedUICommand CmdColon = new RoutedUICommand();
        public static readonly RoutedUICommand CmdDoubleInvertedComma = new RoutedUICommand();
        public static readonly RoutedUICommand CmdEnter = new RoutedUICommand();

        public static readonly RoutedUICommand CmdShift = new RoutedUICommand();
        public static readonly RoutedUICommand CmdZ = new RoutedUICommand();
        public static readonly RoutedUICommand CmdX = new RoutedUICommand();
        public static readonly RoutedUICommand CmdC = new RoutedUICommand();
        public static readonly RoutedUICommand CmdV = new RoutedUICommand();
        public static readonly RoutedUICommand CmdB = new RoutedUICommand();
        public static readonly RoutedUICommand CmdN = new RoutedUICommand();
        public static readonly RoutedUICommand CmdM = new RoutedUICommand();
        public static readonly RoutedUICommand CmdGreaterThan = new RoutedUICommand();
        public static readonly RoutedUICommand CmdLessThan = new RoutedUICommand();
        public static readonly RoutedUICommand CmdQuestion = new RoutedUICommand();

        public static readonly RoutedUICommand CmdSpaceBar = new RoutedUICommand();
        public static readonly RoutedUICommand CmdClear = new RoutedUICommand();

        public double WidthTouchKeyboard { get; set; }

        private static bool ShiftFlag { get; set; }

        private static bool CapsLockFlag { get; set; }

       
        #endregion

        #region CommandRelatedCode

        private void SetCommandBinding()
        {
            var cbTlide = new CommandBinding(CmdTlide, RunCommand);
            var cb1 = new CommandBinding(Cmd1, RunCommand);
            var cb2 = new CommandBinding(Cmd2, RunCommand);
            var cb3 = new CommandBinding(Cmd3, RunCommand);
            var cb4 = new CommandBinding(Cmd4, RunCommand);
            var cb5 = new CommandBinding(Cmd5, RunCommand);
            var cb6 = new CommandBinding(Cmd6, RunCommand);
            var cb7 = new CommandBinding(Cmd7, RunCommand);
            var cb8 = new CommandBinding(Cmd8, RunCommand);
            var cb9 = new CommandBinding(Cmd9, RunCommand);
            var cb0 = new CommandBinding(Cmd0, RunCommand);
            var cbMinus = new CommandBinding(CmdMinus, RunCommand);
            var cbPlus = new CommandBinding(CmdPlus, RunCommand);
            var cbBackspace = new CommandBinding(CmdBackspace, RunCommand);

            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbTlide);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cb1);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cb2);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cb3);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cb4);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cb5);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cb6);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cb7);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cb8);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cb9);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cb0);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbMinus);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbPlus);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbBackspace);


            var cbTab = new CommandBinding(CmdTab, RunCommand);
            var cbQ = new CommandBinding(CmdQ, RunCommand);
            var cbw = new CommandBinding(Cmdw, RunCommand);
            var cbE = new CommandBinding(CmdE, RunCommand);
            var cbR = new CommandBinding(CmdR, RunCommand);
            var cbT = new CommandBinding(CmdT, RunCommand);
            var cbY = new CommandBinding(CmdY, RunCommand);
            var cbU = new CommandBinding(CmdU, RunCommand);
            var cbI = new CommandBinding(CmdI, RunCommand);
            var cbo = new CommandBinding(CmdO, RunCommand);
            var cbP = new CommandBinding(CmdP, RunCommand);
            var cbOpenCrulyBrace = new CommandBinding(CmdOpenCrulyBrace, RunCommand);
            var cbEndCrultBrace = new CommandBinding(CmdEndCrultBrace, RunCommand);
            var cbOr = new CommandBinding(CmdOr, RunCommand);

            var cbCapsLock = new CommandBinding(CmdCapsLock, RunCommand);
            var cbA = new CommandBinding(CmdA, RunCommand);
            var cbS = new CommandBinding(CmdS, RunCommand);
            var cbD = new CommandBinding(CmdD, RunCommand);
            var cbF = new CommandBinding(CmdF, RunCommand);
            var cbG = new CommandBinding(CmdG, RunCommand);
            var cbH = new CommandBinding(CmdH, RunCommand);
            var cbJ = new CommandBinding(CmdJ, RunCommand);
            var cbK = new CommandBinding(CmdK, RunCommand);
            var cbL = new CommandBinding(CmdL, RunCommand);
            var cbColon = new CommandBinding(CmdColon, RunCommand);
            var cbDoubleInvertedComma = new CommandBinding(CmdDoubleInvertedComma, RunCommand);
            var cbEnter = new CommandBinding(CmdEnter, RunCommand);

            var cbShift = new CommandBinding(CmdShift, RunCommand);
            var cbZ = new CommandBinding(CmdZ, RunCommand);
            var cbX = new CommandBinding(CmdX, RunCommand);
            var cbC = new CommandBinding(CmdC, RunCommand);
            var cbV = new CommandBinding(CmdV, RunCommand);
            var cbB = new CommandBinding(CmdB, RunCommand);
            var cbN = new CommandBinding(CmdN, RunCommand);
            var cbM = new CommandBinding(CmdM, RunCommand);
            var cbGreaterThan = new CommandBinding(CmdGreaterThan, RunCommand);
            var cbLessThan = new CommandBinding(CmdLessThan, RunCommand);
            var cbQuestion = new CommandBinding(CmdQuestion, RunCommand);


            var cbSpaceBar = new CommandBinding(CmdSpaceBar, RunCommand);
            var cbClear = new CommandBinding(CmdClear, RunCommand);

            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbTab);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbQ);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbw);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbE);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbR);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbT);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbY);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbU);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbI);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbo);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbP);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbOpenCrulyBrace);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbEndCrultBrace);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbOr);

            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbCapsLock);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbA);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbS);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbD);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbF);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbG);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbH);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbJ);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbK);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbL);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbColon);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbDoubleInvertedComma);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbEnter);

            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbShift);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbZ);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbX);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbC);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbV);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbB);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbN);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbM);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbGreaterThan);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbLessThan);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbQuestion);


            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbSpaceBar);
            CommandManager.RegisterClassCommandBinding(typeof (WinTest), cbClear);
        }

        private void RunCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == CmdTlide) //First Row
            {
                if (!ShiftFlag)
                {
                    Text += "`";
                }
                else
                {
                    Text += "~";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == Cmd1)
            {
                if (!ShiftFlag)
                {
                    Text += "1";
                }
                else
                {
                    Text += "!";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == Cmd2)
            {
                if (!ShiftFlag)
                {
                    Text += "2";
                }
                else
                {
                    Text += "@";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == Cmd3)
            {
                if (!ShiftFlag)
                {
                    Text += "3";
                }
                else
                {
                    Text += "#";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == Cmd4)
            {
                if (!ShiftFlag)
                {
                    Text += "4";
                }
                else
                {
                    Text += "$";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == Cmd5)
            {
                if (!ShiftFlag)
                {
                    Text += "5";
                }
                else
                {
                    Text += "%";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == Cmd6)
            {
                if (!ShiftFlag)
                {
                    Text += "6";
                }
                else
                {
                    Text += "^";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == Cmd7)
            {
                if (!ShiftFlag)
                {
                    Text += "7";
                }
                else
                {
                    Text += "&";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == Cmd8)
            {
                if (!ShiftFlag)
                {
                    Text += "8";
                }
                else
                {
                    Text += "*";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == Cmd9)
            {
                if (!ShiftFlag)
                {
                    Text += "9";
                }
                else
                {
                    Text += "(";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == Cmd0)
            {
                if (!ShiftFlag)
                {
                    Text += "0";
                }
                else
                {
                    Text += ")";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == CmdMinus)
            {
                if (!ShiftFlag)
                {
                    Text += "-";
                }
                else
                {
                    Text += "_";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == CmdPlus)
            {
                if (!ShiftFlag)
                {
                    Text += "=";
                }
                else
                {
                    Text += "+";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == CmdBackspace)
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    Text = Text.Substring(0, Text.Length - 1);
                }
            }
            else if (e.Command == CmdTab) //Second Row
            {
                Text += "     ";
            }
            else if (e.Command == CmdQ)
            {
                AddKeyBoardINput('Q');
            }
            else if (e.Command == Cmdw)
            {
                AddKeyBoardINput('w');
            }
            else if (e.Command == CmdE)
            {
                AddKeyBoardINput('E');
            }
            else if (e.Command == CmdR)
            {
                AddKeyBoardINput('R');
            }
            else if (e.Command == CmdT)
            {
                AddKeyBoardINput('T');
            }
            else if (e.Command == CmdY)
            {
                AddKeyBoardINput('Y');
            }
            else if (e.Command == CmdU)
            {
                AddKeyBoardINput('U');
            }
            else if (e.Command == CmdI)
            {
                AddKeyBoardINput('I');
            }
            else if (e.Command == CmdO)
            {
                AddKeyBoardINput('O');
            }
            else if (e.Command == CmdP)
            {
                AddKeyBoardINput('P');
            }
            else if (e.Command == CmdOpenCrulyBrace)
            {
                if (!ShiftFlag)
                {
                    Text += "[";
                }
                else
                {
                    Text += "{";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == CmdEndCrultBrace)
            {
                if (!ShiftFlag)
                {
                    Text += "]";
                }
                else
                {
                    Text += "}";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == CmdOr)
            {
                if (!ShiftFlag)
                {
                    Text += @"\";
                }
                else
                {
                    Text += "|";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == CmdCapsLock)
            {
                CapsLockFlag = !CapsLockFlag;
            }
            else if (e.Command == CmdA)
            {
                AddKeyBoardINput('A');
            }
            else if (e.Command == CmdS)
            {
                AddKeyBoardINput('S');
            }
            else if (e.Command == CmdD)
            {
                AddKeyBoardINput('D');
            }
            else if (e.Command == CmdF)
            {
                AddKeyBoardINput('F');
            }
            else if (e.Command == CmdG)
            {
                AddKeyBoardINput('G');
            }
            else if (e.Command == CmdH)
            {
                AddKeyBoardINput('H');
            }
            else if (e.Command == CmdJ)
            {
                AddKeyBoardINput('J');
            }
            else if (e.Command == CmdK)
            {
                AddKeyBoardINput('K');
            }
            else if (e.Command == CmdL)
            {
                AddKeyBoardINput('L');
            }
            else if (e.Command == CmdColon)
            {
                if (!ShiftFlag)
                {
                    Text += ";";
                }
                else
                {
                    Text += ":";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == CmdDoubleInvertedComma)
            {
                if (!ShiftFlag)
                {
                    Text += "'";
                }
                else
                {
                    Text += Char.ConvertFromUtf32(34);
                    ShiftFlag = false;
                }
            }
            else if (e.Command == CmdEnter)
            {
               Close();
            }
            else if (e.Command == CmdShift) //Fourth Row
            {
                ShiftFlag = true;
            }
            else if (e.Command == CmdZ)
            {
                AddKeyBoardINput('Z');
            }
            else if (e.Command == CmdX)
            {
                AddKeyBoardINput('X');
            }
            else if (e.Command == CmdC)
            {
                AddKeyBoardINput('C');
            }
            else if (e.Command == CmdV)
            {
                AddKeyBoardINput('V');
            }
            else if (e.Command == CmdB)
            {
                AddKeyBoardINput('B');
            }
            else if (e.Command == CmdN)
            {
                AddKeyBoardINput('N');
            }
            else if (e.Command == CmdM)
            {
                AddKeyBoardINput('M');
            }
            else if (e.Command == CmdLessThan)
            {
                if (!ShiftFlag)
                {
                    Text += ",";
                }
                else
                {
                    Text += "<";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == CmdGreaterThan)
            {
                if (!ShiftFlag)
                {
                    Text += ".";
                }
                else
                {
                    Text += ">";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == CmdQuestion)
            {
                if (!ShiftFlag)
                {
                    Text += "/";
                }
                else
                {
                    Text += "?";
                    ShiftFlag = false;
                }
            }
            else if (e.Command == CmdSpaceBar) //Last row
            {
                Text += " ";
            }
            else if (e.Command == CmdClear) //Last row
            {
                Text = "";
            }
        }


        #endregion

        #region Main Functionality
        private void AddKeyBoardINput(char input)
        {
            if (CapsLockFlag)
            {
                if (ShiftFlag)
                {
                    Text += char.ToLower(input).ToString(CultureInfo.InvariantCulture);
                    ShiftFlag = false;
                }
                else
                {
                    Text += char.ToUpper(input).ToString(CultureInfo.InvariantCulture);
                }
            }
            else
            {
                if (!ShiftFlag)
                {
                    Text += char.ToLower(input).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    Text += char.ToUpper(input).ToString(CultureInfo.InvariantCulture);
                    ShiftFlag = false;
                }
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
