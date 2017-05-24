using System;
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
    }
}
