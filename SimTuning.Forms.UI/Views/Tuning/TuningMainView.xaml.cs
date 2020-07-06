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