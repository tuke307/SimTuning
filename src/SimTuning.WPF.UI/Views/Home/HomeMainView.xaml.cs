﻿// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using Mvx.Wpf.ItemsPresenter;
using SimTuning.WPF.UI.ViewModels.Home;

namespace SimTuning.WPF.UI.Views.Home
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class HomeMainView : MvxWpfView<HomeMainViewModel>
    {
        public HomeMainView()
        {
            InitializeComponent();
        }
    }
}