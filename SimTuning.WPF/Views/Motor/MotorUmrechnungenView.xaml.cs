using SimTuning.WPF.ViewModels.Motor;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Motor
{
    public partial class MotorUmrechnungenView : UserControl
    {
        public MotorUmrechnungenView()
        {
            InitializeComponent();

            DataContext = new MotorUmrechnungViewModel();
        }
    }
}