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
            //return new CustomViewPresenter(root);
        }
    }
}