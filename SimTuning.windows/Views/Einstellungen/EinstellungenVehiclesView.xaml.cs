using SimTuning.ViewModels.Einstellungen;
using SimTuning.windows.ViewModels;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Einstellungen
{
    /// <summary>
    /// Interaction logic for Einstellungen_Vehicles.xaml
    /// </summary>
    public partial class EinstellungenVehiclesView : UserControl
    {
        public EinstellungenVehiclesView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new EinstellungenVehiclesViewModel(mainWindowViewModel);
        }
    }
}