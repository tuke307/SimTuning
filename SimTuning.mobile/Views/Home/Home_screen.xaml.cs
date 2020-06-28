using SimTuning.mobile.ViewModels.Home;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Home
{
    /// <summary>
    /// Interaction logic for Slide1_Intro.xaml
    /// </summary>
    public partial class Home_screen : ContentPage
    {
        public Home_screen(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            BindingContext = new HomeViewModel(/*mainWindowViewModel*/);
        }
    }
}