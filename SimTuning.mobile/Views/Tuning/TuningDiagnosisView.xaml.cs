//using SimTuning.mobile.ViewModels;
//using System.Windows.Controls;
using SimTuning.mobile.ViewModels;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Tuning
{
    /// <summary>
    /// Interaktionslogik für LeisungsdiagnoseSite.xaml
    /// </summary>
    public partial class TuningDiagnosisView : ContentPage
    {
        public TuningDiagnosisView()
        {
            InitializeComponent();

            BindingContext = new TuningDiagnosisViewModel();
        }
    }
}