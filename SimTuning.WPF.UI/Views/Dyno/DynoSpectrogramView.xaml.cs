// project=SimTuning.WPF.UI, file=DynoSpectrogramView.xaml.cs, creation=2020:7:7 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views.Dyno
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.WPF.UI.Region;
    using SimTuning.WPF.UI.ViewModels.Dyno;

    /// <summary>
    /// DynoSpectrogramView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.Dyno.DynoSpectrogramViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("DynoRegion", mvxViewPosition.NewOrExsist)]
    public partial class DynoSpectrogramView : MvxWpfView<DynoSpectrogramViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoSpectrogramView" /> class.
        /// </summary>
        public DynoSpectrogramView()
        {
            InitializeComponent();
        }
    }
}