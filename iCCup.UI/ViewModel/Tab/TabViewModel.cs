using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using iCCup.UI.Infrastructure.Contracts;
using iCCup.UI.Navigation;
using iCCup.UI.Tabablz;
using iCCup.UI.View.Tab;
using NavigationCommands = iCCup.UI.Navigation.NavigationCommands;

namespace iCCup.UI.ViewModel.Tab
{
    public class TabViewModel : ViewModelBase, ISlideNavigationSubject
    {
        private readonly SlideNavigator _slideNavigator;
        
        public TabViewModel()
        {
            CommandManager.RegisterClassCommandBinding(typeof(TabView), new CommandBinding(NavigationCommands.GoBackCommand, GoBackExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(TabView), new CommandBinding(NavigationCommands.GoForwardCommand, GoForwardExecuted));

            Slides = new object[] {SearchUserViewModel};
            _slideNavigator = new SlideNavigator(this, Slides);
            _slideNavigator.GoTo(0);
        }

        public object[] Slides { get; }

        public SearchUserViewModel SearchUserViewModel { get; } =
            new SearchUserViewModel(
                SimpleIoc.Default.GetInstance<IScrapperService>(),
                SimpleIoc.Default.GetInstance<ILoggerService>(),
                new HeaderViewModel("Header"));

        private int _activeSlideIndex;
        public int ActiveSlideIndex
        {
            get { return _activeSlideIndex; }
            set { _activeSlideIndex = value; RaisePropertyChanged(() => ActiveSlideIndex); }
        }

        private void GoBackExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoBack();
        }

        private void GoForwardExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _slideNavigator.GoForward();
        }
    }
}
