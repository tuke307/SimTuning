using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Einstellungen;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Einstellungen
{
    public partial class EinstellungenAussehenView : UserControl
    {
        public EinstellungenAussehenView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new EinstellungenAussehenViewModel(mainWindowViewModel);
        }
    }
}