using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace iCCup.UI.Converters
{
    public class MatchNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string ?? String.Empty;
            var mode = Regex.Match(str, @"\ (.*?)\ ").Value.Trim();
            return str.Substring(str.LastIndexOf(mode, StringComparison.Ordinal) + 5);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
