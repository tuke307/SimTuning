using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Tuning;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Tuning
{
    /// <summary>
    /// Interaktionslogik für LeisungsdiagnoseSite.xaml
    /// </summary>
    public partial class TuningDiagnosisView : UserControl
    {
        public TuningDiagnosisView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new TuningDiagnosisViewModel(mainWindowViewModel);
        }
    }
}