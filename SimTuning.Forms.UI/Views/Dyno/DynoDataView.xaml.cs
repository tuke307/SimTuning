// project=SimTuning.Forms.UI, file=DynoDataView.xaml.cs, creation=2020:6:28 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.Views.Dyno
{
    using MvvmCross.Forms.Presenters.Attributes;
    using MvvmCross.Forms.Views;
    using SimTuning.Forms.UI.ViewModels.Dyno;

    /// <summary>
    /// DynoDataView.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Views.MvxContentPage{SimTuning.Forms.UI.ViewModels.Dyno.DynoDataViewModel}" />
    [MvxModalPresentation]
    public partial class DynoDataView : MvxContentPage<DynoDataViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDataView" /> class.
        /// </summary>
        public DynoDataView()
        {
            this.InitializeComponent();
        }
    }
}