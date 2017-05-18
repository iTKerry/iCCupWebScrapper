using System;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;
using iCCup.UI.Navigation;

namespace iCCup.UI.ViewModel.Tab
{
    public class TabViewModel : ViewModelBase, ISlideNavigationSubject
    {
        private readonly SlideNavigator _slideNavigator;
        
        public TabViewModel()
        {
            GameProfileViewModel = new GameProfileViewModel(this,
                SimpleIoc.Default.GetInstance<IScrapperService>());
            SearchUserViewModel = new SearchUserViewModel(this,
                SimpleIoc.Default.GetInstance<IScrapperService>(),
                SimpleIoc.Default.GetInstance<ILoggerService>());

            Slides = new object[] {SearchUserViewModel, GameProfileViewModel};

            _slideNavigator = new SlideNavigator(this, Slides);
            _slideNavigator.GoTo(0);
        }

        public void Navigate(NavigateMessage message)
        {
            switch (message.NavigateTo)
            {
                case NavigateTo.Search:
                    _slideNavigator.GoTo(IndexOfSlide<SearchUserViewModel>());
                    break;
                case NavigateTo.Profile:
                    var content = (UserSearch) message.Content;
                    if (content.Url == (GameProfileViewModel.UserGameProfile?.Url ?? String.Empty))
                    {
                        _slideNavigator.GoForward();
                        break;
                    }
                    _slideNavigator.GoTo(IndexOfSlide<GameProfileViewModel>(), async () => await GameProfileViewModel.Show((UserSearch)message.Content));
                    break;
                case NavigateTo.GameDetails:
                    //TODO: GameDetails navigation here
                    break;
                case NavigateTo.Forward:
                    _slideNavigator.GoForward();
                    break;
                case NavigateTo.Back:
                    _slideNavigator.GoBack();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object[] Slides { get; }

        public SearchUserViewModel SearchUserViewModel { get; }

        public GameProfileViewModel GameProfileViewModel { get; }

        private int _activeSlideIndex;
        public int ActiveSlideIndex
        {
            get => _activeSlideIndex;
            set => Set(() => ActiveSlideIndex, ref _activeSlideIndex, value);
        }

        private int IndexOfSlide<TSlide>()
        {
            return Slides.Select((o, i) => new { o, i }).First(a => a.o.GetType() == typeof(TSlide)).i;
        }
    }
}
