using SimTuning.mobile.ViewModels;
using SimTuning.mobile.ViewModels.Einstellungen;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Einstellungen
{
    public partial class EinstellungenKontoView : ContentPage
    {
        public EinstellungenKontoView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            BindingContext = new EinstellungenKontoViewModel(mainWindowViewModel);
        }
    }
}