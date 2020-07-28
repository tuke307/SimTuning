using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels.Tuning;

namespace SimTuning.Forms.WPFCore.Views.Tuning
{
    [MvxWpfPresenter("TuningRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class TuningInputView : MvxWpfView<TuningInputViewModel>
    {
        public TuningInputView()
        {
            InitializeComponent();
        }
    }
}