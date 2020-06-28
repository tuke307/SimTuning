using SimTuning.ViewModels.Einstellungen;
using SimTuning.windows.ViewModels;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Einstellungen
{
    /// <summary>
    /// Interaktionslogik für Einstellungen_Konto.xaml
    /// </summary>
    public partial class EinstellungenKontoView : UserControl
    {
        public EinstellungenKontoView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new Einstellungen_KontoViewModel(mainWindowViewModel);
        }
    }
}