﻿// project=SimTuning.WPF.App, file=DynoSpectrogramView.xaml.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Dyno;

namespace SimTuning.WPF.App.Views.Dyno
{
    [MvxWpfPresenter("DynoRegion", mvxViewPosition.NewOrExsist)]
    public partial class DynoSpectrogramView : MvxWpfView<DynoSpectrogramViewModel>
    {
        public DynoSpectrogramView()
        {
            InitializeComponent();
        }
    }
}