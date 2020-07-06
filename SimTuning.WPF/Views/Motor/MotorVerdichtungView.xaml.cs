using SimTuning.WPF.ViewModels.Motor;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Motor
{
    /// <summary>
    /// Interaktionslogik für Motor_Verdichtung.xaml
    /// </summary>
    public partial class MotorVerdichtungView : UserControl
    {
        public MotorVerdichtungView()
        {
            InitializeComponent();

            DataContext = new MotorVerdichtungViewModel();
        }
    }
}