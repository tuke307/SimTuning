using SimTuning.mobile.ViewModels.Dyno;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Dyno
{
    /// <summary>
    /// Interaktionslogik für Dyno_Leistungsdiagnose.xaml
    /// </summary>
    public partial class DynoDiagnosisView : ContentView
    {
        public DynoDiagnosisView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            BindingContext = new DynoDiagnosisViewModel(/*mainWindowViewModel*/);
        }
    }
}