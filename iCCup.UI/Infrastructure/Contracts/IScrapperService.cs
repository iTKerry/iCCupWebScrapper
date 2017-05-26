using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iCCup.DATA.Models;

namespace iCCup.UI.Infrastructure.Contracts
{
    public interface IScrapperService
    {
        Task<(List<UserSearch>, string, string)> SearchPlayer(string nickname);
        Task<(List<UserSearch>, string, string)> SearchPlayer(Uri url);

        Task<UserGameProfile> GetUserGameProfile(UserSearch userSearch);

        Task<PersonalGameDetails> GetPersonalGameDetails(string nickname, string matchUrl);
    }
}
