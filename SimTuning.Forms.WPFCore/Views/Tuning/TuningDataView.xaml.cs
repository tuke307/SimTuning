// project=SimTuning.Forms.WPFCore, file=TuningDataView.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Tuning;

namespace SimTuning.Forms.WPFCore.Views.Tuning
{
    [MvxWpfPresenter("TuningRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class TuningDataView : MvxWpfView<TuningDataViewModel>
    {
        public TuningDataView()
        {
            InitializeComponent();
        }
    }
}