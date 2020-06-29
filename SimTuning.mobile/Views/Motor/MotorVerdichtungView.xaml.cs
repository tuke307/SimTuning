using SimTuning.mobile.ViewModels.Motor;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Motor
{
    public partial class MotorVerdichtungView : ContentPage
    {
        public MotorVerdichtungView()
        {
            InitializeComponent();

            BindingContext = new MotorVerdichtungViewModel();
        }
    }
}