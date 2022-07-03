// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views.Einstellungen
{
    using MvvmCross.Platforms.Wpf.Views;
    using Mvx.Wpf.ItemsPresenter;
    using SimTuning.WPF.UI.ViewModels.Einstellungen;

    /// <summary>
    /// EinstellungenUpdateView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.Einstellungen.EinstellungenUpdateViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class EinstellungenUpdateView : MvxWpfView<EinstellungenUpdateViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenUpdateView" />
        /// class.
        /// </summary>
        public EinstellungenUpdateView()
        {
            InitializeComponent();
        }
    }
}