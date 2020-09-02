﻿// project=SimTuning.WPF.UI, file=DynoDataView.xaml.cs, creation=2020:7:7 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPFCore.App.Views.Dyno
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.WPF.UI.Region;
    using SimTuning.WPF.UI.ViewModels.Dyno;

    [MvxWpfPresenter("DynoRegion", mvxViewPosition.NewOrExsist)]
    public partial class DynoDataView : MvxWpfView<DynoDataViewModel>
    {
        public DynoDataView()
        {
            InitializeComponent();
        }
    }
}