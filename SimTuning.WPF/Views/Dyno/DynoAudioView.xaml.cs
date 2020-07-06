using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Dyno;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Dyno
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