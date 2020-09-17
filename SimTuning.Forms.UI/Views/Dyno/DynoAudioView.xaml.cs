// project=SimTuning.Forms.UI, file=DynoAudioView.xaml.cs, creation=2020:6:28 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.Views.Dyno
{
    using MvvmCross.Forms.Presenters.Attributes;
    using MvvmCross.Forms.Views;
    using SimTuning.Forms.UI.ViewModels.Dyno;

    /// <summary>
    /// DynoAudioView.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Views.MvxContentPage{SimTuning.Forms.UI.ViewModels.Dyno.DynoAudioViewModel}" />
    //[MvxTabbedPagePresentation(TabbedPosition.Tab)]
    [MvxModalPresentation]
    public partial class DynoAudioView : MvxContentPage<DynoAudioViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoAudioView" /> class.
        /// </summary>
        public DynoAudioView()
        {
            InitializeComponent();
        }
    }
}