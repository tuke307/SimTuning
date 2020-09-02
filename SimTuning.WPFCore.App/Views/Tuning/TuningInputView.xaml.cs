// project=SimTuning.WPF.UI, file=TuningInputView.xaml.cs, creation=2020:7:7 Copyright (c)
// 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Tuning;

namespace SimTuning.WPFCore.App.Views.Tuning
{
    [MvxWpfPresenter("TuningRegion", mvxViewPosition.NewOrExsist)]
    public partial class TuningInputView : MvxWpfView<TuningInputViewModel>
    {
        public TuningInputView()
        {
            InitializeComponent();
        }
    }
}