using System;
using Microsoft.VisualBasic.CompilerServices;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Auslass;

namespace SimTuning.Forms.WPFCore.Views.Auslass
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    //[MvxRegionPresentation(RegionName = "PageContent", WindowIdentifier = nameof(MainView))]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class AuslassMainView : MvxWpfView<AuslassMainViewModel>
    {
        public AuslassMainView()
        {
            InitializeComponent();
        }
    }
}