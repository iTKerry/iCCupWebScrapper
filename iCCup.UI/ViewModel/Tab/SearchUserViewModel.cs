using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;
using iCCup.UI.Navigation;

namespace iCCup.UI.ViewModel.Tab
{
    public partial class SearchUserViewModel : INotifyPropertyChanged, ITidyable
    {
        private CancellationTokenSource _ts;
        private CancellationToken _ct;

        private readonly IScrapperService _scrapper;
        private readonly ILoggerService _logger;

        public RelayCommand SearchPlayerCommand =>
            new RelayCommand(async () => await Task.Factory.StartNew(async () => await Search(), _ct));

        public RelayCommand NextPageCommand =>
            new RelayCommand(async () => await Search(isSearch: false));

        public RelayCommand PrevPageCommand =>
            new RelayCommand(async () => await Search(isSearch: false, navigateForward: false));

        public RelayCommand<UserSearch> GoToProfileCommand =>
            new RelayCommand<UserSearch>(userSearch => TabViewModel.Navigate(new NavigateMessage
            {
                NavigateTo = NavigateTo.Profile,
                Content = userSearch
            }));

        public SearchUserViewModel(TabViewModel tabViewModel,IScrapperService scrapper, ILoggerService logger)
        {
            TabViewModel = tabViewModel;
            _scrapper = scrapper;
            _logger = logger;

            Players = new ObservableCollection<UserSearch>();
#if DEBUG
            DebugInit();
#endif
        }

        private async Task Search(bool isSearch = true, bool navigateForward = true)
        {
            await HandleSearchResult(isSearch
                ? await _scrapper.SearchPlayer(PlayerName ?? "")
                : await _scrapper.SearchPlayer(new Uri(navigateForward ? NextPage : PrevPage)));
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

        private void DebugInit()
        {
            var testInit = new List<UserSearch>
            {
                new UserSearch
                {
                    Nickname = "Gyber",
                    Pts5V5 = 10062,
                    Win5V5 = 86,
                    Lose5V5 = 69,
                    Pts3V3 = 1000,
                    Win3V3 = 0,
                    Lose3V3 = 0,
                    Rank5V5 = "A-",
                    Rank3V3 = "D",
                    Url = "gamingprofile/Gyber..html"
                },
                new UserSearch
                {
                    Nickname = "iTKerry",
                    Pts5V5 = 3788,
                    Win5V5 = 44,
                    Lose5V5 = 30,
                    Pts3V3 = 1000,
                    Win3V3 = 0,
                    Lose3V3 = 0,
                    Rank5V5 = "C-",
                    Rank3V3 = "D",
                    Url = "gamingprofile/iTKerry.html"
                }
            };
            testInit.ForEach(t => Players.Add(t));
        }

        public void Tidy()
        {
            //Players = new ObservableCollection<UserSearch>();
            _logger.AddInfo($"{GetType()} used Tidy.");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private Action<PropertyChangedEventArgs> RaisePropertyChanged() => args => PropertyChanged?.Invoke(this, args);
    }
}
