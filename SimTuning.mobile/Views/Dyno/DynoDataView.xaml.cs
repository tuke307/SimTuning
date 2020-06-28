using SimTuning.mobile.ViewModels.Dyno;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimTuning.mobile.Views.Dyno
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DynoDataView : ContentPage
    {
        public DynoDataView()
        {
            InitializeComponent();

            BindingContext = new DynoDataViewModel();
        }
    }
}