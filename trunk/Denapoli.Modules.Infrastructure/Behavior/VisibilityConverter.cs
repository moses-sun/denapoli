using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Denapoli.Modules.Infrastructure.Behavior
{
    public class VisibilityConverter : IValueConverter
    {
      

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (bool) value;
            return val ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}