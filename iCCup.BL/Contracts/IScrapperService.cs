using iCCup.DATA.Models;

namespace iCCup.BL.Contracts
{
    public interface IScrapperService
    {
        UserSearch[] SearchPlayer(string url);

        UserGameProfile GetUserGameProfile(string url);

        UserGame[] GetUserGameList(string url);

        GameDetails GetGameDetails(string url);
    }
}
