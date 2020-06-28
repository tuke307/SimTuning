using SimTuning.ViewModels.Einstellungen;
using SimTuning.windows.ViewModels;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Einstellungen
{
    /// <summary>
    /// Interaktionslogik für Einstellungen.xaml
    /// </summary>
    public partial class EinstellungenMainView : UserControl
    {
        public EinstellungenMainView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new EinstellungenViewModel(mainWindowViewModel);
        }
    }
}