using SimTuning.windows.ViewModels;
using SimTuning.windows.ViewModels.Dyno;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Dyno
{
    public partial class DynoAudioView : UserControl
    {
        public DynoAudioView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new DynoAudioViewModel(mainWindowViewModel);
        }
    }
}