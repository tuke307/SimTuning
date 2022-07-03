// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views.Dyno
{
    using MvvmCross.Platforms.Wpf.Views;
    using Mvx.Wpf.ItemsPresenter;
    using SimTuning.WPF.UI.ViewModels.Dyno;

    /// <summary>
    /// DynoMainView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.Dyno.DynoMainViewModel}" />
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