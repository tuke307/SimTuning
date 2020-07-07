using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Einstellungen;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPFCore.Views.Einstellungen
{
    public partial class EinstellungenKontoView : MvxWpfView<EinstellungenKontoViewModel>
    {
        public EinstellungenKontoView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new EinstellungenKontoViewModel(mainWindowViewModel);
        }
    }
}