// Copyright (c) 2021 tuke productions. All rights reserved.
using MediaManager;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using SimTuning.Core.Services;
using SimTuning.Data;
using System.Threading.Tasks;

namespace SimTuning.Core
{
    /// <summary>
    /// BASE Application.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxApplication" />
    public class MvxApp : MvxApplication
    {
        /// <summary>
        /// Any initialization steps that can be done in the background.
        /// </summary>
        public override void Initialize()
        {
            typeof(MvxApp).Assembly.CreatableTypes()
               .EndingWith("Service")
               .AsInterfaces()
               .RegisterAsLazySingleton();

            // static registration => same instance
            Mvx.IoCProvider.RegisterSingleton<DatabaseContext>(() => new DatabaseContext());

            var context = Mvx.IoCProvider.Resolve<DatabaseContext>();
            Mvx.IoCProvider.RegisterSingleton<IVehicleService>(() => new VehicleService(context));

            Mvx.IoCProvider.RegisterSingleton(Plugin.Settings.CrossSettings.Current);

            Mvx.IoCProvider.RegisterSingleton(CrossMediaManager.Current);

            base.Initialize();
        }

        /// <summary>
        /// If the application is restarted (eg primary activity on Android can be
        /// restarted) this method will be called before Startup is called again.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
        }

        /// <summary>
        /// Do any UI bound startup actions here.
        /// </summary>
        public override Task Startup()
        {
            return base.Startup();
        }
    }
}