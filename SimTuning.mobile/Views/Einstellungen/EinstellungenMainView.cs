using SimTuning.mobile.ViewModels;
using SimTuning.mobile.ViewModels.Einstellungen;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Einstellungen
{
    public partial class EinstellungenMainView : TabbedPage
    {
        public EinstellungenVehiclesView Einstellungen_Vehicles { get; set; }
        public EinstellungenKontoView Einstellungen_Konto { get; set; }

        public EinstellungenMainView(MainWindowViewModel mainWindowViewModel)
        {
            //InitializeComponent();

            Einstellungen_Vehicles = new EinstellungenVehiclesView(mainWindowViewModel);
            Einstellungen_Vehicles.Title = "Fahrzeuge";

            Einstellungen_Konto = new EinstellungenKontoView(mainWindowViewModel);
            Einstellungen_Konto.Title = "Konto";

            Children.Add(Einstellungen_Vehicles);
            Children.Add(Einstellungen_Konto);

            BindingContext = new EinstellungenMainViewModel();
        }
    }
}