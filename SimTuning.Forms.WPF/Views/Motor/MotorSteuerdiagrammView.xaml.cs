using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Motor
{
    public partial class MotorSteuerdiagrammView : MvxWpfView/*<MotorSteuerdiagrammViewModel>*/
    {
        public MotorSteuerdiagrammView()
        {
            InitializeComponent();

            //DataContext = new MotorSteuerdiagrammViewModel();
        }
    }
}