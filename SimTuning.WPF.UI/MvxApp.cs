// project=SimTuning.WPF.UI, file=MvxApp.cs, creation=2020:7:30 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.WPF.UI
{
    using MediaManager;
    using MvvmCross.IoC;
    using MvvmCross.Plugin;
    using MvvmCross.ViewModels;

    /// <summary>
    /// WPF-Specific App Start.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxApplication" />
    public class MvxApp : MvxApplication
    {
        /// <summary>
        /// Any initialization steps that can be done in the background
        /// </summary>
        public override void Initialize()
        {
            MvxIoCProvider.Instance.RegisterSingleton(Plugin.Settings.CrossSettings.Current);

            MvxIoCProvider.Instance.RegisterSingleton(CrossMediaManager.Current);
            CrossMediaManager.Current.Init();
            this.RegisterAppStart<SimTuning.WPF.UI.ViewModels.MainViewModel>();

            base.Initialize();
        }

        /// <summary>
        /// Loads the plugins.
        /// </summary>
        /// <param name="pluginManager">The plugin manager.</param>
        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Messenger.Plugin>();
            //pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Location.Platforms.Wpf.Plugin>();

            base.LoadPlugins(pluginManager);
        }
    }
}