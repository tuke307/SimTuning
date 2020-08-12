// project=SimTuning.Forms.WPFCore, file=DynoMainView.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.Views.Dyno
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.Forms.WPFCore.Region;
    using SimTuning.Forms.WPFCore.ViewModels.Dyno;

    /// <summary>
    /// DynoMainView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.Forms.WPFCore.ViewModels.Dyno.DynoMainViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class DynoMainView : MvxWpfView<DynoMainViewModel>
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