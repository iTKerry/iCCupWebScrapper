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
            new RelayCommand(() => TabViewModel.Navigate(new NavigateMessage { NavigateTo = NavigateTo.Back }));

        public GameProfileViewModel(TabViewModel tabViewModel, IMessangerService messanger)
        {
            TabViewModel = tabViewModel;
            _messanger = messanger;
        }

        public async Task Show(UserSearch contentContent)
        {
            Nickname = contentContent.Nickname;
        }

        private TabViewModel _tabViewModel;
        public TabViewModel TabViewModel
        {
            get { return _tabViewModel; }
            set { _tabViewModel = value; RaisePropertyChanged(() => TabViewModel); }
        }

        private string _nickname;
        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; RaisePropertyChanged(() => Nickname);}
        }
    }
}
