// project=SimTuning.WPF.App, file=MotorHubraumView.xaml.cs, creation=2020:9:2 Copyright
// (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Motor;

namespace SimTuning.WPF.App.Views.Motor
{
    [MvxWpfPresenter("MotorRegion", mvxViewPosition.NewOrExsist)]
    public partial class MotorHubraumView : MvxWpfView<MotorHubraumViewModel>
    {
        public MotorHubraumView()
        {
            InitializeComponent();
        }
    }
}