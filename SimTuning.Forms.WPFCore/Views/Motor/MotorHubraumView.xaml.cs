using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Motor;

namespace SimTuning.Forms.WPFCore.Views.Motor
{
    [MvxWpfPresenter("MotorRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class MotorHubraumView : MvxWpfView<MotorHubraumViewModel>
    {
        public MotorHubraumView()
        {
            InitializeComponent();
        }
    }
}