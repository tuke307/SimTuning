// project=SimTuning.WPF.App, file=EinlassMainView.xaml.cs, creation=2020:9:2 Copyright
// (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Einlass;

namespace SimTuning.WPF.App.Views.Einlass
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