using SimTuning.ViewModels;
using SimTuning.windows.ViewModels;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Einstellungen
{
    public partial class EinstellungenAussehenView : UserControl
    {
        public EinstellungenAussehenView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new Einstellungen_AussehenViewModel(mainWindowViewModel);
        }
    }
}