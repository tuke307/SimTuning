using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPF.Views.Motor
{
    public partial class MotorUmrechnungenView : MvxWpfView/*<MotorUmrechnungViewModel>*/
    {
        public MotorUmrechnungenView()
        {
            InitializeComponent();

            //DataContext = new MotorUmrechnungViewModel();
        }
    }
}