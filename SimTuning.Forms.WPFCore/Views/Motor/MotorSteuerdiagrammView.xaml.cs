using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels.Motor;

namespace SimTuning.Forms.WPFCore.Views.Motor
{
    [MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class MotorSteuerdiagrammView : MvxWpfView<MotorSteuerdiagrammViewModel>
    {
        public MotorSteuerdiagrammView()
        {
            InitializeComponent();
        }
    }
}