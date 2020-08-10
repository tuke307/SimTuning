// project=SimTuning.Forms.WPFCore, file=MotorMainView.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Motor;

namespace SimTuning.Forms.WPFCore.Views.Motor
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    //[MvxRegionPresentation(RegionName = "PageContent", WindowIdentifier = nameof(MainView))]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class MotorMainView : MvxWpfView<MotorMainViewModel>
    {
        public MotorMainView()
        {
            InitializeComponent();
        }
    }
}