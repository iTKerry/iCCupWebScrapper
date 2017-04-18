using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iCCup.DATA.Models;

namespace iCCup.UI.ViewModel.Tab
{
    public class GameProfileViewModel : ViewModelBase
    {
        public RelayCommand GoBackCommand =>
            new RelayCommand(() => TabViewModel.Navigate(new NavigateMessage { NavigateTo = NavigateTo.Back }));

        public GameProfileViewModel(TabViewModel tabViewModel)
        {
            TabViewModel = tabViewModel;
        }

        public async Task Show(UserSearch contentContent)
        {
            UserGameProfile = new UserGameProfile(contentContent);
        }

        private TabViewModel _tabViewModel;
        public TabViewModel TabViewModel
        {
            get { return _tabViewModel; }
            set { _tabViewModel = value; RaisePropertyChanged(() => TabViewModel); }
        }

        private UserGameProfile _userGameProfile;
        public UserGameProfile UserGameProfile
        {
            get { return _userGameProfile; }
            set { _userGameProfile = value; RaisePropertyChanged(() => UserGameProfile);}
        }
    }
}
