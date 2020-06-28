using SimTuning.mobile.ViewModels.Tuning;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimTuning.mobile.Views.Tuning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TuningDataView : ContentPage
    {
        public TuningDataView()
        {
            InitializeComponent();

            BindingContext = new TuningDataViewModel();
        }
    }
}