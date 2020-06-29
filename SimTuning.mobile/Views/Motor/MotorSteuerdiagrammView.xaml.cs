using SimTuning.mobile.ViewModels;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Motor
{
    public partial class MotorSteuerdiagrammView : ContentPage
    {
        public MotorSteuerdiagrammView()
        {
            InitializeComponent();

            BindingContext = new MotorSteuerdiagrammViewModel();
        }
    }
}