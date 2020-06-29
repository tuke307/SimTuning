using SimTuning.mobile.ViewModels;
using SimTuning.mobile.ViewModels.Einstellungen;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Einstellungen
{
    /// <summary>
    /// Interaction logic for Einstellungen_Vehicles.xaml
    /// </summary>
    public partial class EinstellungenVehiclesView : ContentPage
    {
        public EinstellungenVehiclesView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            BindingContext = new EinstellungenVehiclesViewModel(mainWindowViewModel);
        }
    }
}