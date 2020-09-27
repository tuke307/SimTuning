// project=SimTuning.Forms.UI, file=App.cs, creation=2020:6:30 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.Forms.UI
{
    using MediaManager;
    using MvvmCross.IoC;
    using SimTuning.Forms.UI.ViewModels;
    using System.Threading.Tasks;

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
            typeof(SimTuning.Core.Models.LocationService).Assembly.CreatableTypes()
               .EndingWith("Service")
               .AsInterfaces()
               .RegisterAsLazySingleton();

            MvxIoCProvider.Instance.RegisterSingleton(Plugin.Settings.CrossSettings.Current);

            MvxIoCProvider.Instance.RegisterSingleton(CrossMediaManager.Current);

            //CrossMediaManager.Current.Init();

            this.RegisterAppStart<MainPageViewModel>();

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