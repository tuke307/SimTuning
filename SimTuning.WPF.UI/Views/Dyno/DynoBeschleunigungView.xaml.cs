﻿// project=SimTuning.WPF.UI, file=DynoDiagnosisView.xaml.cs, creation=2020:7:7 Copyright
// (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Dyno;

namespace SimTuning.WPF.UI.Views.Dyno
{
    [MvxWpfPresenter("DynoRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class DynoBeschleunigungView : MvxWpfView<DynoBeschleunigungViewModel>
    {
        public DynoBeschleunigungView()
        {
            InitializeComponent();
        }
    }
}