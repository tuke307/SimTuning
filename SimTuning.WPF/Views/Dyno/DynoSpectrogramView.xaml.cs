using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Dyno;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Dyno
{
    /// <summary>
    /// Interaktionslogik für Dyno_Spectrogram.xaml
    /// </summary>
    public partial class DynoSpectrogramView : UserControl
    {
        public DynoSpectrogramView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new DynoSpectrogramViewModel(mainWindowViewModel);
        }
    }
}