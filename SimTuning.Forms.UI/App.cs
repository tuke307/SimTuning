// project=SimTuning.Forms.UI, file=App.cs, creation=2020:6:30 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.Forms.UI
{
    using MediaManager;
    using MvvmCross;
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
            Mvx.IoCProvider.RegisterSingleton(CrossMediaManager.Current);

            // CrossMediaManager.Current.Library.Providers.Add(new MediaItemProvider());

            this.RegisterAppStart<MainPageViewModel>();
        }
    }
}