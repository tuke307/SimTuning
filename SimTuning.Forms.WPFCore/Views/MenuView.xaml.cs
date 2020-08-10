// project=SimTuning.Forms.WPFCore, file=MenuView.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SimTuning.Forms.WPFCore.Views
{
    [MvxWpfPresenter("MainViewRegion", mvxViewPosition.NewOrExsist)]
    public partial class MenuView : MvxWpfView<MenuViewModel>
    {
        public MenuView()
        {
            this.InitializeComponent();
        }
    }
}