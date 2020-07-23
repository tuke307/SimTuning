using MvvmCross.ViewModels;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels;

namespace SimTuning.Forms.WPFCore.Views
{
    [MvxViewFor(typeof(MenuViewModel))]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = true)]
    [MvxRegionPresentation(RegionName = "MenuContent", WindowIdentifier = nameof(MainWindow))]
    //[MvxRegion("MenuContent")]
    public partial class MenuView //: MvxWpfView<MenuViewModel>
    {
        public MenuView()
        {
            InitializeComponent();
        }
    }
}