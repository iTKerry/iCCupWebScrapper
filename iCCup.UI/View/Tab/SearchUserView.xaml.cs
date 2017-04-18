using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using iCCup.DATA.Models;
using iCCup.UI.ViewModel.Tab;

namespace iCCup.UI.View.Tab
{
    public partial class SearchUserView
    {
        private SearchUserViewModel _viewModel;

        public SearchUserView()
        {
            InitializeComponent();
            DataContextChanged += (sender, args) => _viewModel = (SearchUserViewModel) DataContext;
        }

        private void SearchUserListItemClicked(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, (DependencyObject) e.OriginalSource) as ListBoxItem;
            if (item != null)
                _viewModel.GoToProfileCommand.Execute((UserSearch)item.Content);
        }
    }
}
