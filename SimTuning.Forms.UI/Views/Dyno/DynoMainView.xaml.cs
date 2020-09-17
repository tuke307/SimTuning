// project=SimTuning.Forms.UI, file=DynoMainView.xaml.cs, creation=2020:6:30 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.Views.Dyno
{
    using MvvmCross.Forms.Presenters.Attributes;
    using MvvmCross.Forms.Views;
    using SimTuning.Forms.UI.ViewModels.Dyno;

    /// <summary>
    /// DynoMainView.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Views.MvxTabbedPage{SimTuning.Forms.UI.ViewModels.Dyno.DynoMainViewModel}" />
    [MvxTabbedPagePresentation(TabbedPosition.Root, WrapInNavigationPage = true, NoHistory = false)]
    //[MvxModalPresentation]
    //[MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class DynoMainView : MvxTabbedPage<DynoMainViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoMainView" /> class.
        /// </summary>
        public DynoMainView()
        {
            InitializeComponent();
        }
    }
}