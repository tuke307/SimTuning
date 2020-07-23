using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Dyno
{
    public partial class DynoDataView : MvxWpfView/*<DynoDataViewModel>*/
    {
        public DynoDataView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new DynoDataViewModel(mainWindowViewModel);
        }
    }
}