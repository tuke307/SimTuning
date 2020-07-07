using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SimTuning.Forms.WPFCore.Views
{
    public partial class MainWindow : MvxWindow<MainWindowViewModel>
    {
        //private bool _firstTime = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        //protected override void OnAppearing()
        //{
        //    if (_firstTime)
        //    {
        //        ViewModel.ShowMenuViewModelCommand.Execute(null);
        //        ViewModel.ShowHomeViewModelCommand.Execute(null);

        //        _firstTime = false;
        //    }

        //    base.OnAppearing();
        //}

        private void GridTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void WindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void WindowMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized) { this.WindowState = WindowState.Normal; }
            else if (this.WindowState == WindowState.Normal) { this.WindowState = WindowState.Maximized; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}