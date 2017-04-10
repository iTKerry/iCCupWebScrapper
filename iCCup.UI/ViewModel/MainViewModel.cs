using System.Collections.ObjectModel;
using Dragablz;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using iCCup.UI.Tabablz;

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

        public MainViewModel()
        {
            SelectedIndx = 0;
            Items = new ObservableCollection<HeaderedItemViewModel> {TabFactory.Factory.Invoke()};
        }

        public ObservableCollection<HeaderedItemViewModel> Items { get; }

        private int _selectedIndx;
        public int SelectedIndx
        {
            get { return _selectedIndx; }
            set { _selectedIndx = value; RaisePropertyChanged(() => SelectedIndx); }
        }
    }
}