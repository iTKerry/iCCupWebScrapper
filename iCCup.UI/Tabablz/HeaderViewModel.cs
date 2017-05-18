using GalaSoft.MvvmLight;

namespace iCCup.UI.Tabablz
{
    public class HeaderViewModel : ViewModelBase
    {
        public HeaderViewModel(string header)
        {
            Header = header;
        }

        private string _header;
        public string Header
        {
            get => _header;
            set => Set(() => Header, ref _header, value);
        }
    }
}
