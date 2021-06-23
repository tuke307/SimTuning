// project=SimTuning.Forms.UI, file=DynoSpectrogramView.xaml.cs, creation=2020:6:28
// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Dyno;

namespace SimTuning.Forms.UI.Views.Dyno
{
    /// <summary>
    /// DynoSpectrogramView.
    /// </summary>
    //[MvxTabbedPagePresentation(TabbedPosition.Tab)]
    [MvxModalPresentation]
    public partial class DynoSpectrogramView : MvxContentPage<DynoSpectrogramViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoSpectrogramView" /> class.
        /// </summary>
        public DynoSpectrogramView()
        {
            InitializeComponent();
        }
    }
}