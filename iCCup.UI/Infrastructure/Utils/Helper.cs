namespace iCCup.UI.Infrastructure.Utils
{
    public static class Helper
    {
        public static int ToInteger(this string str)
        {
            int result = int.TryParse(str, out result) ? result : 0;
            return result;
        }
    }
}
