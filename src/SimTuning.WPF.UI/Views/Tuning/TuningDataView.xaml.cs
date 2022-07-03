// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using Mvx.Wpf.ItemsPresenter;
using SimTuning.WPF.UI.ViewModels.Tuning;

namespace SimTuning.WPF.UI.Views.Tuning
{
    [MvxWpfPresenter("TuningRegion", mvxViewPosition.NewOrExsist)]
    public partial class TuningDataView : MvxWpfView<TuningDataViewModel>
    {
        public TuningDataView()
        {
            InitializeComponent();
        }
    }
}