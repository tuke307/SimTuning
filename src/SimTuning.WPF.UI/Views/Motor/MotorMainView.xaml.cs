// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using Mvx.Wpf.ItemsPresenter;
using SimTuning.WPF.UI.ViewModels.Motor;

namespace SimTuning.WPF.UI.Views.Motor
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class MotorMainView : MvxWpfView<MotorMainViewModel>
    {
        public MotorMainView()
        {
            InitializeComponent();
        }
    }
}