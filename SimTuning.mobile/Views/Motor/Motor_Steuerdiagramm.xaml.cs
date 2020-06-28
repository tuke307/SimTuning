//using SimTuning.mobile.ViewModels;
//using System.Windows.Controls;
using SimTuning.mobile.ViewModels;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Motor
{
    public partial class Motor_Steuerdiagramm : ContentPage
    {
        public Motor_Steuerdiagramm()
        {
            InitializeComponent();

            BindingContext = new Motor_SteuerdiagrammViewModel();
        }
    }
}