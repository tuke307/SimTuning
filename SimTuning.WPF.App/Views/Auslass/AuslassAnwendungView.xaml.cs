// project=SimTuning.WPF.App, file=AuslassAnwendungView.xaml.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Auslass;

namespace SimTuning.WPF.App.Views.Auslass
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