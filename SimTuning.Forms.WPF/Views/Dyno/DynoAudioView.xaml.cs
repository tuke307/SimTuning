using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Dyno
{
    public partial class DynoAudioView : MvxWpfView/*<DynoAudioViewModel>*/
    {
        public DynoAudioView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new DynoAudioViewModel(mainWindowViewModel);
        }
    }
}