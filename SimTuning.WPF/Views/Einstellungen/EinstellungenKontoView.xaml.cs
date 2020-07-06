using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Einstellungen;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Einstellungen
{
    public partial class EinstellungenKontoView : UserControl
    {
        public EinstellungenKontoView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new EinstellungenKontoViewModel(mainWindowViewModel);
        }
    }
}