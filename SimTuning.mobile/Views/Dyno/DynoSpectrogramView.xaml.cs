using SimTuning.mobile.ViewModels.Dyno;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Dyno
{
    /// <summary>
    /// Interaktionslogik für Dyno_Spectrogram.xaml
    /// </summary>
    public partial class DynoSpectrogramView : ContentView
    {
        public DynoSpectrogramView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            BindingContext = new DynoSpectrogramViewModel(/*mainWindowViewModel*/);
        }
    }
}