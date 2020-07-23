using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Tuning
{
    public partial class TuningDiagnosisView : MvxWpfView/*<TuningDiagnosisViewModel>*/
    {
        public TuningDiagnosisView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new TuningDiagnosisViewModel(mainWindowViewModel);
        }
    }
}