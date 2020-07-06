using SimTuning.WPF.ViewModels;
using SimTuning.WPF.ViewModels.Home;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Home
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