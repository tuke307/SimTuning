using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Tuning
{
    public partial class TuningDataView : MvxWpfView/*<TuningDataViewModel>*/
    {
        public TuningDataView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new TuningDataViewModel(mainWindowViewModel);
        }
    }
}