using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace iCCup.UI.Infrastructure.Utils
{
    public static class Helper
    {
        public static int ToInteger(this string str)
        {
            int result = int.TryParse(str, out result) ? result : 0;
            return result;
        }

        public static void MutateVerbose<TField>(this INotifyPropertyChanged instance
            , ref TField field
            , TField newValue
            , Action<PropertyChangedEventArgs> raise
            , [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TField>.Default.Equals(field, newValue)) return;
            field = newValue;
            raise?.Invoke(new PropertyChangedEventArgs(propertyName));
        }
    }
}
