// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.Views.Dyno
{
    using MvvmCross.Forms.Presenters.Attributes;
    using MvvmCross.Forms.Views;
    using SimTuning.Forms.UI.ViewModels.Dyno;

    /// <summary>
    /// DynoDiagnosisView.
    /// </summary>
    [MvxModalPresentation]
    public partial class DynoDiagnosisView : MvxContentPage<DynoDiagnosisViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDiagnosisView" /> class.
        /// </summary>
        public DynoDiagnosisView()
        {
            InitializeComponent();
        }
    }
}