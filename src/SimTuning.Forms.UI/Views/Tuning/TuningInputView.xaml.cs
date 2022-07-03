// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Tuning;

namespace SimTuning.Forms.UI.Views.Tuning
{
    [MvxContentPagePresentation]
    public partial class TuningInputView : MvxContentView<TuningInputViewModel>
    {
        public TuningInputView()
        {
            InitializeComponent();
        }
    }
}