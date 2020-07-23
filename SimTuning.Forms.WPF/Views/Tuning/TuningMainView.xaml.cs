using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Tuning
{
    public partial class TuningMainView : MvxWpfView/*<TuningMainViewModel>*/
    {
        public TuningMainView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new TuningMainViewModel(mainWindowViewModel);
        }
    }
}