using System.Collections.ObjectModel;
using Dragablz;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using iCCup.DATA.Models;
using iCCup.UI.Tabablz;
using iCCup.UI.View;

namespace iCCup.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand AddTabItemCommand => new RelayCommand(() =>
        {
            var item = TabFactory.Factory.Invoke();
            Items.Add(item);
            SelectedIndx = Items.IndexOf(item);
        });
        
        public RelayCommand ShowSettingsCommand => new RelayCommand(() => ShowSettings = true);
        public RelayCommand ShowLogCommand => new RelayCommand(() => ShowLog = true);

        public MainViewModel()
        {
            Items = new ObservableCollection<HeaderedItemViewModel> {TabFactory.Factory.Invoke()};

            Messenger.Default.Register<NotificationMessage<LogMessange>>(this,
                async messange => await View.AddToLog(messange.Content));
        }

        public ObservableCollection<HeaderedItemViewModel> Items { get; }

        public MainView View { get; set; }

        private int _selectedIndx;
        public int SelectedIndx
        {
            get { return _selectedIndx; }
            set { _selectedIndx = value; RaisePropertyChanged(() => SelectedIndx); }
        }

        private bool _showSettings;
        public bool ShowSettings
        {
            get { return _showSettings; }
            set { _showSettings = value; RaisePropertyChanged(() => ShowSettings); }
        }

        private bool _showLog;
        public bool ShowLog
        {
            get { return _showLog; }
            set { _showLog = value; RaisePropertyChanged(() => ShowLog); }
        }
    }
}