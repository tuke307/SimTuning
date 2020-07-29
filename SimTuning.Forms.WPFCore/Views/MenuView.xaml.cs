using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SimTuning.Forms.WPFCore.Views
{
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = true)]
    //[MvxWindowPresentation(Identifier = nameof(MainView), Modal = false)]
    //[MvxViewFor(typeof(MainViewModel))]
    [MvxWpfPresenter("MainContent", mvxViewPosition.NewOrExsist)]
    public partial class MenuView : MvxWpfView<MenuViewModel>/*: MvxWindow<RootWindowViewModel>*/
    {
        public MenuView()
        {
            this.InitializeComponent();
        }
    }
}