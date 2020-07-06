using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Dyno;

namespace SimTuning.Forms.UI.Views.Dyno
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class DynoSpectrogramView : MvxContentPage<DynoSpectrogramViewModel>
    {
        public DynoSpectrogramView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();

            //BindingContext = new DynoSpectrogramViewModel(/*mainWindowViewModel*/);
        }
    }
}