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
using iCCup.UI.Navigation;

namespace iCCup.UI.ViewModel.Tab
{
    public partial class SearchUserViewModel : ViewModelBase, ITidyable
    {
        private CancellationTokenSource _ts;
        private CancellationToken _ct;

        private readonly IScrapperService _scrapper;
        private readonly ILoggerService _logger;
        private readonly IMessangerService _messanger;

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

        public SearchUserViewModel(TabViewModel tabViewModel,IScrapperService scrapper, ILoggerService logger, IMessangerService messanger)
        {
            TabViewModel = tabViewModel;
            _scrapper = scrapper;
            _logger = logger;
            _messanger = messanger;

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
                    Nickname = "Test1",
                    Pts5V5 = 1000,
                    Win5V5 = 0,
                    Lose5V5 = 0,
                    Pts3V3 = 1000,
                    Win3V3 = 0,
                    Lose3V3 = 0,
                    Rank5V5 = "D",
                    Rank3V3 = "D"
                },
                new UserSearch
                {
                    Nickname = "Test2",
                    Pts5V5 = 1000,
                    Win5V5 = 0,
                    Lose5V5 = 0,
                    Pts3V3 = 1000,
                    Win3V3 = 0,
                    Lose3V3 = 0,
                    Rank5V5 = "D",
                    Rank3V3 = "D"
                },
                new UserSearch
                {
                    Nickname = "Test3",
                    Pts5V5 = 1000,
                    Win5V5 = 0,
                    Lose5V5 = 0,
                    Pts3V3 = 1000,
                    Win3V3 = 0,
                    Lose3V3 = 0,
                    Rank5V5 = "D",
                    Rank3V3 = "D"
                },
            };
            testInit.ForEach(t => Players.Add(t));
        }

        public void Tidy()
        {
            //Players = new ObservableCollection<UserSearch>();
            _logger.AddInfo($"{this.GetType()} used Tidy.");
        }
    }
}
