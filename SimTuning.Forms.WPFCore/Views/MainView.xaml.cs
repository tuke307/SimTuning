using System.Windows;
using System.Windows.Input;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.Forms.WPFCore.Region;

namespace SimTuning.Forms.WPFCore.Views
{
    [MvxWpfPresenter("MainWindowRegion", mvxViewPosition.NewOrExsist)]
    //[MvxViewFor(typeof(MenuViewModel))]
    //[MvxWpfPresenter("MenuContent", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainView), StackNavigation = false)]
    //[MvxRegionPresentation(RegionName = "MenuContent", WindowIdentifier = nameof(MainView))]
    //[MvxRegion("MenuContent")]
    public partial class MainView : MvxWpfView<SimTuning.Forms.WPFCore.ViewModels.MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}