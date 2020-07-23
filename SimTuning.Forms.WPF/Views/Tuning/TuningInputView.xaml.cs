using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Tuning
{
    public partial class TuningInputView : MvxWpfView/*<TuningInputViewModel>*/
    {
        public TuningInputView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new TuningInputViewModel(mainWindowViewModel);
        }
    }
}