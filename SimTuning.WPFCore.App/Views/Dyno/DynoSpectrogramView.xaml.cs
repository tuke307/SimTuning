// project=SimTuning.WPF.UI, file=DynoSpectrogramView.xaml.cs, creation=2020:7:7 Copyright
// (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Dyno;

namespace SimTuning.WPFCore.App.Views.Dyno
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