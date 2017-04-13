using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iCCup.DATA.Models;

namespace iCCup.UI.Infrastructure.Contracts
{
    public interface IScrapperService
    {
        Task<Tuple<List<UserSearch>, string, string>> SearchPlayer(string nickname);
        Task<Tuple<List<UserSearch>, string, string>> SearchPlayer(Uri url);

        Task<UserGameProfile> GetUserGameProfile(UserSearch userSearch);

        List<UserGame> GetUserGameList(string url);

        GameDetails GetGameDetails(string url);
    }
}
