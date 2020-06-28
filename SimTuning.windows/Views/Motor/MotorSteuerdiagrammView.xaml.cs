using SimTuning.windows.ViewModels.Motor;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Motor
{
    public partial class MotorSteuerdiagrammView : UserControl
    {
        public MotorSteuerdiagrammView()
        {
            InitializeComponent();

            DataContext = new Motor_SteuerdiagrammViewModel();
        }
    }
}