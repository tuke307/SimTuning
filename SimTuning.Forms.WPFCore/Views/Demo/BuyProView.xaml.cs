using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.ViewModels.Demo;

namespace SimTuning.Forms.WPFCore.Views.Demo
{
    [MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class BuyProView : MvxWpfView<DemoMainViewModel>
    {
        public BuyProView()
        {
            InitializeComponent();
        }
    }
}