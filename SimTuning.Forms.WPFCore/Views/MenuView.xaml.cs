// project=SimTuning.Forms.WPFCore, file=MenuView.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.Views
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.Forms.WPFCore.Region;
    using SimTuning.Forms.WPFCore.ViewModels;

    /// <summary>
    /// MenuView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.Forms.WPFCore.ViewModels.MenuViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("MainViewRegion", mvxViewPosition.NewOrExsist)]
    public partial class MenuView : MvxWpfView<MenuViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuView"/> class.
        /// </summary>
        public MenuView()
        {
            this.InitializeComponent();
        }
    }
}