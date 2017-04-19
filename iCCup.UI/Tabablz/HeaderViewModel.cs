using System;
using System.ComponentModel;
using iCCup.UI.Infrastructure.Utils;

namespace iCCup.UI.Tabablz
{
    public class HeaderViewModel : INotifyPropertyChanged
    {
        public HeaderViewModel(string header)
        {
            Header = header;
        }

        private string _header;
        public string Header
        {
            get => _header;
            set => this.MutateVerbose(ref _header, value, RaisePropertyChanged());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
