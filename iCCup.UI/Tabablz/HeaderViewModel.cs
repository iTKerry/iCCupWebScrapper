using GalaSoft.MvvmLight;

namespace iCCup.UI.Tabablz
{
    public class HeaderViewModel : ViewModelBase
    {
        public HeaderViewModel(string header)
        {
            this.Header = header;
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set { _header = value; RaisePropertyChanged(() => Header); }
        }
    }
}
