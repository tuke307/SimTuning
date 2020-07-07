using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels.Home;

namespace SimTuning.Forms.WPFCore.Views.Home
{
    public partial class Home_screen : MvxWpfView<HomeMainViewModel>
    {
        public Home_screen(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //DataContext = new HomeMainViewModel(mainWindowViewModel);
        }
    }
}