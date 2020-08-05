using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Home;

namespace SimTuning.Forms.WPFCore.Views.Home
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class HomeMainView : MvxWpfView<HomeMainViewModel>
    {
        public HomeMainView()
        {
            InitializeComponent();
        }
    }
}