using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Motor
{
    public partial class MotorHubraumView : MvxWpfView/*<MotorHubraumViewModel>*/
    {
        public MotorHubraumView()
        {
            InitializeComponent();

            //DataContext = new MotorHubraumViewModel();
        }
    }
}