﻿// project=SimTuning.Forms.WPFCore, file=EinstellungenVehiclesView.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Einstellungen;

namespace SimTuning.Forms.WPFCore.Views.Einstellungen
{
    [MvxWpfPresenter("EinstellungenRegion", mvxViewPosition.NewOrExsist)]
    //[MvxRegionPresentation(RegionName = "VehiclesRegion", WindowIdentifier = nameof(EinstellungenMainView))]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class EinstellungenVehiclesView : MvxWpfView<EinstellungenVehiclesViewModel>
    {
        public EinstellungenVehiclesView()
        {
            InitializeComponent();
        }
    }
}