using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Dyno
{
    public partial class DynoMainView : MvxWpfView/*<DynoMainViewModel>*/
    {
        public DynoMainView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new DynoMainViewModel(mainWindowViewModel);
        }
    }
}