// project=SimTuning.Forms.WPFCore, file=EinstellungenVehiclesView.xaml.cs, creation=2020:7:7
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.Views.Einstellungen
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.Forms.WPFCore.Region;
    using SimTuning.Forms.WPFCore.ViewModels.Einstellungen;

    /// <summary>
    /// EinstellungenVehiclesView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.Forms.WPFCore.ViewModels.Einstellungen.EinstellungenVehiclesViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("EinstellungenRegion", mvxViewPosition.NewOrExsist)]
    public partial class EinstellungenVehiclesView : MvxWpfView<EinstellungenVehiclesViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenVehiclesView" />
        /// class.
        /// </summary>
        public EinstellungenVehiclesView()
        {
            this.InitializeComponent();
        }
    }
}