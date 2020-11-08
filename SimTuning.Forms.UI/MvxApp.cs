// project=SimTuning.Forms.UI, file=App.cs, creation=2020:6:30 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.Forms.UI
{
    using MvvmCross.IoC;
    using SimTuning.Forms.UI.ViewModels;

    /// <summary>
    /// specific application.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxApplication" />
    public class MvxApp : SimTuning.Core.MvxApp
    {
        /// <summary>
        /// Any initialization steps that can be done in the background.
        /// </summary>
        public override void Initialize()
        {
            // init des location services.
            typeof(SimTuning.Core.Models.LocationService).Assembly.CreatableTypes()
               .EndingWith("Service")
               .AsInterfaces()
               .RegisterAsLazySingleton();

            this.RegisterAppStart<MainPageViewModel>();

            base.Initialize();
        }
    }
}