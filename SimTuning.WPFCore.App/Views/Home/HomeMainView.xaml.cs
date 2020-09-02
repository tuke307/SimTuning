// project=SimTuning.WPF.UI, file=HomeMainView.xaml.cs, creation=2020:7:7 Copyright (c)
// 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Home;

namespace SimTuning.WPFCore.App.Views.Home
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