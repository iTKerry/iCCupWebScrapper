using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;

namespace iCCup.UI.ViewModel.Tab
{
    public class GameProfileViewModel : ViewModelBase
    {
        private readonly IScrapperService _scrapper;

        public RelayCommand GoBackCommand =>
            new RelayCommand(() => TabViewModel.Navigate(new NavigateMessage { NavigateTo = NavigateTo.Back }));

        public GameProfileViewModel(TabViewModel tabViewModel, IScrapperService scrapper)
        {
            TabViewModel = tabViewModel;
            _scrapper = scrapper;
        }

        public async Task Show(UserSearch user)
        {
            UserGameProfile = null;
            UserGameProfile = await _scrapper.GetUserGameProfile(user);
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
