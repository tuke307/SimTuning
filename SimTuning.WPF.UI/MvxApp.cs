// project=SimTuning.WPF.UI, file=MvxApp.cs, creation=2020:7:30 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.WPF.UI
{
    using Data;
    using MediaManager;
    using Microsoft.EntityFrameworkCore;
    using MvvmCross;
    using MvvmCross.IoC;
    using MvvmCross.Plugin;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-Specific App Start.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxApplication" />
    public class MvxApp : MvxApplication
    {
        public override void Initialize()
        {
            SimTuning.Core.Constants.FileDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SimTuning"); // appdata-local-simtunig
            Data.Constants.DatabasePath = Path.Combine(SimTuning.Core.Constants.FileDirectory, Data.Constants.DatabaseName);

            if (!Directory.Exists(SimTuning.Core.Constants.FileDirectory))
            {
                Directory.CreateDirectory(SimTuning.Core.Constants.FileDirectory);
            }

            using (var db = new DatabaseContext())
            {
                db.Database.Migrate();
            }

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

            base.LoadPlugins(pluginManager);
        }
    }
}