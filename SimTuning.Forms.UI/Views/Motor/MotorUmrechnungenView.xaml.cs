using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Motor;

namespace SimTuning.Forms.UI.Views.Motor
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class MotorUmrechnungenView : MvxContentPage<MotorUmrechnungenViewModel>
    {
        public MotorUmrechnungenView()
        {
            InitializeComponent();

            //BindingContext = new MotorUmrechnungViewModel();
        }
    }
}