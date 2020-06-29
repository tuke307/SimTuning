//using SimTuning.mobile.ViewModels.Auslass;
//using System.Windows.Controls;
using SimTuning.mobile.ViewModels.Auslass;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Auslass
{
    /// <summary>
    /// Interaction logic for Auslass_Anwendung.xaml
    /// </summary>
    public partial class AuslassAnwendungView : ContentPage
    {
        public AuslassAnwendungView()
        {
            InitializeComponent();

            BindingContext = new AuslassAnwendungViewModel();
        }
    }
}