using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Motor
{
    public partial class MotorMainView : MvxWpfView/*<MotorMainViewModel>*/
    {
        public MotorMainView()
        {
            InitializeComponent();

            //DataContext = new MotorMainViewModel();
        }
    }
}