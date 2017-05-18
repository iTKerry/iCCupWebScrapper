using System.Collections.ObjectModel;
using System.ComponentModel;
using Dragablz;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Utils;
using iCCup.UI.Tabablz;
using iCCup.UI.View;
using System;
using GalaSoft.MvvmLight;

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
            get => _selectedIndx;
            set => Set(() => SelectedIndx, ref _selectedIndx, value);
        }

        private bool _showSettings;
        public bool ShowSettings
        {
            get => _showSettings;
            set => Set(() => ShowSettings, ref _showSettings, value);
        }

        private bool _showLog;
        public bool ShowLog
        {
            get => _showLog;
            set => Set(() => ShowLog, ref _showLog, value);
        }
    }
}