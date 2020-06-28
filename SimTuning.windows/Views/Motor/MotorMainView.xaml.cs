using SimTuning.windows.ViewModels.Motor;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Motor
{
    /// <summary>
    /// Interaktionslogik für Motor.xaml
    /// </summary>
    public partial class MotorMainView : UserControl
    {
        public MotorMainView()
        {
            InitializeComponent();

            DataContext = new MotorViewModel();
        }
    }
}