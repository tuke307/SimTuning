using SimTuning.mobile.ViewModels;
using SimTuning.mobile.ViewModels.Einstellungen;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Einstellungen
{
    /// <summary>
    /// Interaction logic for Einstellungen_Vehicles.xaml
    /// </summary>
    public partial class Einstellungen_Vehicles : ContentPage
    {
        public Einstellungen_Vehicles(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            BindingContext = new Einstellungen_VehiclesViewModel(mainWindowViewModel);
        }
    }
}