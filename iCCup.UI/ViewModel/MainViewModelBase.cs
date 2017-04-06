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
                ShowNextPage = !Equals(value, string.Empty);

                _nextPage = value;
                RaisePropertyChanged(() => NextPage);
            }
        }

        private Boolean _showNextPage;
        public Boolean ShowNextPage
        {
            get { return _showNextPage; }
            set { _showNextPage = value; RaisePropertyChanged(() => ShowNextPage); }
        }

        private string _prevPage;
        public string PrevPage
        {
            get { return _prevPage; }
            set
            {
                ShowPrevPage = !Equals(value, string.Empty);

                _prevPage = value;
                RaisePropertyChanged(() => PrevPage);
            }
        }

        private Boolean _showPrevPage;
        public Boolean ShowPrevPage
        {
            get { return _showPrevPage; }
            set { _showPrevPage = value; RaisePropertyChanged(() => ShowPrevPage); }
        }
    }
}