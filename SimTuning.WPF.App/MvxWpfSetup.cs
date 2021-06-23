// project=SimTuning.WPF.App, file=MvxWpfSetup.cs, creation=2020:9:6 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.WPF.App
{
    using MediaManager;
    using Microsoft.Extensions.Logging;
    using MvvmCross.IoC;
    using MvvmCross.Platforms.Wpf.Presenters;
    using MvvmCross.Plugin;
    using MvvmCross.ViewModels;
    using Serilog;
    using Serilog.Extensions.Logging;
    using SimTuning.Core;
    using SimTuning.WPF.UI.Region;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Windows.Controls;

    /// <summary>
    /// WPF dotnetcore app start.
    /// </summary>
    /// <typeparam name="TApplication">The type of the application.</typeparam>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Core.MvxWpfSetup{TApplication}" />
    public class MvxWpfSetup<TApplication> : MvvmCross.Platforms.Wpf.Core.MvxWpfSetup<TApplication>
        where TApplication : class, IMvxApplication, new()
    {
        /// <inheritdoc />
        public override IEnumerable<Assembly> GetViewAssemblies()
        {
            var list = new List<Assembly>();
            list.AddRange(base.GetViewAssemblies());
            list.Add(typeof(SimTuning.WPF.UI.Views.MainWindow).Assembly);
            return list.ToArray();
        }

        /// <inheritdoc />
        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Messenger.Plugin>();

            // TODO: when webbrowser plugin updated; uncomment next
#if NET472
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.WebBrowser.Platforms.Wpf.Plugin>();
#endif
            base.LoadPlugins(pluginManager);
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

        /// <inheritdoc />
        protected override IMvxWpfViewPresenter CreateViewPresenter(ContentControl root)
        {
            return new MvxWpfPresenter(root);
        }

        /// <inheritdoc />
        protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
        {
            //MvxIoCProvider.Instance.RegisterSingleton<Plugin.Settings.Abstractions.ISettings>(Plugin.Settings.CrossSettings.Current);
            CrossMediaManager.Current.Init();
            //MvxIoCProvider.Instance.RegisterSingleton(CrossMediaManager.Current);

            base.InitializeFirstChance(iocProvider);
        }
    }
}