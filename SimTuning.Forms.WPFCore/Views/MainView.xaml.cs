using System.Windows;
using System.Windows.Input;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels;

namespace SimTuning.Forms.WPFCore.Views
{
    //[MvxViewFor(typeof(MenuViewModel))]
    //[MvxWpfPresenter("MenuContent", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainView), StackNavigation = false)]
    //[MvxRegionPresentation(RegionName = "MenuContent", WindowIdentifier = nameof(MainView))]
    //[MvxRegion("MenuContent")]
    public partial class MainView : MvxWpfView<MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void GridTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }

        private void WindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            //this.WindowState = WindowState.Minimized;
        }

        private void WindowMaximize_Click(object sender, RoutedEventArgs e)
        {
            //if (this.WindowState == WindowState.Maximized) { this.WindowState = WindowState.Normal; }
            //else if (this.WindowState == WindowState.Normal) { this.WindowState = WindowState.Maximized; }
        }

        private void WindowClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}