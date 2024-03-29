﻿// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using Mvx.Wpf.ItemsPresenter;
using SimTuning.WPF.UI.ViewModels.Einlass;

namespace SimTuning.WPF.UI.Views.Einlass
{
    [MvxWpfPresenter("EinlassRegion", mvxViewPosition.NewOrExsist)]
    public partial class EinlassKanalView : MvxWpfView<EinlassKanalViewModel>
    {
        public EinlassKanalView()
        {
            InitializeComponent();
        }
    }
}