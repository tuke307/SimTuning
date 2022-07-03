// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views
{
    using MvvmCross.Platforms.Wpf.Views;
    using Mvx.Wpf.ItemsPresenter;

    /// <summary>
    /// MainView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.MainViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("MainWindowRegion", mvxViewPosition.NewOrExsist)]
    public partial class MainView : MvxWpfView<SimTuning.WPF.UI.ViewModels.MainViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView" /> class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
        }
    }
}