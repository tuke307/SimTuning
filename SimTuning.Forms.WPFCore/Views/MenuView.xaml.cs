using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels;

namespace SimTuning.Forms.WPFCore.Views
{
    //[MvxViewFor(typeof(MenuViewModel))]
    [MvxWpfPresenter("MenuContent", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainView), StackNavigation = false)]
    //[MvxRegionPresentation(RegionName = "MenuContent", WindowIdentifier = nameof(MainView))]
    //[MvxRegion("MenuContent")]
    public partial class MenuView //: MvxWpfView/*<MenuViewModel>*/
    {
        public MenuView()
        {
            InitializeComponent();
        }
    }
}