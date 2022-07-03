// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views.Einstellungen
{
    using MvvmCross.Platforms.Wpf.Views;
    using Mvx.Wpf.ItemsPresenter;
    using SimTuning.WPF.UI.ViewModels.Einstellungen;

    /// <summary>
    /// EinstellungenKontoView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.Einstellungen.EinstellungenKontoViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class EinstellungenKontoView : MvxWpfView<EinstellungenKontoViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenKontoView" /> class.
        /// </summary>
        public EinstellungenKontoView()
        {
            this.InitializeComponent();
        }
    }
}