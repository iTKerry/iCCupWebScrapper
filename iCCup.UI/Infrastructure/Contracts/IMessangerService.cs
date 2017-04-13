using iCCup.DATA.Models;

namespace iCCup.UI.Infrastructure.Contracts
{
    public interface IMessangerService
    {
        void AddToLog(LogMessange content);
    }
}
