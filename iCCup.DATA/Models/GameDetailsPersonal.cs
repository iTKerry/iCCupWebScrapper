using System.Collections.Generic;

namespace iCCup.DATA.Models
{
    public class GameDetailsPersonal
    {
        public string GameName { get; set; } //+
        public string DateTime { get; set; } //+
        public string GameTime { get; set; } //+

        public int Kills { get; set; } //+
        public int Deaths { get; set; } //+
        public int Assists { get; set; } //+

        public int CreepStats { get; set; } //+
        public int TowersDestroyed { get; set; } //+
        public int GoldLeft { get; set; } //+

        public MatchResult MatchResult { get; set; }
        public GameSide GameSide { get; set; } //+
        public int Pts { get; set; }
        public int? BonusPts { get; set; }

        public string Hero { get; set; }
        public List<string> Items { get; set; }
    }
}
