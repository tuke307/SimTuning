using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Einstellungen;
using System.Windows.Controls;

namespace SimTuning.Forms.WPF.Views.Einstellungen
{
    public partial class EinstellungenMainView : MvxWpfView<EinstellungenMainViewModel>
    {
        public EinstellungenMainView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new EinstellungenMainViewModel(mainWindowViewModel);
        }
    }
}