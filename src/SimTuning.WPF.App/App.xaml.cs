﻿// project=SimTuning.WPF.App, file=App.xaml.cs, creation=2020:9:2 Copyright (c) 2021 tuke
// productions. All rights reserved.
namespace SimTuning.WPF.App
{
    using MvvmCross.Core;
    using MvvmCross.Platforms.Wpf.Views;

    /// <summary>
    /// Normal App Start.
    /// </summary>
    public partial class App : MvxApplication
    {
        /// <summary>
        /// Registers the setup.
        /// </summary>
        protected override void RegisterSetup()
        {
            base.RegisterSetup();
            this.RegisterSetupType<MvxWpfSetup<SimTuning.WPF.UI.MvxApp>>();
        }
    }
}