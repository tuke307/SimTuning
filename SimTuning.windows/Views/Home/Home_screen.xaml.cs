using SimTuning.windows.ViewModels;
using SimTuning.windows.ViewModels.Home;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Home
{
    /// <summary>
    /// Interaction logic for Slide1_Intro.xaml
    /// </summary>
    public partial class Home_screen : UserControl
    {
        public Home_screen(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            DataContext = new HomeMainViewModel(mainWindowViewModel);
        }
    }
}