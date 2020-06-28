//using System.Windows.Controls;
using SimTuning.mobile.ViewModels.Tuning;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Tuning
{
    /// <summary>
    /// Interaktionslogik für Tuning_Durchlauf.xaml
    /// </summary>
    public partial class TuningInputView : ContentPage
    {
        public TuningInputView()
        {
            InitializeComponent();

            BindingContext = new TuningInputViewModel();
        }
    }
}