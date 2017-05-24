using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;

namespace iCCup.UI.Infrastructure.Utils
{
    public static class Helper
    {
        public static int AsInt(this string str)
        {
            int result = int.TryParse(str, out result) ? result : 0;
            return result;
        }

        public static BitmapImage DownloadImage(this Uri uri)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = uri;
            image.EndInit();

            return image;
        }

        public static BitmapImage[] DownloadImages(this IEnumerable<Uri> uris) 
            => uris.Select(DownloadImage).ToArray();

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
            => new ObservableCollection<T>(enumerable);
    }
}
