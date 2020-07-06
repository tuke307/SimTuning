using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Einstellungen;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Einstellungen
{
    public partial class EinstellungenVehiclesView : UserControl
    {
        public EinstellungenVehiclesView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new EinstellungenVehiclesViewModel(mainWindowViewModel);
        }
    }
}