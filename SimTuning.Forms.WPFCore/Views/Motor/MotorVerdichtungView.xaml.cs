// project=SimTuning.Forms.WPFCore, file=MotorVerdichtungView.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Motor;

namespace SimTuning.Forms.WPFCore.Views.Motor
{
    [MvxWpfPresenter("MotorRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class MotorVerdichtungView : MvxWpfView<MotorVerdichtungViewModel>
    {
        public MotorVerdichtungView()
        {
            InitializeComponent();
        }
    }
}