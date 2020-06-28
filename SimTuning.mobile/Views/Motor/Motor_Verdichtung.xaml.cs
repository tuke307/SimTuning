//using SimTuning.mobile.ViewModels.Motor;
//using System.Windows.Controls;
using SimTuning.mobile.ViewModels.Motor;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Motor
{
    /// <summary>
    /// Interaktionslogik für Motor_Verdichtung.xaml
    /// </summary>
    public partial class Motor_Verdichtung : ContentPage
    {
        public Motor_Verdichtung()
        {
            InitializeComponent();

            BindingContext = new Motor_VerdichtungViewModel();
        }
    }
}