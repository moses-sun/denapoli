using System;
using System.Globalization;
using System.Windows.Data;
using Denapoli.Modules.Infrastructure.Services;

namespace Denapoli.Modules.Infrastructure.Behavior
{
    public class LocalizationConverter : IValueConverter
    {
        public static ILocalizationService LocalizationService { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return LocalizationService.Localize(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}