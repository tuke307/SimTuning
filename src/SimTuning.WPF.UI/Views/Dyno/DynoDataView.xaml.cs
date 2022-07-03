// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views.Dyno
{
    using MvvmCross.Platforms.Wpf.Views;
    using Mvx.Wpf.ItemsPresenter;
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