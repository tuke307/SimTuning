using SimTuning.windows.ViewModels.Motor;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Motor
{
    /// <summary>
    /// Interaktionslogik für Motor_Verdichtung.xaml
    /// </summary>
    public partial class MotorVerdichtungView : UserControl
    {
        public MotorVerdichtungView()
        {
            InitializeComponent();

            DataContext = new Motor_VerdichtungViewModel();
        }
    }
}