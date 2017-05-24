using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace iCCup.UI.Converters
{
    public class MatchNameToModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string ?? String.Empty;
            return Regex.Match(str, @"\ (.*?)\ ").Value.Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
