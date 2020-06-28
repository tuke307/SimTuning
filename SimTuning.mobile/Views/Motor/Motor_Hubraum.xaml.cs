//using SimTuning.mobile.ViewModels.Motor;
//using System.Windows.Controls;
using SimTuning.mobile.ViewModels.Motor;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Motor
{
    /// <summary>
    /// Interaktionslogik für Motor_Hubraum.xaml
    /// </summary>
    public partial class Motor_Hubraum : ContentPage
    {
        public Motor_Hubraum()
        {
            InitializeComponent();

            BindingContext = new Motor_HubraumViewModel();
        }
    }
}