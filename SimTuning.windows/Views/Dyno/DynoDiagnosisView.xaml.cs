using SimTuning.windows.ViewModels;
using SimTuning.windows.ViewModels.Dyno;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Dyno
{
    /// <summary>
    /// Interaktionslogik für Dyno_Leistungsdiagnose.xaml
    /// </summary>
    public partial class DynoDiagnosisView : UserControl
    {
        public DynoDiagnosisView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new DynoDiagnosisViewModel(mainWindowViewModel);
        }
    }
}