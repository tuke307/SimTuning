using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Dyno;

namespace SimTuning.Forms.UI.Views.Dyno
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class DynoAudioView : MvxContentPage<DynoAudioViewModel>
    {
        public DynoAudioView()
        {
            InitializeComponent();
        }
    }
}