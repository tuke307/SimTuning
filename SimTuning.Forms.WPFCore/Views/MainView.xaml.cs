using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.WPFCore.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SimTuning.Forms.WPFCore.Views
{
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = true)]
    //[MvxWindowPresentation(Identifier = nameof(MainView), Modal = false)]
    //[MvxViewFor(typeof(MainViewModel))]
    public partial class MainView /*: MvxWpfView*//*: MvxWindow<RootWindowViewModel>*/
    {
        public MainView()
        {
            this.InitializeComponent();
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