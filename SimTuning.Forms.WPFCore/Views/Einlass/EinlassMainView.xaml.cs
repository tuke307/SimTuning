﻿// project=SimTuning.Forms.WPFCore, file=EinlassMainView.xaml.cs, creation=2020:7:7
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Einlass;

namespace SimTuning.Forms.WPFCore.Views.Einlass
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    //[MvxRegionPresentation(RegionName = "PageContent", WindowIdentifier = nameof(MainView))]
    public partial class EinlassMainView : MvxWpfView<EinlassMainViewModel>
    {
        public EinlassMainView()
        {
            InitializeComponent();
        }
    }
}