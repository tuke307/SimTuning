// project=SimTuning.Forms.UI, file=App.cs, creation=2020:6:30 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.Forms.UI
{
    using MediaManager;
    using MvvmCross;
    using MvvmCross.IoC;
    using MvvmCross.Plugin;
    using SimTuning.Forms.UI.ViewModels;

    /// <summary>
    /// specific application.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxApplication" />
    public class App : MvvmCross.ViewModels.MvxApplication
    {
        /// <summary>
        /// Any initialization steps that can be done in the background.
        /// </summary>
        public override void Initialize()
        {
            this.CreatableTypes()
               .EndingWith("Service")
               .AsInterfaces()
               .RegisterAsLazySingleton();

            MvxIoCProvider.Instance.RegisterSingleton<Plugin.Settings.Abstractions.ISettings>(Plugin.Settings.CrossSettings.Current);

            CrossMediaManager.Current.Init();

            this.RegisterAppStart<MainPageViewModel>();

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