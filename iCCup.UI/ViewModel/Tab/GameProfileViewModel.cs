using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;
using iCCup.UI.Infrastructure.Utils;

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

            MatchList = new ObservableCollection<PersonalGameDetails>();
            MatchListHome = new ObservableCollection<PersonalGameDetails>();
            
            await Task.Factory.StartNew(GetAvatar);
            await Task.Factory.StartNew(GetMatches);
        }

        private void GetMatches() =>
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                foreach (var url in UserGameProfile.MatchUrls)
                {
                    MatchList.Add(await _scrapper.GetPersonalGameDetails(UserGameProfile.Nickname, url));
                    if (MatchListHome.Count < 5)
                        MatchListHome.Add(MatchList.Last());
                }
            });

        private void GetAvatar() =>
            DispatcherHelper.CheckBeginInvokeOnUI(() 
                => Avatar = new Uri($"http:{UserGameProfile.ImageSource}", UriKind.Absolute).DownloadImage());

        private ObservableCollection<PersonalGameDetails> _matchList;
        public ObservableCollection<PersonalGameDetails> MatchList
        {
            get => _matchList;
            set => Set(() => MatchList, ref _matchList, value);
        }

        private ObservableCollection<PersonalGameDetails> _matchListHome;
        public ObservableCollection<PersonalGameDetails> MatchListHome
        {
            get => _matchListHome;
            set => Set(() => MatchListHome, ref _matchListHome, value);
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
