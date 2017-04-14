using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;

namespace iCCup.UI.ViewModel.Tab
{
    public class GameProfileViewModel : ViewModelBase
    {
        private readonly IMessangerService _messanger;

        public RelayCommand GoBackCommand =>
            new RelayCommand(() => _messanger.NavigateMessage(new NavigateMessage { NavigateTo = NavigateTo.Back }));

        public GameProfileViewModel(IMessangerService messanger)
        {
            _messanger = messanger;
        }

        public async Task Show(UserSearch contentContent)
        {

        }
    }
}
