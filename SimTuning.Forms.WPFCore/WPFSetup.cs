﻿using System.Windows.Controls;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Presenters;
using MvvmCross.ViewModels;
using SimTuning.WPFCore.Region;

namespace SimTuning.Forms.WPFCore
{
    //public class WPFSetup : MvxWpfSetup<SimTuning.WPFCore.App>
    //{
    //    protected override IMvxApplication CreateApp()
    //    {
    //        return new SimTuning.WPFCore.App();
    //    }

    //    protected override IMvxWpfViewPresenter CreateViewPresenter(ContentControl root)
    //    {
    //        return new CustomViewPresenter(root);
    //    }

    //    //protected override void InitializeFirstChance()
    //    //{
    //    //    base.InitializeFirstChance();

    //    //    Mvx.IoCProvider.RegisterSingleton<CustomViewPresenter>(() => new CustomViewPresenter(_presenter));
    //    //}
    //}
}