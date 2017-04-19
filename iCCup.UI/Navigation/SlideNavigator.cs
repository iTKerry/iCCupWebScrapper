using System;
using System.Collections.Generic;

namespace iCCup.UI.Navigation
{
    internal class SlideNavigator
    {
        private readonly ISlideNavigationSubject _slideNavigationSubject;
        private readonly object[] _slides;
        private readonly LinkedList<SlideNavigatorFrame> _historyLinkedList = new LinkedList<SlideNavigatorFrame>();
        private LinkedListNode<SlideNavigatorFrame> _currentPositionNode;

        public SlideNavigator(ISlideNavigationSubject slideNavigationSubject, object[] slides)
        {
            _slideNavigationSubject = slideNavigationSubject ?? throw new ArgumentNullException(nameof(slideNavigationSubject));
            _slides = slides ?? throw new ArgumentNullException(nameof(slides));
        }

        public void GoTo(int slideIndex) => GoTo(slideIndex, () => { });

        public void GoTo(int slideIndex, Action setupSlide)
        {
            if (_currentPositionNode == null)
            {
                _currentPositionNode = new LinkedListNode<SlideNavigatorFrame>(new SlideNavigatorFrame(slideIndex, setupSlide));
                _historyLinkedList.AddLast(_currentPositionNode);
            }
            else
            {
                var newNode = new LinkedListNode<SlideNavigatorFrame>(new SlideNavigatorFrame(slideIndex, setupSlide));
                _historyLinkedList.AddAfter(_currentPositionNode, newNode);
                _currentPositionNode = newNode;
                var tail = newNode.Next;
                while (tail != null)
                {
                    _historyLinkedList.Remove(tail);
                    tail = tail.Next;
                }
            }

            var tidyable = _slides[_currentPositionNode.Value.SlideIndex] as ITidyable;
            tidyable?.Tidy();
            setupSlide();
            GoTo(_currentPositionNode);
        }

        public void GoBack()
        {
            if (_currentPositionNode?.Previous == null) return;

            _currentPositionNode = _currentPositionNode.Previous;
            GoTo(_currentPositionNode);
        }

        public void GoForward()
        {
            if (_currentPositionNode?.Next == null) return;

            _currentPositionNode = _currentPositionNode.Next;
            GoTo(_currentPositionNode);
        }

        private void GoTo(LinkedListNode<SlideNavigatorFrame> node) =>
            _slideNavigationSubject.ActiveSlideIndex = node.Value.SlideIndex;
    }
}
