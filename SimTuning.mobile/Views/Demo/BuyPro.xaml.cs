//using System.Windows.Controls;
using SimTuning.mobile.ViewModels.Demo;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Demo
{
    /// <summary>
    /// Interaction logic for BuyPro.xaml
    /// </summary>
    public partial class BuyPro : ContentPage
    {
        public BuyPro()
        {
            InitializeComponent();

            BindingContext = new BuyPro_ViewModel();
        }
    }
}