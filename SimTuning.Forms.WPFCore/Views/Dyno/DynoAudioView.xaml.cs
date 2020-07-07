using SimTuning.WPFCore.ViewModels;
using SimTuning.WPFCore.ViewModels.Dyno;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPFCore.Views.Dyno
{
    public partial class DynoAudioView : MvxWpfView<DynoAudioViewModel>
    {
        public DynoAudioView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new DynoAudioViewModel(mainWindowViewModel);
        }
    }
}