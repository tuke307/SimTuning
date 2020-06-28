using SimTuning.ViewModels.Einstellungen;
using SimTuning.windows.ViewModels;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Einstellungen
{
    /// <summary>
    /// Interaktionslogik für Update.xaml
    /// </summary>
    public partial class EinstellungenUpdateView : UserControl
    {
        public EinstellungenUpdateView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new Einstellungen_UpdateViewModel(mainWindowViewModel);
        }
    }
}