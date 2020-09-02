// project=SimTuning.WPF.UI, file=MenuView.xaml.cs, creation=2020:7:7 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.WPFCore.App.Views
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.WPF.UI.Region;
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