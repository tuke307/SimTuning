// project=SimTuning.WPF.App, file=TuningMainView.xaml.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Tuning;

namespace SimTuning.WPF.App.Views.Tuning
{
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    public partial class TuningMainView : MvxWpfView<TuningMainViewModel>
    {
        public TuningMainView()
        {
            InitializeComponent();
        }
    }
}