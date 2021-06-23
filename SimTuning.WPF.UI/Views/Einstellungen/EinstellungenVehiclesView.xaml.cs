﻿// project=SimTuning.WPF.UI, file=EinstellungenVehiclesView.xaml.cs, creation=2020:9:7
// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views.Einstellungen
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.WPF.UI.Region;
    using SimTuning.WPF.UI.ViewModels.Einstellungen;

    /// <summary>
    /// EinstellungenVehiclesView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.Einstellungen.EinstellungenVehiclesViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
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