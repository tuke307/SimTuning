using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Dyno
{
    public partial class DynoDiagnosisView : MvxWpfView/*<DynoDiagnosisViewModel>*/
    {
        public DynoDiagnosisView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new DynoDiagnosisViewModel(mainWindowViewModel);
        }
    }
}