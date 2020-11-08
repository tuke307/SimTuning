﻿// project=SimTuning.WPF.App, file=MvxWpfSetup.cs, creation=2020:9:6 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.WPF.App
{
    using MediaManager;
    using MvvmCross.IoC;
    using MvvmCross.Platforms.Wpf.Presenters;
    using MvvmCross.Plugin;
    using MvvmCross.ViewModels;
    using SimTuning.WPF.UI.Region;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Controls;

    /// <summary>
    /// WPF dotnetframework app start.
    /// </summary>
    /// <typeparam name="TApplication">The type of the application.</typeparam>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Core.MvxWpfSetup{TApplication}" />
    public class MvxWpfSetup<TApplication> : MvvmCross.Platforms.Wpf.Core.MvxWpfSetup<TApplication>
        where TApplication : class, IMvxApplication, new()
    {
        /// <summary>
        /// Gets the view assemblies.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Assembly> GetViewAssemblies()
        {
            var list = new List<Assembly>();
            list.AddRange(base.GetViewAssemblies());
            list.Add(typeof(SimTuning.WPF.UI.Views.MainWindow).Assembly);
            return list.ToArray();
        }

        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Messenger.Plugin>();
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.WebBrowser.Platforms.Wpf.Plugin>();

            base.LoadPlugins(pluginManager);
        }

        /// <summary>
        /// Creates the view presenter.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns></returns>
        protected override IMvxWpfViewPresenter CreateViewPresenter(ContentControl root)
        {
            return new MvxWpfPresenter(root);
        }

        /// <summary>
        /// Initializes the first chance.
        /// </summary>
        protected override void InitializeFirstChance()
        {
            //MvxIoCProvider.Instance.RegisterSingleton<Plugin.Settings.Abstractions.ISettings>(Plugin.Settings.CrossSettings.Current);
            CrossMediaManager.Current.Init();
            //MvxIoCProvider.Instance.RegisterSingleton(CrossMediaManager.Current);

            base.InitializeFirstChance();
        }
    }
}