using SimTuning.mobile.ViewModels.Einlass;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Einlass
{
    public partial class EinlassMainView : TabbedPage
    {
        public EinlassMainView()
        {
            InitializeComponent();

            BindingContext = new EinlassMainViewModel();
        }
    }
}