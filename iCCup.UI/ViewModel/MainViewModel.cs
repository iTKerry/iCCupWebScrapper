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

namespace iCCup.UI.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
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
            set => this.MutateVerbose(ref _selectedIndx, value, RaisePropertyChanged());
        }

        private bool _showSettings;
        public bool ShowSettings
        {
            get => _showSettings;
            set => this.MutateVerbose(ref _showSettings, value, RaisePropertyChanged());
        }

        private bool _showLog;
        public bool ShowLog
        {
            get => _showLog;
            set => this.MutateVerbose(ref _showLog, value, RaisePropertyChanged());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private Action<PropertyChangedEventArgs> RaisePropertyChanged() => args => PropertyChanged?.Invoke(this, args);
    }
}