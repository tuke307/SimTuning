// project=SimTuning.Forms.WPFCore, file=App.xaml.cs, creation=2020:7:31 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore
{
    using MvvmCross.Core;
    using SimTuning.Forms.WPFCore.Region;

    /// <summary>
    /// Normal App Start.
    /// </summary>
    public partial class App
    {
        protected override void RegisterSetup()
        {
            base.RegisterSetup();
            this.RegisterSetupType<MvxWpfSetup<MvxApp>>();
        }
    }
}