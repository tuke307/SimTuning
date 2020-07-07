using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Tuning;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Tuning
{
    public partial class TuningDiagnosisView : MvxWpfView<TuningDiagnosisViewModel>
    {
        public TuningDiagnosisView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new TuningDiagnosisViewModel(mainWindowViewModel);
        }
    }
}