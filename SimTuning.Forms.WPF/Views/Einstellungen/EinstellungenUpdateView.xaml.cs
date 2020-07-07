using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Einstellungen;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Einstellungen
{
    public partial class EinstellungenUpdateView : MvxWpfView<EinstellungenUpdateViewModel>
    {
        public EinstellungenUpdateView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new EinstellungenUpdateViewModel(mainWindowViewModel);
        }
    }
}