using System;
using Dragablz;
using GalaSoft.MvvmLight.Ioc;
using iCCup.UI.Infrastructure.Contracts;
using iCCup.UI.View;
using iCCup.UI.ViewModel;
using iCCup.UI.ViewModel.Tab;

namespace iCCup.UI.Tabablz
{
    public static class TabFactory
    {
        public static Func<HeaderedItemViewModel> Factory { get; } = () =>
        {
            var hvm = new HeaderViewModel("");
            //var viewModel = new SearchUserViewModel(SimpleIoc.Default.GetInstance<IScrapperService>(), hvm, SimpleIoc.Default.GetInstance<ILoggerService>());
            //var view = new SearchUserView {DataContext = viewModel};
            
            return new HeaderedItemViewModel
            {
                Header = hvm.Header,
                Content = ""
            };
        };
    }
}
