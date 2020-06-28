//using SimTuning.mobile.ViewModels.Auslass;
//using System.Windows.Controls;
using SimTuning.mobile.ViewModels.Auslass;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Auslass
{
    /// <summary>
    /// Interaction logic for Auslass_Anwendung.xaml
    /// </summary>
    public partial class Auslass_Anwendung : ContentPage
    {
        public Auslass_Anwendung()
        {
            InitializeComponent();

            BindingContext = new Auslass_AnwendungViewModel();
        }
    }
}