using SimTuning.mobile.ViewModels.Einlass;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Einlass
{
    public partial class EinlassVergaserView : ContentPage
    {
        public EinlassVergaserView()
        {
            InitializeComponent();

            BindingContext = new EinlassVergaserViewModel();
        }
    }
}