// project=SimTuning.Forms.WPFCore, file=DynoDataView.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.Views.Dyno
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.Forms.WPFCore.Region;
    using SimTuning.Forms.WPFCore.ViewModels.Dyno;

    [MvxWpfPresenter("DynoRegion", mvxViewPosition.NewOrExsist)]
    public partial class DynoDataView : MvxWpfView<DynoDataViewModel>
    {
        public DynoDataView()
        {
            InitializeComponent();
        }
    }
}