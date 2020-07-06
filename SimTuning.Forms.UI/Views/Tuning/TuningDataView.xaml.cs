using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Tuning;

namespace SimTuning.Forms.UI.Views.Tuning
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class TuningDataView : MvxContentPage<TuningDataViewModel>
    {
        public TuningDataView()
        {
            InitializeComponent();

            //BindingContext = new TuningDataViewModel();
        }
    }
}