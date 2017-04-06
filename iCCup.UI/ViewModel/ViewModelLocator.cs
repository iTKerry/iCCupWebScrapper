using GalaSoft.MvvmLight.Ioc;
using iCCup.BL.Contracts;
using iCCup.BL.Service;
using Microsoft.Practices.ServiceLocation;

namespace iCCup.UI.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IScrapperService, ScrapperService>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
    }
}