using MvvmCross.Commands;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels;

namespace SimTuning.Forms.WPFCore.Views
{
    [MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class MenuView : MvxWpfView<MenuViewModel>
    {
        public MenuView()
        {
            InitializeComponent();
        }
    }
}