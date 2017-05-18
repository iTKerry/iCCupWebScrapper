using System.Collections.ObjectModel;
using iCCup.DATA.Models;

namespace iCCup.UI.ViewModel.Tab
{
    public partial class SearchUserViewModel
    {
        private TabViewModel _tabViewModel;
        public TabViewModel TabViewModel
        {
            get => _tabViewModel;
            set => Set(() => TabViewModel, ref _tabViewModel, value);
        }

        private string _playerName;
        public string PlayerName
        {
            get => _playerName;
            set => Set(() => PlayerName, ref _playerName, value);
        }

        private UserSearch _selectedUserSearch;
        public UserSearch SelectedUserSearch
        {
            get => _selectedUserSearch;
            set => Set(() => SelectedUserSearch, ref _selectedUserSearch, value);
        }

        private ObservableCollection<UserSearch> _players;
        public ObservableCollection<UserSearch> Players
        {
            get => _players;
            set => Set(() => Players, ref _players, value);
        }

        private string _nextPage;
        public string NextPage
        {
            get => _nextPage;
            set
            {
                AllowNextPage = !Equals(value, string.Empty);
                Set(() => NextPage, ref _nextPage, value);
            }
        }

        private bool _allowNextPage;
        public bool AllowNextPage
        {
            get => _allowNextPage;
            set => Set(() => AllowNextPage, ref _allowNextPage, value);
        }

        private string _prevPage;
        public string PrevPage
        {
            get => _prevPage;
            set
            {
                AllowPrevPage = !Equals(value, string.Empty);
                Set(() => PrevPage, ref _prevPage, value);
            }
        }

        private bool _allowPrevPage;
        public bool AllowPrevPage
        {
            get => _allowPrevPage;
            set => Set(() => AllowPrevPage, ref _allowPrevPage, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(() => IsBusy, ref _isBusy, value);
        }
    }
}