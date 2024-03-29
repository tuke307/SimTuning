﻿// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using Mvx.Wpf.ItemsPresenter;
using SimTuning.WPF.UI.ViewModels.Demo;

namespace SimTuning.WPF.UI.Views.Demo
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    // [MvxRegionPresentation(RegionName = "PageContent", WindowIdentifier =
    // nameof(MainView))] [MvxContentPresentation(WindowIdentifier = nameof(MainWindow),
    // StackNavigation = false)]
    public partial class BuyProView : MvxWpfView<DemoMainViewModel>
    {
        public BuyProView()
        {
            InitializeComponent();
        }
    }
}