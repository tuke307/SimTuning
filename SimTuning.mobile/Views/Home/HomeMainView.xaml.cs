using SimTuning.mobile.ViewModels.Home;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Home
{
    public partial class HomeMainView : ContentPage
    {
        public HomeMainView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            BindingContext = new HomeMainViewModel(/*mainWindowViewModel*/);
        }
    }
}