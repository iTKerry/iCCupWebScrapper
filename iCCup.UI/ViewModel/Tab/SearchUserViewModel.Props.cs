using System.Collections.ObjectModel;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Utils;

namespace iCCup.UI.ViewModel.Tab
{
    public partial class SearchUserViewModel
    {
        private TabViewModel _tabViewModel;
        public TabViewModel TabViewModel
        {
            get => _tabViewModel;
            set => this.MutateVerbose(ref _tabViewModel, value, RaisePropertyChanged());
        }

        private string _playerName;
        public string PlayerName
        {
            get => _playerName;
            set => this.MutateVerbose(ref _playerName, value, RaisePropertyChanged());
        }

        private UserSearch _selectedUserSearch;
        public UserSearch SelectedUserSearch
        {
            get => _selectedUserSearch;
            set => this.MutateVerbose(ref _selectedUserSearch, value, RaisePropertyChanged());
        }

        private ObservableCollection<UserSearch> _players;
        public ObservableCollection<UserSearch> Players
        {
            get => _players;
            set => this.MutateVerbose(ref _players, value, RaisePropertyChanged());
        }

        private string _nextPage;
        public string NextPage
        {
            get => _nextPage;
            set
            {
                AllowNextPage = !Equals(value, string.Empty);
                this.MutateVerbose(ref _nextPage, value, RaisePropertyChanged());
            }
        }

        private bool _allowNextPage;
        public bool AllowNextPage
        {
            get => _allowNextPage;
            set => this.MutateVerbose(ref _allowNextPage, value, RaisePropertyChanged());
        }

        private string _prevPage;
        public string PrevPage
        {
            get => _prevPage;
            set
            {
                AllowPrevPage = !Equals(value, string.Empty);
                this.MutateVerbose(ref _prevPage, value, RaisePropertyChanged());
            }
        }

        private bool _allowPrevPage;
        public bool AllowPrevPage
        {
            get => _allowPrevPage;
            set => this.MutateVerbose(ref _allowPrevPage, value, RaisePropertyChanged());
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => this.MutateVerbose(ref _isBusy, value, RaisePropertyChanged());
        }
    }
}