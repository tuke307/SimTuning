using SimTuning.mobile.ViewModels.Dyno;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Dyno
{
    public partial class DynoAudioView : ContentView
    {
        public DynoAudioView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            BindingContext = new DynoAudioViewModel(/*mainWindowViewModel*/);
        }
    }
}