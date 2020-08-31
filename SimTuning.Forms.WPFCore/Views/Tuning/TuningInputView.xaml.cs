﻿// project=SimTuning.Forms.WPFCore, file=TuningInputView.xaml.cs, creation=2020:7:7
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Tuning;

namespace SimTuning.Forms.WPFCore.Views.Tuning
{
    [MvxWpfPresenter("TuningRegion", mvxViewPosition.NewOrExsist)]
    public partial class TuningInputView : MvxWpfView<TuningInputViewModel>
    {
        public TuningInputView()
        {
            InitializeComponent();
        }
    }
}