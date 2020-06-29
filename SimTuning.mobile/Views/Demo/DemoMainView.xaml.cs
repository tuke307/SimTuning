//using System.Windows.Controls;
using SimTuning.mobile.ViewModels.Demo;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Demo
{
    public partial class DemoMainView : ContentPage
    {
        public DemoMainView()
        {
            InitializeComponent();

            BindingContext = new DemoMainViewModel();
        }
    }
}