// project=SimTuning.WPF.App, file=DynoDataView.xaml.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.App.Views.Dyno
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