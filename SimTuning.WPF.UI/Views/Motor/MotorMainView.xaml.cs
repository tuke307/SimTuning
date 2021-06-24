// project=SimTuning.WPF.UI, file=MotorMainView.xaml.cs, creation=2020:9:7 Copyright (c)
// 2021 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using Mvx.Wpf.ItemsPresenter;
using SimTuning.WPF.UI.ViewModels.Motor;

namespace SimTuning.WPF.UI.Views.Motor
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class MotorMainView : MvxWpfView<MotorMainViewModel>
    {
        public MotorMainView()
        {
            InitializeComponent();
        }
    }
}