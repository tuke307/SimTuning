using SimTuning.mobile.ViewModels.Dyno;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Dyno
{
    public partial class DynoMainView : TabbedPage
    {
        public DynoMainView()
        {
            InitializeComponent();

            BindingContext = new DynoMainViewModel();
        }
    }
}