using System;
using Dragablz;
using GalaSoft.MvvmLight.Ioc;
using iCCup.BL.Contracts;
using iCCup.UI.View;
using iCCup.UI.ViewModel;

namespace iCCup.UI.Tabablz
{
    public static class TabFactory
    {
        public static Func<HeaderedItemViewModel> Factory { get; } = () =>
        {
            var dateTime = DateTime.Now;

            var hvm = new HeaderViewModel("");
            var viewModel = new SearchUserViewModel(SimpleIoc.Default.GetInstance<IScrapperService>(), hvm);
            var view = new SearchUserView {DataContext = viewModel};
            
            return new HeaderedItemViewModel
            {
                Header = hvm.Header,
                Content = view
            };
        };
    }
}
