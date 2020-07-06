using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Dyno;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Dyno
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