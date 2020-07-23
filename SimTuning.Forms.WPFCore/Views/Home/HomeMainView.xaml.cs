using MvvmCross.ViewModels;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels.Home;

namespace SimTuning.Forms.WPFCore.Views.Home
{
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = true)]
    [MvxRegionPresentation(RegionName = "PageContent", WindowIdentifier = nameof(MainWindow))]
    //[MvxRegion("PageContent")]
    [MvxViewFor(typeof(HomeMainViewModel))]
    public partial class HomeMainView /*: MvxWpfView<HomeMainViewModel>*/
    {
        public HomeMainView()
        {
            InitializeComponent();
        }
    }
}