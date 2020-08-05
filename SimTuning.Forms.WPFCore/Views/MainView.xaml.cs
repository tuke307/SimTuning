using System;
using System.Windows;
using System.Windows.Input;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.Forms.WPFCore.Business;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.Views.Dialog;

namespace SimTuning.Forms.WPFCore.Views
{
    [MvxWpfPresenter("MainWindowRegion", mvxViewPosition.NewOrExsist)]
    public partial class MainView : MvxWpfView<SimTuning.Forms.WPFCore.ViewModels.MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}