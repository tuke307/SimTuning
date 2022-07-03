// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.App
{
    using MvvmCross.Core;
    using MvvmCross.Platforms.Wpf.Views;

    /// <summary>
    /// Normal App Start.
    /// </summary>
    public partial class App : MvxApplication
    {
        /// <summary>
        /// Registers the setup.
        /// </summary>
        protected override void RegisterSetup()
        {
            this.RegisterSetupType<MvxWpfSetup>();
        }
    }
}