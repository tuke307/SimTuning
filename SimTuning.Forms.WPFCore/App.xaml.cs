// project=SimTuning.Forms.WPFCore, file=App.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using System.Windows;
using MediaManager;
using MvvmCross;
using MvvmCross.Core;
using SimTuning.Forms.WPFCore.Region;

namespace SimTuning.Forms.WPFCore
{
    public partial class App
    {
        protected override void RegisterSetup()
        {
            base.RegisterSetup();
            this.RegisterSetupType<MvxWpfSetup<MvxApp>>();
        }
    }
}