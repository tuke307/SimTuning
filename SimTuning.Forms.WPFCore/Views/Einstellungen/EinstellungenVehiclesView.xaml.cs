using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Einstellungen;
using System.Windows.Controls;

namespace SimTuning.Forms.WPFCore.Views.Einstellungen
{
    public partial class EinstellungenVehiclesView : MvxWpfView<EinstellungenVehiclesViewModel>
    {
        public EinstellungenVehiclesView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new EinstellungenVehiclesViewModel(mainWindowViewModel);
        }
    }
}