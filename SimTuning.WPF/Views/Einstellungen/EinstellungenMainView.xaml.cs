using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Einstellungen;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Einstellungen
{
    public partial class EinstellungenMainView : UserControl
    {
        public EinstellungenMainView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new EinstellungenMainViewModel(mainWindowViewModel);
        }
    }
}