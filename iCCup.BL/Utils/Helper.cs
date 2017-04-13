using System;

namespace iCCup.BL.Utils
{
    public static class Helper
    {
        public static int ToInteger(this string str)
        {
            return str == "" || str == " " ? 0 : Int32.Parse(str);
        }
    }
}
