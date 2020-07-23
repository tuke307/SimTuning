using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Einstellungen
{
    public partial class EinstellungenVehiclesView : MvxWpfView/*<EinstellungenVehiclesViewModel>*/
    {
        public EinstellungenVehiclesView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new EinstellungenVehiclesViewModel(mainWindowViewModel);
        }
    }
}