using SimTuning.mobile.ViewModels;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Einstellungen
{
    /// <summary>
    /// Interaktionslogik für Einstellungen.xaml
    /// </summary>
    public partial class Einstellungen_main : TabbedPage
    {
        public Einstellungen_Vehicles Einstellungen_Vehicles { get; set; }
        public Einstellungen_Konto Einstellungen_Konto { get; set; }

        public Einstellungen_main(MainWindowViewModel mainWindowViewModel)
        {
            //InitializeComponent();

            Einstellungen_Vehicles = new Einstellungen_Vehicles(mainWindowViewModel);
            Einstellungen_Vehicles.Title = "Fahrzeuge";

            Einstellungen_Konto = new Einstellungen_Konto(mainWindowViewModel);
            Einstellungen_Konto.Title = "Konto";

            Children.Add(Einstellungen_Vehicles);
            Children.Add(Einstellungen_Konto);
            //BindingContext = new EinstellungenViewModel(mainWindowViewModel);
        }
    }
}