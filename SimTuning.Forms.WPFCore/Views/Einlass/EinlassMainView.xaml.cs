using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels.Einlass;

namespace SimTuning.Forms.WPFCore.Views.Einlass
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    //[MvxRegionPresentation(RegionName = "PageContent", WindowIdentifier = nameof(MainView))]
    public partial class EinlassMainView : MvxWpfView<EinlassMainViewModel>
    {
        public EinlassMainView()
        {
            InitializeComponent();
        }
    }
}