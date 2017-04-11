using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using iCCup.BL.Contracts;
using iCCup.BL.Service;
using Microsoft.Practices.ServiceLocation;

namespace iCCup.UI.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            DispatcherHelper.Initialize();

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IScrapperService, ScrapperService>();

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public SearchUserViewModel SearchUser => ServiceLocator.Current.GetInstance<SearchUserViewModel>();
    }
}