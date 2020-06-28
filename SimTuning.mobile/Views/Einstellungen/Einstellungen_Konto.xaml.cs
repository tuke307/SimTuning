using SimTuning.mobile.ViewModels;
using SimTuning.mobile.ViewModels.Einstellungen;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Einstellungen
{
    /// <summary>
    /// Interaktionslogik für Einstellungen_Konto.xaml
    /// </summary>
    public partial class Einstellungen_Konto : ContentPage
    {
        public Einstellungen_Konto(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            BindingContext = new Einstellungen_KontoViewModel(mainWindowViewModel);
        }
    }
}