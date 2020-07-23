using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Einstellungen
{
    public partial class EinstellungenMainView : MvxWpfView/*<EinstellungenMainViewModel>*/
    {
        public EinstellungenMainView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new EinstellungenMainViewModel(mainWindowViewModel);
        }
    }
}