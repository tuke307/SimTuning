// project=SimTuning.Forms.UI, file=DynoDiagnosisView.xaml.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Dyno;

namespace SimTuning.Forms.UI.Views.Dyno
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class DynoDiagnosisView : MvxContentPage<DynoDiagnosisViewModel>
    {
        public DynoDiagnosisView()
        {
            InitializeComponent();
        }
    }
}