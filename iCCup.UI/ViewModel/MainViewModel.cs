using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iCCup.BL.Contracts;
using iCCup.DATA.Models;

namespace iCCup.UI.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly IScrapperService _scrapper;

        public RelayCommand SearchPlayerCommand =>
            new RelayCommand(async () =>
            {
                Players.Clear();

                var searchResults = await _scrapper.SearchPlayer(PlayerName ?? "");
                NextPage = searchResults.Item2;
                PrevPage = searchResults.Item3;

                foreach (var player in searchResults.Item1)
                {
                    Players.Add(player);
                }
            });

        public RelayCommand NextPageCommand =>
            new RelayCommand(async () =>
            {
                await SearchNavigate(true);
            });

        public RelayCommand PrevPageCommand => 
            new RelayCommand(async () =>
            {
                await SearchNavigate(false);
            });

        public MainViewModel(IScrapperService scrapper)
        {
            _scrapper = scrapper;
            Players = new ObservableCollection<UserSearch>();
        }

        private async Task SearchNavigate(bool ahead)
        {
            Players.Clear();
            var searchResults = await _scrapper.SearchPlayer(ahead
                ? new Uri(NextPage)
                : new Uri(PrevPage));
            NextPage = searchResults.Item2;
            PrevPage = searchResults.Item3;

            foreach (var player in searchResults.Item1)
            {
                Players.Add(player);
            }
        }
    }
}