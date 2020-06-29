//using SimTuning.mobile.ViewModels.Einlass;
//using System.Windows.Controls;
using SimTuning.mobile.ViewModels.Einlass;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Einlass
{
    /// <summary>
    /// Interaktionslogik für Einlass_Kanal.xaml
    /// </summary>
    public partial class EinlassKanalView : ContentPage
    {
        public EinlassKanalView()
        {
            InitializeComponent();

            BindingContext = new EinlassKanalViewModel();
        }
    }
}