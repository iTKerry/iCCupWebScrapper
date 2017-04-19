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
        }

        private async Task Search(bool isSearch = true, bool navigateForward = true)
        {
            await HandleSearchResult(isSearch
                ? await _scrapper.SearchPlayer(PlayerName ?? "")
                : await _scrapper.SearchPlayer(new Uri(navigateForward ? NextPage : PrevPage)));
        }

        private async Task HandleSearchResult((List<UserSearch> list, string nextPageUrl, string prevPageUrl) result)
        {
            _ts?.Cancel();
            _ts = new CancellationTokenSource();
            _ct = _ts.Token;

            try
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    Players.Clear();
                    NextPage = result.nextPageUrl;
                    PrevPage = result.prevPageUrl;
                });

                foreach (var player in result.list)
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

        public void Tidy()
        {
            //Players = new ObservableCollection<UserSearch>();
            _logger.AddInfo($"{GetType()} used Tidy.");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private Action<PropertyChangedEventArgs> RaisePropertyChanged() => args => PropertyChanged?.Invoke(this, args);
    }
}
