// project=SimTuning.Forms.WPFCore, file=EinstellungenKontoView.xaml.cs, creation=2020:7:7
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.Views.Einstellungen
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.Forms.WPFCore.Region;
    using SimTuning.Forms.WPFCore.ViewModels.Einstellungen;

    /// <summary>
    /// EinstellungenKontoView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.Forms.WPFCore.ViewModels.Einstellungen.EinstellungenKontoViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("EinstellungenRegion", mvxViewPosition.NewOrExsist)]
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