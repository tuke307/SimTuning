// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.App
{
    using MediaManager;
    using Microsoft.Extensions.Logging;
    using MvvmCross.IoC;
    using MvvmCross.Platforms.Wpf.Presenters;
    using MvvmCross.Plugin;
    using MvvmCross.ViewModels;
    using Mvx.Wpf.ItemsPresenter;
    using Serilog;
    using Serilog.Extensions.Logging;
    using SimTuning.Core;
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
    public class MvxWpfSetup : MvvmCross.Platforms.Wpf.Core.MvxWpfSetup<SimTuning.WPF.UI.MvxApp>
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

            base.LoadPlugins(pluginManager);
        }

        /// <inheritdoc />
        protected override ILoggerFactory CreateLogFactory()
        {
            // serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(GeneralSettings.LogFilePath, rollingInterval: RollingInterval.Month)
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
            return new Mvx.Wpf.ItemsPresenter.MvxWpfPresenter(root);
        }

        /// <inheritdoc />
        protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
        {
            // MvxIoCProvider.Instance.RegisterSingleton<Plugin.Settings.Abstractions.ISettings>(Plugin.Settings.CrossSettings.Current);
            CrossMediaManager.Current.Init();
            // MvxIoCProvider.Instance.RegisterSingleton(CrossMediaManager.Current);

            base.InitializeFirstChance(iocProvider);
        }
    }
}