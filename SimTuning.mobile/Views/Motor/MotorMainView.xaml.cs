using SimTuning.mobile.ViewModels.Motor;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Motor
{
    public partial class MotorMainView : TabbedPage
    {
        public MotorMainView()
        {
            InitializeComponent();

            BindingContext = new MotorMainViewModel();
        }
    }
}