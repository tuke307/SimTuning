// project=SimTuning.Forms.UI, file=DynoGeschwindigkeitView.xaml.cs, creation=2020:12:14
// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Dyno;

namespace SimTuning.Forms.UI.Views.Dyno
{
    /// <summary>
    /// DynoGeschwindigkeitView.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Views.MvxContentPage{SimTuning.Forms.UI.ViewModels.Dyno.DynoGeschwindigkeitViewModel}" />
    [MvxModalPresentation]
    public partial class DynoGeschwindigkeitView : MvxContentPage<DynoGeschwindigkeitViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoGeschwindigkeitView" />
        /// class.
        /// </summary>
        public DynoGeschwindigkeitView()
        {
            InitializeComponent();
        }
    }
}