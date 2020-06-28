//using SimTuning.mobile.ViewModels.Einlass;
//using System.Windows.Controls;
using SimTuning.mobile.ViewModels.Einlass;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Einlass
{
    /// <summary>
    /// Interaktionslogik für Einlass_Vergaser.xaml
    /// </summary>
    public partial class Einlass_Vergaser : ContentPage
    {
        public Einlass_Vergaser()
        {
            InitializeComponent();

            BindingContext = new Einlass_VergaserViewModel();
        }
    }
}