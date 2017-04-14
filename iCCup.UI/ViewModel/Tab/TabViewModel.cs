using System;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;
using iCCup.UI.Navigation;
using iCCup.UI.Tabablz;

namespace iCCup.UI.ViewModel.Tab
{
    public class TabViewModel : ViewModelBase, ISlideNavigationSubject
    {
        private readonly SlideNavigator _slideNavigator;
        
        public TabViewModel()
        {
            Slides = new object[] {SearchUserViewModel, GameProfileViewModel};
            _slideNavigator = new SlideNavigator(this, Slides);
            _slideNavigator.GoTo(0);

            Messenger.Default.Register<NotificationMessage<NavigateMessage>>(this, Navigate);
        }

        private void Navigate(NotificationMessage<NavigateMessage> message)
        {
            switch (message.Content.NavigateTo)
            {
                case NavigateTo.Search:
                    _slideNavigator.GoTo(IndexOfSlide<SearchUserViewModel>());
                    break;
                case NavigateTo.Profile:
                    _slideNavigator.GoTo(
                    IndexOfSlide<GameProfileViewModel>(),
                        async () => await GameProfileViewModel.Show((UserSearch) message.Content.Content));
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

        public SearchUserViewModel SearchUserViewModel { get; } =
            new SearchUserViewModel(
                SimpleIoc.Default.GetInstance<IScrapperService>(),
                SimpleIoc.Default.GetInstance<ILoggerService>(),
                SimpleIoc.Default.GetInstance<IMessangerService>());

        public GameProfileViewModel GameProfileViewModel { get; } = 
            new GameProfileViewModel(SimpleIoc.Default.GetInstance<IMessangerService>());

        private HeaderViewModel _hvm;
        public HeaderViewModel Hvm
        {
            get { return _hvm; }
            set { _hvm = value; RaisePropertyChanged<HeaderViewModel>(() => Hvm); }
        }

        public string Header
        {
            get { return Hvm.Header; }
            set { Hvm.Header = value; RaisePropertyChanged(() => Hvm); }
        }

        private int _activeSlideIndex;
        public int ActiveSlideIndex
        {
            get { return _activeSlideIndex; }
            set { _activeSlideIndex = value; RaisePropertyChanged(() => ActiveSlideIndex); }
        }

        private int IndexOfSlide<TSlide>()
        {
            return Slides.Select((o, i) => new { o, i }).First(a => a.o.GetType() == typeof(TSlide)).i;
        }
    }
}
