﻿// project=SimTuning.WPF.UI, file=EinstellungenMainView.xaml.cs, creation=2020:7:7
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPFCore.App.Views.Einstellungen
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.WPF.UI.Region;
    using SimTuning.WPF.UI.ViewModels.Einstellungen;

    /// <summary>
    /// EinstellungenMainView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.Einstellungen.EinstellungenMainViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class EinstellungenMainView : MvxWpfView<EinstellungenMainViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenMainView" /> class.
        /// </summary>
        public EinstellungenMainView()
        {
            InitializeComponent();
        }
    }
}