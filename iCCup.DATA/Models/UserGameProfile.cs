namespace iCCup.DATA.Models
{
    public class UserGameProfile : UserSearch
    {
        public string GamesListUrl { get; set; }
        public int RaitingPosition5V5 { get; set; }
        public int Couriers { get; set; }
        public int Neutrals { get; set; }
        public int Hours { get; set; }
        public int Winrate5V5 { get; set; }
        public int Leaves5V5 { get; set; }
        public int MaxWinstreak5V5 { get; set; }
        public int CurrentWinstreak5V5 { get; set; }

        public UserGameProfile(UserSearch search)
        {
            this.Url = search.Url;
            this.Nickname = search.Nickname;

            this.Win5V5 = search.Win5V5;
            this.Lose5V5 = search.Lose5V5;
            this.Pts5V5 = search.Pts5V5;
            this.Rank5V5 = search.Rank5V5;

            this.Win3V3 = search.Win3V3;
            this.Lose3V3 = search.Lose3V3;
            this.Pts3V3 = search.Pts3V3;
            this.Rank3V3 = search.Rank3V3;
        }
    }
}
