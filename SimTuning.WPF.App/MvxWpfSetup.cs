// project=SimTuning.WPF.UI, file=MvxWpfSetup.cs, creation=2020:7:30 Copyright (c) 2020
// tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters;
using MvvmCross.ViewModels;
using SimTuning.WPF.UI.Region;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;

namespace SimTuning.WPF.App
{
    public class MvxWpfSetup<TApplication> : MvvmCross.Platforms.Wpf.Core.MvxWpfSetup<TApplication>
        where TApplication : class, IMvxApplication, new()
    {
        public override IEnumerable<Assembly> GetViewAssemblies()
        {
            var list = new List<Assembly>();
            list.AddRange(base.GetViewAssemblies());
            list.Add(typeof(SimTuning.WPF.UI.Views.MainWindow).Assembly);
            return list.ToArray();
        }

        protected override IMvxWpfViewPresenter CreateViewPresenter(ContentControl root)
        {
            return new MvxWpfPresenter(root);
        }
    }
}