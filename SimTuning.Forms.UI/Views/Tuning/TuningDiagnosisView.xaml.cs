// project=SimTuning.Forms.UI, file=TuningDiagnosisView.xaml.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Tuning;

namespace SimTuning.Forms.UI.Views.Tuning
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class TuningDiagnosisView : MvxContentPage<TuningDiagnosisViewModel>
    {
        public TuningDiagnosisView()
        {
            InitializeComponent();

            //BindingContext = new TuningDiagnosisViewModel();
        }
    }
}