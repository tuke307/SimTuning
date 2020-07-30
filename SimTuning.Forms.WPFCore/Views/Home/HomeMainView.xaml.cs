using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.ViewModels;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Home;

namespace SimTuning.Forms.WPFCore.Views.Home
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    //[MvxRegionPresentation(RegionName = "PageContent", WindowIdentifier = nameof(MainView))]
    //[MvxRegionPresentation("PageContent")]
    //[MvxRegion("PageContent")]
    //[MvxViewFor(typeof(HomeMainViewModel))]
    public partial class HomeMainView /*: MvxWpfView<HomeMainViewModel>*/
    {
        public HomeMainView()
        {
            InitializeComponent();
        }
    }
}