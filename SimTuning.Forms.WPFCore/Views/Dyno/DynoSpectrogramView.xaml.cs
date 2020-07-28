using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels.Dyno;

namespace SimTuning.Forms.WPFCore.Views.Dyno
{
    [MvxWpfPresenter("DynoRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class DynoSpectrogramView : MvxWpfView<DynoSpectrogramViewModel>
    {
        public DynoSpectrogramView()
        {
            InitializeComponent();
        }
    }
}