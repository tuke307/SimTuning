using SimTuning.WPF.ViewModels.Motor;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Motor
{
    public partial class MotorSteuerdiagrammView : UserControl
    {
        public MotorSteuerdiagrammView()
        {
            InitializeComponent();

            DataContext = new MotorSteuerdiagrammViewModel();
        }
    }
}