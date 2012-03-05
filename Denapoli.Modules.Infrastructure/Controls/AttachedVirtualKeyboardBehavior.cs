using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Denapoli.Modules.Infrastructure.Controls
{
    public static class AttachedVirtualKeyboardBehavior
    {

        private static readonly WinTest TactileKeyboard = Intialize();
        public static bool IsClosing { get; set; }
        private static WinTest Intialize()
        {
            var keyboard = new WinTest
                               {
                                     AllowsTransparency = true, 
                                     WindowStyle = WindowStyle.None, 
                                     ShowInTaskbar = false,
                                     Topmost = true
                               };
            keyboard.PropertyChanged += (a,b) =>{TouchScreenText = keyboard.Text;};
            keyboard.Deactivated += OnKeyboardclosed;

            return keyboard;
        }

        private static Brush _previousTextBoxBackgroundBrush;
        private static Brush _previousTextBoxBorderBrush;
        private static Thickness _previousTextBoxBorderThickness;

        private static Control _currentControl;

        private static string TouchScreenText
        {
            get
            {
                var currentControl = _currentControl as TextBox;
                if (currentControl != null)
                {
                    return (currentControl).Text;
                }
                var passwordBox = _currentControl as PasswordBox;
                return passwordBox != null ? (passwordBox).Password : "";
            }
            set
            {
                if (_currentControl is TextBox)
                {
                    ((TextBox)_currentControl).Text = value;
                }
                else if (_currentControl is PasswordBox)
                {
                    ((PasswordBox)_currentControl).Password = value;
                }
            }
        }





        public static readonly DependencyProperty TactileKeyboardProperty =
           DependencyProperty.RegisterAttached("TactileKeyboard", typeof(bool), typeof(AttachedVirtualKeyboardBehavior),
                                               new UIPropertyMetadata(default(bool), TactileKeyboardPropertyChanged));

        public static bool GetTactileKeyboard(DependencyObject obj)
        {
            return (bool)obj.GetValue(TactileKeyboardProperty);
        }

        public static void SetTactileKeyboard(DependencyObject obj, bool value)
        {
            obj.SetValue(TactileKeyboardProperty, value);
        }


        private static void TactileKeyboardPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var host = sender as FrameworkElement;
            if (host == null) return;
            host.GotFocus += OnGotFocus;
            host.LostFocus += OnLostFocus;
        }

        private static void OnGotFocus(object sender, RoutedEventArgs e)
        {
            var host = (Control)sender;

            _previousTextBoxBackgroundBrush = host.Background;
            _previousTextBoxBorderBrush = host.BorderBrush;
            _previousTextBoxBorderThickness = host.BorderThickness;

            host.Background = Brushes.Yellow;
            host.BorderBrush = Brushes.Red;
            host.BorderThickness = new Thickness(4);

            //if (_currentControl != null)
            //    _currentControl.LayoutUpdated -= TbLayoutUpdated;

            _currentControl = host;

            var ct = Window.GetWindow(_currentControl);
            if (ct != null)
            {
                //ct.LocationChanged += TactileKeyboardLocationChanged;
                ct.Activated += TactileKeyboardActivated;
                ct.Deactivated += TactileKeyboardDeactivated;
            }
            TactileKeyboard.Text = TouchScreenText;
            Syncchild();
            //host.LayoutUpdated += TbLayoutUpdated;
        }

        private static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            var host = (Control)sender;
            host.Background = _previousTextBoxBackgroundBrush;
            host.BorderBrush = _previousTextBoxBorderBrush;
            host.BorderThickness = _previousTextBoxBorderThickness;
            TactileKeyboard.Hide();
        }

        private static void Syncchild()
        {
            if (_currentControl == null ) return;
            var virtualpoint = new Point(0, _currentControl.ActualHeight + 3);
            var actualpoint = _currentControl.PointToScreen(virtualpoint);

            if (TactileKeyboard.WidthTouchKeyboard + actualpoint.X > SystemParameters.VirtualScreenWidth)
            {
                var difference = TactileKeyboard.WidthTouchKeyboard + actualpoint.X - SystemParameters.VirtualScreenWidth;
                TactileKeyboard.Left = actualpoint.X - difference;
            }
            else if (!(actualpoint.X > 1))
            {
                TactileKeyboard.Left = 1;
            }
            else
                TactileKeyboard.Left = actualpoint.X;


            TactileKeyboard.Top = actualpoint.Y;
            TactileKeyboard.Show();
        }

        private static void TactileKeyboardDeactivated(object sender, EventArgs e)
        {
            TactileKeyboard.Topmost = false;
        }

        private static void TactileKeyboardActivated(object sender, EventArgs e)
        {
                TactileKeyboard.Topmost = true;
        }

        private static void TactileKeyboardLocationChanged(object sender, EventArgs e)
        {
            Syncchild();
        }

        private static void TbLayoutUpdated(object sender, EventArgs e)
        {
            Syncchild();
        }

        private static void OnKeyboardclosed(object sender, EventArgs eventArgs)
        {
            _currentControl.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}