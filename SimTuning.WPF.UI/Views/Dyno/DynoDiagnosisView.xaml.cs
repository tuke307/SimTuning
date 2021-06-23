// project=SimTuning.WPF.UI, file=DynoDiagnosisView.xaml.cs, creation=2020:9:7 Copyright
// (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views.Dyno
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.WPF.UI.Region;
    using SimTuning.WPF.UI.ViewModels.Dyno;

    /// <summary>
    /// DynoDiagnosisView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.Dyno.DynoDiagnosisViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("DynoRegion", mvxViewPosition.NewOrExsist)]
    public partial class DynoDiagnosisView : MvxWpfView<DynoDiagnosisViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDiagnosisView" /> class.
        /// </summary>
        public DynoDiagnosisView()
        {
            InitializeComponent();
        }
    }
}