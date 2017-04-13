using System;

namespace iCCup.UI.Infrastructure.Contracts
{
    public interface ILoggerService
    {
        void AddInfo(string text);

        void AddInfoWithUrl(string text, string url);

        void AddError(Exception ex, string text);
    }
}
