// project=SimTuning.Forms.UI, file=TuningInputView.xaml.cs, creation=2020:6:28 Copyright
// (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Tuning;

namespace SimTuning.Forms.UI.Views.Tuning
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class TuningInputView : MvxContentPage<TuningInputViewModel>
    {
        public TuningInputView()
        {
            InitializeComponent();

            //BindingContext = new TuningInputViewModel();
        }
    }
}