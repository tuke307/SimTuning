using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels;

namespace SimTuning.Forms.UI.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, NoHistory = true)]
    public partial class MainPage : MvxMasterDetailPage<MainPageViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}