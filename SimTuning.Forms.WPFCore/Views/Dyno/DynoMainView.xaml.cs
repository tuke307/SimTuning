using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels.Dyno;

namespace SimTuning.Forms.WPFCore.Views.Dyno
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    //[MvxRegionPresentation(RegionName = "PageContent", WindowIdentifier = nameof(MainView))]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class DynoMainView : MvxWpfView<DynoMainViewModel>
    {
        public DynoMainView()
        {
            InitializeComponent();
        }
    }
}