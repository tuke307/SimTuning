//using SimTuning.mobile.ViewModels;
//using System.Windows.Controls;
using SimTuning.mobile.ViewModels;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Motor
{
    public partial class Motor_Umrechnungen : ContentPage
    {
        public Motor_Umrechnungen()
        {
            InitializeComponent();

            BindingContext = new Motor_UmrechnungViewModel();
        }
    }
}