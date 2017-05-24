using System;
using System.Globalization;
using System.Windows.Data;
using iCCup.DATA.Models;

namespace iCCup.UI.Converters
{
    public class MatchResultTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value as MatchResult? ?? MatchResult.Restart)
            {
                case MatchResult.Win:
                    return "Won Match";
                case MatchResult.Lose:
                    return "Lost Match";
                case MatchResult.Leave:
                    return "Abandoned";
                default:
                    return "No stats recorded";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
