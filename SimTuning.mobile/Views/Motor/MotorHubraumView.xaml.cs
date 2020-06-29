using SimTuning.mobile.ViewModels.Motor;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Motor
{
    public partial class MotorHubraumView : ContentPage
    {
        public MotorHubraumView()
        {
            InitializeComponent();

            BindingContext = new MotorHubraumViewModel();
        }
    }
}