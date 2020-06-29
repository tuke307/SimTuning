//using SimTuning.ViewModels.Auslass;
//using System.Windows.Controls;
using SimTuning.mobile.ViewModels.Auslass;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Auslass
{
    /// <summary>
    /// Interaction logic for Auslass_Theorie.xaml
    /// </summary>
    public partial class AuslassTheorieView : ContentPage
    {
        public AuslassTheorieView()
        {
            InitializeComponent();

            BindingContext = new AuslassTheorieViewModel();
        }
    }
}