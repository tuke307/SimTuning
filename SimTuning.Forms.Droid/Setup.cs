// project=SimTuning.Forms.Droid, file=Setup.cs, creation=2020:10:19 Copyright (c) 2021
// tuke productions. All rights reserved.
using Data;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;
using Serilog;
using Serilog.Extensions.Logging;
using SimTuning.Core;
using SimTuning.Forms.UI;
using SimTuning.Forms.UI.Services;
using System;
using System.IO;

namespace SimTuning.Forms.Droid
{
    public class Setup : MvxFormsAndroidSetup<Forms.UI.MvxApp, FormsApp>
    // No Splash Screen with this; MvxAndroidSetup<App>
    {
        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);

            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.WebBrowser.Platforms.Android.Plugin>();
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Messenger.Plugin>();
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Location.Fused.Plugin>();
        }

        protected override IMvxApplication CreateApp(IMvxIoCProvider iocProvider)
        {
            return base.CreateApp(iocProvider);
        }

        /// <inheritdoc />
        protected override ILoggerFactory CreateLogFactory()
        {
            // TODO: eigentlich nicht die richtige stelle-
            // android: "/data/user/0/com.tuke_productions.SimTuning/files/"
            SimTuning.Core.GeneralSettings.FileDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SimTuning"); // appdata-local-simtuning

            if (string.IsNullOrEmpty(Data.DatabaseSettings.DatabasePath))
            {
                Data.DatabaseSettings.DatabasePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, Data.DatabaseSettings.DatabaseName);
            }

            // appdata-local-simtuning ist anfangs nicht da
            if (!Directory.Exists(SimTuning.Core.GeneralSettings.FileDirectory))
            {
                Directory.CreateDirectory(SimTuning.Core.GeneralSettings.FileDirectory);
            }

            // serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(GeneralSettings.LogFilePath)
                .CreateLogger();

            return new SerilogLoggerFactory();
        }

        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }

        protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
        {
            base.InitializeFirstChance(iocProvider);
            Mvx.IoCProvider.RegisterSingleton<IThemeService>(() => new ThemeService());
        }
    }
}