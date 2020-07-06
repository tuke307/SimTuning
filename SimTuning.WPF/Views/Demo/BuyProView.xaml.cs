using SimTuning.WPF.ViewModels.Demo;
using System.Windows.Controls;

namespace SimTuning.WPF.Views.Demo
{
    public partial class BuyProView : UserControl
    {
        public BuyProView()
        {
            InitializeComponent();

            DataContext = new DemoMainViewModel();
        }
    }
}