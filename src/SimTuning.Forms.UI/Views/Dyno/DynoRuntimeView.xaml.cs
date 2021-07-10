// project=SimTuning.Forms.UI, file=DynoRuntimeView.xaml.cs, creation=2020:10:19 Copyright
// (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.Views.Dyno
{
    using MvvmCross.Forms.Presenters.Attributes;
    using MvvmCross.Forms.Views;
    using SimTuning.Forms.UI.ViewModels.Dyno;

    /// <summary>
    /// DynoRuntimeView.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Views.MvxContentPage{SimTuning.Forms.UI.ViewModels.Dyno.DynoRuntimeViewModel}" />
    [MvxModalPresentation]
    public partial class DynoRuntimeView : MvxContentPage<DynoRuntimeViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoRuntimeView" /> class.
        /// </summary>
        public DynoRuntimeView()
        {
            InitializeComponent();
        }
    }
}