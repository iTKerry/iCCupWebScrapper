using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iCCup.BL.Contracts;
using iCCup.BL.Scrapper;
using iCCup.BL.Utils;
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

        public Task<Tuple<List<UserSearch>, string, string>> SearchPlayer(string url)
        {
            return _scrapper.SearchPlayer(url);
        }

        public Task<Tuple<List<UserSearch>, string, string>> SearchPlayer(Uri url)
        {
            return _scrapper.SearchPlayer(url);
        }

        public Task<UserGameProfile> GetUserGameProfile(string url)
        {
            return _scrapper.GetPlayerProfile(new Uri($"{Globals.iCCupUrl}{url}"));
        }

        public List<UserGame> GetUserGameList(string url)
        {
            throw new NotImplementedException();
        }

        public GameDetails GetGameDetails(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
