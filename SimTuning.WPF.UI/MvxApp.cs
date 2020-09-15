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
        public override void Initialize()
        {
            this.CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

#if NET472
            CrossMediaManager.Current.Init();
#endif
            this.RegisterAppStart<SimTuning.WPF.UI.ViewModels.MainViewModel>();

            base.Initialize();
        }

        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Messenger.Plugin>();
            //pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Location.Platforms.Wpf.Plugin>();

            base.LoadPlugins(pluginManager);
        }
    }
}