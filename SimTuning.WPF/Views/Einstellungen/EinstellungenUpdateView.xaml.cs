using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Einstellungen;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Einstellungen
{
    public partial class EinstellungenUpdateView : UserControl
    {
        public EinstellungenUpdateView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new EinstellungenUpdateViewModel(mainWindowViewModel);
        }
    }
}