using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.WPFCore.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SimTuning.Forms.Wpf.Views
{
    [MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    [MvxViewFor(typeof(MainViewModel))]
    public partial class MainView : MvxWpfView/*: MvxWindow<RootWindowViewModel>*/
    {
        public MainView()
        {
            InitializeComponent();
        }

        // public void PopToRoot()
        // {
        //    var frame = PageContent;
        //    while (frame.CanGoBack)
        //    {
        //        frame.GoBack();
        //    }
        // }
    }
}