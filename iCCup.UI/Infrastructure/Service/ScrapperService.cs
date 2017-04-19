using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;
using iCCup.UI.Infrastructure.Scrapper;
using iCCup.UI.Infrastructure.Utils;

namespace iCCup.UI.Infrastructure.Service
{
    public class ScrapperService : IScrapperService
    {
        private readonly ScrapperBase _scrapper;

        public ScrapperService(ScrapperBase scrapper)
        {
            _scrapper = scrapper;
        }

        public Task<Tuple<List<UserSearch>, string, string>> SearchPlayer(string url) =>_scrapper.SearchPlayer(url);

        public Task<Tuple<List<UserSearch>, string, string>> SearchPlayer(Uri url) => _scrapper.SearchPlayer(url);

        public Task<UserGameProfile> GetUserGameProfile(UserSearch userSearch) => 
            _scrapper.GetPlayerProfile(new Uri($"{Globals.iCCupUrl}{userSearch.Url}"), userSearch);

        public List<UserGame> GetUserGameList(string url) => throw new NotImplementedException();

        public GameDetails GetGameDetails(string url) => throw new NotImplementedException();
    }
}
