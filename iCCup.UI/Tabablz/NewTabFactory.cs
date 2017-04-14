using System;
using Dragablz;
using iCCup.UI.View.Tab;
using iCCup.UI.ViewModel.Tab;

namespace iCCup.UI.Tabablz
{
    public static class TabFactory
    {
        public static Func<HeaderedItemViewModel> Factory { get; } = () =>
        {
            var viewModel = new TabViewModel();
            var view = new TabView {DataContext = viewModel};
            
            return new HeaderedItemViewModel
            {
                Header = "",
                Content = view
            };
        };
    }
}
