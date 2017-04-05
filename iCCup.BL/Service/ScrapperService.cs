using iCCup.BL.Contracts;
using iCCup.BL.Scrapper;
using iCCup.DATA.Models;

namespace iCCup.BL.Service
{
    public class ScrapperService : IScrapperService
    {
        private readonly ScrapperBase _scrapper;

        public ScrapperService()
        {
            _scrapper = new ScrapperBase();
        }

        public UserSearch[] SearchPlayer(string url)
        {
            throw new System.NotImplementedException();
        }

        public UserGameProfile GetUserGameProfile(string url)
        {
            throw new System.NotImplementedException();
        }

        public UserGame[] GetUserGameList(string url)
        {
            throw new System.NotImplementedException();
        }

        public GameDetails GetGameDetails(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
