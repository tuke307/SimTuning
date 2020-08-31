// project=SimTuning.Forms.WPFCore, file=DynoDiagnosisView.xaml.cs, creation=2020:7:7
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Dyno;

namespace SimTuning.Forms.WPFCore.Views.Dyno
{
    [MvxWpfPresenter("DynoRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class DynoDiagnosisView : MvxWpfView<DynoDiagnosisViewModel>
    {
        public DynoDiagnosisView()
        {
            InitializeComponent();
        }
    }
}