using SimTuning.WPFCore.ViewModels.Demo;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.Forms.WPFCore.Views.Demo
{
    public partial class BuyProView : MvxWpfView<DemoMainViewModel>
    {
        public BuyProView()
        {
            InitializeComponent();

            //DataContext = new DemoMainViewModel();
        }
    }
}