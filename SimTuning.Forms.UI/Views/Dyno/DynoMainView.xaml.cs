using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Dyno;

namespace SimTuning.Forms.UI.Views.Dyno
{
    [MvxTabbedPagePresentation(TabbedPosition.Root)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class DynoMainView : MvxTabbedPage<DynoMainViewModel>
    {
        public DynoMainView()
        {
            InitializeComponent();

            //BindingContext = new DynoMainViewModel();
        }
    }
}