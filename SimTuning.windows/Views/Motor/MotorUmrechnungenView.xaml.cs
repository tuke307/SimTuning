using SimTuning.windows.ViewModels.Motor;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Motor
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