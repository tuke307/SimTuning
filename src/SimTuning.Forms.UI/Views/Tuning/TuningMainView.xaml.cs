// project=SimTuning.Forms.UI, file=TuningMainView.xaml.cs, creation=2020:6:28 Copyright
// (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Tuning;

namespace SimTuning.Forms.UI.Views.Tuning
{
    [MvxTabbedPagePresentation(TabbedPosition.Root)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class TuningMainView : MvxTabbedPage<TuningMainViewModel>
    {
        public TuningMainView()
        {
            InitializeComponent();

            //BindingContext = new TuningMainViewModel();
        }
    }
}