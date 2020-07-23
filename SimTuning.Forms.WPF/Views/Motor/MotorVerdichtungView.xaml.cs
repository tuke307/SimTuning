using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Motor
{
    public partial class MotorVerdichtungView : MvxWpfView/*<MotorVerdichtungViewModel>*/
    {
        public MotorVerdichtungView()
        {
            InitializeComponent();

            //DataContext = new MotorVerdichtungViewModel();
        }
    }
}