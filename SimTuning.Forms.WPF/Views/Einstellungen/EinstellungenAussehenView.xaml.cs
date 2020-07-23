using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Einstellungen
{
    public partial class EinstellungenAussehenView : MvxWpfView/*<EinstellungenAussehenViewModel>*/
    {
        public EinstellungenAussehenView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new EinstellungenAussehenViewModel(mainWindowViewModel);
        }
    }
}