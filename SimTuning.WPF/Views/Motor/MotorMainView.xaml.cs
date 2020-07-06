using SimTuning.WPF.ViewModels.Motor;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Motor
{
    /// <summary>
    /// Interaktionslogik für Motor.xaml
    /// </summary>
    public partial class MotorMainView : UserControl
    {
        public MotorMainView()
        {
            InitializeComponent();

            DataContext = new MotorMainViewModel();
        }
    }
}