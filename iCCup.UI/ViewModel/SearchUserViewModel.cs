using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;
using iCCup.UI.Tabablz;

namespace iCCup.UI.ViewModel
{
    public class SearchUserViewModel : ViewModelBase
    {
        private CancellationTokenSource _ts;
        private CancellationToken _ct;

        private readonly ILoggerService _logger;
        private readonly IScrapperService _scrapper;

        public RelayCommand SearchPlayerCommand =>
            new RelayCommand(async () => await Task.Factory.StartNew(async () => await SearchPlayerTask(), _ct));

        public RelayCommand NextPageCommand =>
            new RelayCommand(async () => await SearchNavigate(true));

        public RelayCommand PrevPageCommand =>
            new RelayCommand(async () => await SearchNavigate(false));

        public RelayCommand GetPlayerProfileCommand =>
            new RelayCommand(() =>
            {
                var profile = _scrapper.GetUserGameProfile(SelectedUserSearch.Url);
            });

        public SearchUserViewModel(IScrapperService scrapper, HeaderViewModel hvm, ILoggerService logger)
        {
            _scrapper = scrapper;
            _logger = logger;
            _logger.AddInfo("New search tab initialized.");

            Hvm = hvm;
            Header = PlayerName ?? "Search";
            Players = new ObservableCollection<UserSearch>();
#if DEBUG
            Players.Add(new UserSearch
            {
                Nickname = "iTKerry",
                Pts5V5 = 3755,
                Rank5V5 = "C-",
                Win5V5 = 33,
                Lose5V5 = 26,
                Pts3V3 = 1000,
                Rank3V3 = "D",
                Win3V3 = 0,
                Lose3V3 = 0
            });
#endif
        }

        private async Task SearchPlayerTask()
        {
            var searchResults = await _scrapper.SearchPlayer(PlayerName ?? "");
            await HandleSearchResult(searchResults);
        }

        private async Task SearchNavigate(bool ahead)
        {
            var searchResults = await _scrapper.SearchPlayer(ahead
                ? new Uri(NextPage)
                : new Uri(PrevPage));
            await HandleSearchResult(searchResults);
        }

        private async Task HandleSearchResult(Tuple<List<UserSearch>, string, string> searchResults)
        {
            _ts?.Cancel();
            _ts = new CancellationTokenSource();
            _ct = _ts.Token;

            try
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    Players.Clear();
                    NextPage = searchResults.Item2;
                    PrevPage = searchResults.Item3;
                });

                foreach (var player in searchResults.Item1)
                {
                    await Task.Delay(15, _ct);
                    DispatcherHelper.CheckBeginInvokeOnUI(() => Players.Add(player));
                }
            }
            catch (TaskCanceledException)
            {
                if (_ct.IsCancellationRequested)
                {
                    Players.Clear();
                }
            }
        }

        #region Props

        private HeaderViewModel _hvm;
        public HeaderViewModel Hvm
        {
            get { return _hvm; }
            set { _hvm = value; RaisePropertyChanged(() => Hvm);}
        }

        public string Header
        {
            get { return Hvm.Header; }
            set { Hvm.Header = value; RaisePropertyChanged(() => Hvm); }
        }

        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                if (!string.Equals(_playerName, string.Empty))
                {
                    if(_playerName.Length >= 3)
                        //SearchPlayerCommand.Execute(true);
                    RaisePropertyChanged(() => Header);
                }
                RaisePropertyChanged(() => PlayerName);
            }
        }

        private UserSearch _selectedUserSearch = new UserSearch { Nickname = "Search" };
        public UserSearch SelectedUserSearch
        {
            get { return _selectedUserSearch; }
            set { _selectedUserSearch = value; RaisePropertyChanged(() => SelectedUserSearch); }
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

        #endregion
    }
}
