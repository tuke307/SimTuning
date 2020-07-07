using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels;

namespace SimTuning.Forms.WPFCore.Views
{
    public partial class MenuView : MvxWpfView<MenuViewModel>
    {
        public MenuView()
        {
            InitializeComponent();
        }
    }
}