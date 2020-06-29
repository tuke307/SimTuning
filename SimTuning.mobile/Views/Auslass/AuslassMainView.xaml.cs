using SimTuning.mobile.ViewModels.Auslass;
using Xamarin.Forms;

namespace SimTuning.mobile.Views.Auslass
{
    /// <summary>
    /// Interaktionslogik für Auslass.xaml
    /// </summary>
    public partial class AuslassMainView : TabbedPage
    {
        public AuslassMainView()
        {
            InitializeComponent();

            BindingContext = new AuslassMainViewModel();
        }
    }
}