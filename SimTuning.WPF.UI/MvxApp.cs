// project=SimTuning.WPF.UI, file=MvxApp.cs, creation=2020:7:30 Copyright (c) 2020 tuke
// productions. All rights reserved.
using MvvmCross;
using SimTuning.WPF.UI.Models;

namespace SimTuning.WPF.UI
{
    /// <summary>
    /// WPF-spezifische App.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxApplication" />
    public class MvxApp : SimTuning.Core.MvxApp
    {
        /// <summary>
        /// Any initialization steps that can be done in the background
        /// </summary>
        public override void Initialize()
        {
            this.RegisterAppStart<SimTuning.WPF.UI.ViewModels.MainViewModel>();

            Mvx.IoCProvider.RegisterSingleton<IThemeService>(() => new ThemeServiceBase());

            base.Initialize();
        }
    }
}