using GalaSoft.MvvmLight.Messaging;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;

namespace iCCup.UI.Infrastructure.Service
{
    public class MessangerService : IMessangerService
    {
        public void AddToLog(LogMessange content)
        {
            Messenger.Default.Send(new NotificationMessage<LogMessange>(this, content, "Sending AddToLog messange"));
        }

        public void NavigateMessage(NavigateMessage content)
        {
            Messenger.Default.Send(new NotificationMessage<NavigateMessage>(this, content, "Navigating"));
        }
    }
}
