using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Dyno
{
    public partial class DynoSpectrogramView : MvxWpfView/*<DynoSpectrogramViewModel>*/
    {
        public DynoSpectrogramView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new DynoSpectrogramViewModel(mainWindowViewModel);
        }
    }
}