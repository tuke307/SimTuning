using SimTuning.windows.ViewModels;
using SimTuning.windows.ViewModels.Tuning;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Tuning
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