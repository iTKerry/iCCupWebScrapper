using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iCCup.DATA.Models;

namespace iCCup.BL.Contracts
{
    public interface IScrapperService
    {
        Task<Tuple<List<UserSearch>, string, string>> SearchPlayer(string nickname);
        Task<Tuple<List<UserSearch>, string, string>> SearchPlayer(Uri url);

        UserGameProfile GetUserGameProfile(string url);

        List<UserGame> GetUserGameList(string url);

        GameDetails GetGameDetails(string url);
    }
}
