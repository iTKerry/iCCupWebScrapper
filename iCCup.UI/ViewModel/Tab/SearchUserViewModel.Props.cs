using System.Collections.ObjectModel;
using iCCup.DATA.Models;

namespace iCCup.UI.ViewModel.Tab
{
    public partial class SearchUserViewModel
    {
        public TabViewModel TabViewModel
        {
            get { return _tabViewModel; }
            set { _tabViewModel = value; RaisePropertyChanged(() => TabViewModel);}
        }

        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; RaisePropertyChanged(() => PlayerName); }
        }

        private UserSearch _selectedUserSearch;
        public UserSearch SelectedUserSearch
        {
            get { return _selectedUserSearch; }
            set
            {
                _selectedUserSearch = value;
                RaisePropertyChanged(() => SelectedUserSearch);
            }
        }

        private ObservableCollection<UserSearch> _players;
        public ObservableCollection<UserSearch> Players
        {
            get { return _players; }
            set { _players = value; RaisePropertyChanged(() => Players); }
        }

        private string _nextPage;
        public string NextPage
        {
            get { return _nextPage; }
            set
            {
                AllowNextPage = !Equals(value, string.Empty);

                _nextPage = value;
                RaisePropertyChanged(() => NextPage);
            }
        }

        private bool _allowNextPage;
        public bool AllowNextPage
        {
            get { return _allowNextPage; }
            set { _allowNextPage = value; RaisePropertyChanged(() => AllowNextPage); }
        }

        private string _prevPage;
        public string PrevPage
        {
            get { return _prevPage; }
            set
            {
                AllowPrevPage = !Equals(value, string.Empty);

                _prevPage = value;
                RaisePropertyChanged(() => PrevPage);
            }
        }

        private bool _allowPrevPage;
        public bool AllowPrevPage
        {
            get { return _allowPrevPage; }
            set { _allowPrevPage = value; RaisePropertyChanged(() => AllowPrevPage); }
        }

        private bool _isBusy;
        private TabViewModel _tabViewModel;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy); }
        }

    }
}