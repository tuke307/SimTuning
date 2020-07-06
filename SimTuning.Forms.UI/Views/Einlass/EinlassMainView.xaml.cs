using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Einlass;

namespace SimTuning.Forms.UI.Views.Einlass
{
    [MvxTabbedPagePresentation(TabbedPosition.Root)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class EinlassMainView : MvxTabbedPage<EinlassMainViewModel>
    {
        public EinlassMainView()
        {
            InitializeComponent();

            //BindingContext = new EinlassMainViewModel();
        }
    }
}