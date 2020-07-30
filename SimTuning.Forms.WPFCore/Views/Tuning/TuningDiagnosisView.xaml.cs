using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Tuning;

namespace SimTuning.Forms.WPFCore.Views.Tuning
{
    [MvxWpfPresenter("TuningRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class TuningDiagnosisView : MvxWpfView<TuningDiagnosisViewModel>
    {
        public TuningDiagnosisView()
        {
            InitializeComponent();
        }
    }
}