using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iCCup.BL.Scrapper;
using iCCup.DATA.Models;

namespace iCCup.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand SearchPlayerCommand =>
            new RelayCommand(async () =>
            {
                Players.Clear();
                var test = new ScrapperBase();
                var searchResults = await test.SearchPlayer(PlayerName ?? "");

                foreach (var player in searchResults)
                {
                    Players.Add(player);
                }
            });

        public MainViewModel()
        {
            Players = new ObservableCollection<UserSearch>();
        }

        private string _playerName;
        public string PlayerName
        {
            get {return _playerName;}
            set { _playerName = value; RaisePropertyChanged(() => PlayerName); }
        }

        private ObservableCollection<UserSearch> _players;
        public ObservableCollection<UserSearch> Players
        {
            get { return _players; }
            set { _players = value; RaisePropertyChanged(() => Players); }
        }
    }
}