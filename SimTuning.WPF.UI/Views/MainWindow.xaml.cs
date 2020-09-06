// project=SimTuning.WPF.UI, file=MainWindow.xaml.cs, creation=2020:7:9 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views
{
    using MvvmCross.Platforms.Wpf.Presenters.Attributes;
    using MvvmCross.Platforms.Wpf.Views;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// MainWindow.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWindow" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWindowPresentation(Modal = false)]
    public partial class MainWindow : MvxWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void GridTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void WindowClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void WindowMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized) { this.WindowState = WindowState.Normal; }
            else if (this.WindowState == WindowState.Normal) { this.WindowState = WindowState.Maximized; }
        }

        private void WindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}