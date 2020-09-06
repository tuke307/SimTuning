﻿// project=SimTuning.WPF.App, file=App.xaml.cs, creation=2020:9:2 Copyright (c) 2020 tuke
// productions. All rights reserved.
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;

namespace SimTuning.WPF.App
{
    /// <summary>
    /// Normal App Start.
    /// </summary>
    public partial class App : MvxApplication
    {
        protected override void RegisterSetup()
        {
            base.RegisterSetup();
            this.RegisterSetupType<MvxWpfSetup<SimTuning.WPF.UI.MvxApp>>();
        }
    }
}