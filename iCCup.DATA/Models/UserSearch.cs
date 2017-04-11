namespace iCCup.DATA.Models
{
    public class UserSearch
    {
        public string Url { get; set; }

        public string Nickname { get; set; }

        public int Win5V5 { get; set; }
        public int Lose5V5 { get; set; }
        public int Pts5V5 { get; set; }
        public string Rank5V5 { get; set; }

        public int Win3V3 { get; set; }
        public int Lose3V3 { get; set; }
        public int Pts3V3 { get; set; }
        public string Rank3V3 { get; set; }
    }
}
