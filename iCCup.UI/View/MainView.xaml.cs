using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Threading;
using iCCup.DATA.Models;
using iCCup.UI.ViewModel;

namespace iCCup.UI.View
{
    public partial class MainView : IMainView
    {
        public MainView()
        {
            InitializeComponent();

            var viewModel = (MainViewModel)DataContext;
            if (viewModel != null) viewModel.View = this;

#if DEBUG
            LogButton.Visibility = Visibility.Visible;
#endif
        }

        public async Task AddToLog(LogMessange content)
        {
            await Task.Factory.StartNew(() 
                => DispatcherHelper.CheckBeginInvokeOnUI(()
                    =>
                    {
                        LoggerBox.Document.Blocks.Add(content.Content);
                        LoggerBox.ScrollToEnd();
                    }));
        }
    }

    public interface IMainView
    {
        Task AddToLog(LogMessange content);
    }
}
