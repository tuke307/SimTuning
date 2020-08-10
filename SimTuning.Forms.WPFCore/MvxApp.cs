// project=SimTuning.Forms.WPFCore, file=MvxApp.cs, creation=2020:8:3
// Copyright (c) 2020 tuke productions. All rights reserved.
using System.IO;
using MediaManager;
using MvvmCross;
using MvvmCross.ViewModels;
using OxyPlot.Wpf;

namespace SimTuning.Forms.WPFCore
{
    public class MvxApp : MvxApplication
    {
        public override void Initialize()
        {
            SimTuning.Core.Constants.Platform = Plugin.DeviceInfo.Abstractions.Platform.Windows;

            Data.Constants.DatabasePath = Path.Combine(SimTuning.Core.Constants.FileDirectory, Data.Constants.DatabaseName);

            if (!Directory.Exists(SimTuning.Core.Constants.FileDirectory))
                Directory.CreateDirectory(SimTuning.Core.Constants.FileDirectory);

            RegisterAppStart<SimTuning.Forms.WPFCore.ViewModels.MainViewModel>();

            base.Initialize();

            //CrossMediaManager.Current.Init(this);
            //CrossMediaManager.Current.Init();
            //Mvx.IoCProvider.RegisterSingleton(CrossMediaManager.Current);
        }
    }
}