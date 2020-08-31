// project=SimTuning.Forms.WPFCore, file=MvxWpfSetup.cs, creation=2020:7:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MvvmCross.Platforms.Wpf.Presenters;
using MvvmCross.ViewModels;

namespace SimTuning.Forms.WPFCore.Region
{
    public class MvxWpfSetup<TApplication> : MvvmCross.Platforms.Wpf.Core.MvxWpfSetup<TApplication> where TApplication : class, IMvxApplication, new()
    {
        protected override IMvxWpfViewPresenter CreateViewPresenter(ContentControl root)
        {
            return new MvxWpfPresenter(root);
        }
    }
}