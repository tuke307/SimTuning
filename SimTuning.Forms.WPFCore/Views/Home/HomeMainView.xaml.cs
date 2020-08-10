// project=SimTuning.Forms.WPFCore, file=HomeMainView.xaml.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Home;

namespace SimTuning.Forms.WPFCore.Views.Home
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class HomeMainView : MvxWpfView<HomeMainViewModel>
    {
        public HomeMainView()
        {
            InitializeComponent();
        }
    }
}