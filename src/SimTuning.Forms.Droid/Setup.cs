// Copyright (c) 2021 tuke productions. All rights reserved.
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

namespace SimTuning.Forms.Droid
{
    public class Setup : MvxFormsAndroidSetup<Forms.UI.MvxApp, FormsApp>
    // No Splash Screen with this; MvxAndroidSetup<App>
    {
        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);

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
            // serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.AndroidLog()
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