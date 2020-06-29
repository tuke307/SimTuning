using SimTuning.windows.ViewModels.Motor;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Motor
{
    /// <summary>
    /// Interaktionslogik für Motor_Hubraum.xaml
    /// </summary>
    public partial class MotorHubraumView : UserControl
    {
        public MotorHubraumView()
        {
            InitializeComponent();

            DataContext = new MotorHubraumViewModel();
        }
    }
}