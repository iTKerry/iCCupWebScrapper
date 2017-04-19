using System;
using System.ComponentModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;
using iCCup.UI.Infrastructure.Utils;

namespace iCCup.UI.ViewModel.Tab
{
    public class GameProfileViewModel : INotifyPropertyChanged
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
            get => _tabViewModel;
            set => this.MutateVerbose(ref _tabViewModel, value, RaisePropertyChanged());
        }

        private UserGameProfile _userGameProfile;
        public UserGameProfile UserGameProfile
        {
            get => _userGameProfile;
            set => this.MutateVerbose(ref _userGameProfile, value, RaisePropertyChanged());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private Action<PropertyChangedEventArgs> RaisePropertyChanged() => args => PropertyChanged?.Invoke(this, args);
    }
}
