// project=SimTuning.WPF.UI, file=MvxApp.cs, creation=2020:7:30 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.WPF.UI
{
    using MediaManager;
    using MvvmCross;
    using MvvmCross.IoC;
    using MvvmCross.Plugin;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;
    using System.IO;

    /// <summary>
    /// WPF-Specific App Start.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxApplication" />
    public class MvxApp : MvxApplication
    {
        public override void Initialize()
        {
            SimTuning.Core.Constants.Platform = Plugin.DeviceInfo.Abstractions.Platform.Windows;

            Data.Constants.DatabasePath = Path.Combine(SimTuning.Core.Constants.FileDirectory, Data.Constants.DatabaseName);

            if (!Directory.Exists(SimTuning.Core.Constants.FileDirectory))
                Directory.CreateDirectory(SimTuning.Core.Constants.FileDirectory);

            //Mvx.RegisterType<IMvxMessenger, MvxReloaderMessage>();
            //Mvx.IoCProvider.RegisterSingleton<MvvmCross.Plugin.Messenger.IMvxMessenger>(new Core.Models.MvxReloaderMessage());
            //Mvx.IoCProvider.Resolve<MvvmCross.Plugin.Messenger.IMvxMessenger>();

            this.CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

#if NET472
            Mvx.IoCProvider.RegisterSingleton(CrossMediaManager.Current);
#endif
            this.RegisterAppStart<SimTuning.WPF.UI.ViewModels.MainViewModel>();

            base.Initialize();
        }

        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Messenger.Plugin>();

            base.LoadPlugins(pluginManager);
        }
    }
}