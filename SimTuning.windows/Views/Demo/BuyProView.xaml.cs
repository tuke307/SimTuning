using SimTuning.ViewModels.Demo;
using System.Windows.Controls;

namespace SimTuning.windows.Views.Demo
{
    /// <summary>
    /// Interaction logic for BuyPro.xaml
    /// </summary>
    public partial class BuyProView : UserControl
    {
        public BuyProView()
        {
            InitializeComponent();

            DataContext = new BuyPro_ViewModel();
        }
    }
}