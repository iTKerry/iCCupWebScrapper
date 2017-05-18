using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
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

            await Task.Factory.StartNew(GetAvatar);
        }

        private void GetAvatar()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                Avatar = new BitmapImage();
                Avatar.BeginInit();
                Avatar.UriSource = new Uri($"http:{UserGameProfile.ImageSource}", UriKind.Absolute);
                Avatar.EndInit();
            });
        }

        private BitmapImage _avatar;
        public BitmapImage Avatar
        {
            get => _avatar;
            set => Set(() => Avatar, ref _avatar, value);
        }

        private TabViewModel _tabViewModel;
        public TabViewModel TabViewModel
        {
            get => _tabViewModel;
            set => Set(() => TabViewModel, ref _tabViewModel, value);
        }

        private UserGameProfile _userGameProfile;
        public UserGameProfile UserGameProfile
        {
            get => _userGameProfile;
            set => Set(() => UserGameProfile, ref _userGameProfile, value);
        }
    }
}
