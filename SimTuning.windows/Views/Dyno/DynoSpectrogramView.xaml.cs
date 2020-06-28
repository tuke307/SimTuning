using SimTuning.windows.ViewModels;
using SimTuning.windows.ViewModels.Dyno;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Dyno
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