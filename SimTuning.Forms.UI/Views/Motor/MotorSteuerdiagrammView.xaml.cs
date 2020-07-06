using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Motor;

namespace SimTuning.Forms.UI.Views.Motor
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class MotorSteuerdiagrammView : MvxContentPage<MotorSteuerdiagrammViewModel>
    {
        public MotorSteuerdiagrammView()
        {
            InitializeComponent();

            //BindingContext = new MotorSteuerdiagrammViewModel();
        }
    }
}