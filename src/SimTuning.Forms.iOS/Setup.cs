// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Serilog;
using Serilog.Extensions.Logging;
using SimTuning.Forms.UI;
using System;
using Xamarin.Forms;

namespace SimTuning.mobile.iOS
{
    /// <summary>
    /// Setup.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Platforms.Ios.Core.MvxFormsIosSetup{SimTuning.Forms.UI.MvxApp, SimTuning.Forms.UI.FormsApp}" />
    public class Setup : MvxFormsIosSetup<Forms.UI.MvxApp, FormsApp>
    {
        protected override ILoggerFactory CreateLogFactory()
        {
            // serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.NSLog()
                // .MinimumLevel.Warning() .WriteTo.File(GeneralSettings.LogFilePath)
                .CreateLogger();

            return new SerilogLoggerFactory();
        }

        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }
    }
}