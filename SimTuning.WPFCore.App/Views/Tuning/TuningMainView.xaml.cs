﻿// project=SimTuning.WPF.UI, file=TuningMainView.xaml.cs, creation=2020:7:7 Copyright (c)
// 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Tuning;

namespace SimTuning.WPFCore.App.Views.Tuning
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class TuningMainView : MvxWpfView<TuningMainViewModel>
    {
        public TuningMainView()
        {
            InitializeComponent();
        }
    }
}