using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace SimTuning.Forms.UI.Views.Auslass
{
    [MvxTabbedPagePresentation(TabbedPosition.Root)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class AuslassMainView : MvxTabbedPage<SimTuning.Forms.UI.ViewModels.Auslass.AuslassMainViewModel>
    {
        public AuslassMainView()
        {
            InitializeComponent();

            //BindingContext = new AuslassMainViewModel();
        }
    }
}