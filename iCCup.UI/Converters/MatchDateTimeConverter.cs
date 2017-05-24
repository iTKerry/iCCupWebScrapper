using System;
using System.Globalization;
using System.Windows.Data;

namespace iCCup.UI.Converters
{
    public class MatchDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            return !str.Contains("today")
                ? str.Substring(0, str.LastIndexOf('@') + 1).Replace(" @", String.Empty)
                : str.Substring(str.LastIndexOf('@')).Replace("@ ", String.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
