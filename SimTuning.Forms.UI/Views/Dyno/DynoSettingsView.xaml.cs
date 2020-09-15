using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Dyno;
using Xamarin.Forms.Xaml;

namespace SimTuning.Forms.UI.Views.Dyno
{
    [MvxModalPresentation]
    public partial class DynoSettingsView : MvxContentPage<DynoSettingsViewModel>
    {
        public DynoSettingsView()
        {
            InitializeComponent();
        }
    }
}