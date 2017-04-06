using System;
using System.Collections.ObjectModel;
using iCCup.DATA.Models;

namespace iCCup.UI.ViewModel
{
    public partial class MainViewModel
    {
        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; RaisePropertyChanged(() => PlayerName); }
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

        // Currently used for disable button.
        private Boolean _allowNextPage;
        public Boolean AllowNextPage
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

        // Currently used for disable button.
        private Boolean _allowPrevPage;
        public Boolean AllowPrevPage
        {
            get { return _allowPrevPage; }
            set { _allowPrevPage = value; RaisePropertyChanged(() => AllowPrevPage); }
        }
    }
}