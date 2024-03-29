﻿// Copyright (c) 2021 tuke productions. All rights reserved.
using MaterialDesignThemes.Wpf;
using MvvmCross;
using SimTuning.WPF.UI.Services;
using SimTuning.WPF.UI.Views;

namespace SimTuning.WPF.UI
{
    /// <summary>
    /// WPF-spezifische App.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxApplication" />
    public class MvxApp : SimTuning.Core.MvxApp
    {
        /// <summary>
        /// Any initialization steps that can be done in the background.
        /// </summary>
        public override void Initialize()
        {
            this.RegisterAppStart<SimTuning.WPF.UI.ViewModels.MainViewModel>();

            MvvmCross.Mvx.IoCProvider.RegisterSingleton<IThemeService>(() => new ThemeServiceBase());

            base.Initialize();
        }
    }
}