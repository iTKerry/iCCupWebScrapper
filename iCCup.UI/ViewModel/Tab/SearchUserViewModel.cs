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
        private readonly IMessangerService _messanger;

        public RelayCommand SearchPlayerCommand =>
            new RelayCommand(async () => await Task.Factory.StartNew(async () => await Search(), _ct));

        public RelayCommand NextPageCommand =>
            new RelayCommand(async () => await Search(isSearch: false));

        public RelayCommand PrevPageCommand =>
            new RelayCommand(async () => await Search(isSearch: false, navigateForward: false));

        public RelayCommand GetPlayerProfileCommand =>
            new RelayCommand(() =>
            {
                var profile = _scrapper.GetUserGameProfile(SelectedUserSearch);
            });

        public RelayCommand GoToProfileCommand =>
            new RelayCommand(() => _messanger.NavigateMessage(new NavigateMessage {NavigateTo = NavigateTo.Profile}));

        public SearchUserViewModel(IScrapperService scrapper, ILoggerService logger, IMessangerService messanger)
        {
            _scrapper = scrapper;
            _messanger = messanger;

            Players = new ObservableCollection<UserSearch>();
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

        #region Navigation

        public void Show()
        {

        }

        public void Tidy()
        {
            Players = new ObservableCollection<UserSearch>();
        }

        private void Init()
        {
            IsBusy = true;

            //

            IsBusy = false;
        }

        #endregion

    }
}
