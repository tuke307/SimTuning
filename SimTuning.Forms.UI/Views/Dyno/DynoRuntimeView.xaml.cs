using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Dyno;
using Xamarin.Forms.Xaml;

namespace SimTuning.Forms.UI.Views.Dyno
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DynoRuntimeView : MvxContentPage<DynoRuntimeViewModel>
    {
        public DynoRuntimeView()
        {
            InitializeComponent();
        }
    }
}