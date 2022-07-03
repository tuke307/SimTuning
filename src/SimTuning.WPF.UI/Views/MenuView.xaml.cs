// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views
{
    using MvvmCross.Platforms.Wpf.Presenters.Attributes;
    using MvvmCross.Platforms.Wpf.Views;
    using Mvx.Wpf.ItemsPresenter;
    using SimTuning.WPF.UI.ViewModels;

    /// <summary>
    /// MenuView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.MenuViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("MainViewRegion", mvxViewPosition.NewOrExsist)]
    public partial class MenuView : MvxWpfView<MenuViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuView" /> class.
        /// </summary>
        public MenuView()
        {
            this.InitializeComponent();
        }
    }
}