using System.Collections.Generic;

namespace iCCup.BL.Utils
{
    public static class Globals
    {
        public static string iCCupUrl = "http://iccup.com/";

        public static string Search = "search.html?search={0}&search-sel=0";

        public static Dictionary<string, string> Ranks = new Dictionary<string, string>
        {
            {"d1", "D-"},
            {"d2", "D" },
            {"d3", "D+"},
            {"c1", "C-"},
            {"c2", "C" },
            {"c3", "C+"},
            {"b1", "B-"},
            {"b2", "B" },
            {"b3", "B+"},
            {"a1", "A-"},
            {"a2", "A" },
            {"a3", "A+"},
        };

        public static int ToInteger(this string str)
        {
            return str == "" || str == " " ? 0 : int.Parse(str);
        }
    }
}
