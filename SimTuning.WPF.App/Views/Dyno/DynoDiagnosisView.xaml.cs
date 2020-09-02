// project=SimTuning.WPF.App, file=DynoDiagnosisView.xaml.cs, creation=2020:9:2 Copyright
// (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPF.UI.Region;
using SimTuning.WPF.UI.ViewModels.Dyno;

namespace SimTuning.WPF.App.Views.Dyno
{
    [MvxWpfPresenter("DynoRegion", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class DynoDiagnosisView : MvxWpfView<DynoDiagnosisViewModel>
    {
        public DynoDiagnosisView()
        {
            InitializeComponent();
        }
    }
}