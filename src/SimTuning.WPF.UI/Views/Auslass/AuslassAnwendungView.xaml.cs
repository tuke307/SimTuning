// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using Mvx.Wpf.ItemsPresenter;
using SimTuning.WPF.UI.ViewModels.Auslass;

namespace SimTuning.WPF.UI.Views.Auslass
{
    [MvxWpfPresenter("AuslassRegion", mvxViewPosition.NewOrExsist)]
    public partial class AuslassAnwendungView : MvxWpfView<AuslassAnwendungViewModel>
    {
        public AuslassAnwendungView()
        {
            this.InitializeComponent();
        }
    }
}