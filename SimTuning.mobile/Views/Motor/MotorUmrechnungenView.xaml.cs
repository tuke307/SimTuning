using SimTuning.mobile.ViewModels;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Motor
{
    public partial class MotorUmrechnungenView : ContentPage
    {
        public MotorUmrechnungenView()
        {
            InitializeComponent();

            BindingContext = new MotorUmrechnungViewModel();
        }
    }
}