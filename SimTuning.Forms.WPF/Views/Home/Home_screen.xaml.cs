﻿using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Home
{
    public partial class Home_screen : MvxWpfView/*<HomeMainViewModel>*/
    {
        public Home_screen(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new HomeMainViewModel(mainWindowViewModel);
        }
    }
}