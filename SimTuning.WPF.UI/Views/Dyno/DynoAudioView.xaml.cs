// project=SimTuning.WPF.UI, file=DynoAudioView.xaml.cs, creation=2020:9:7 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views.Dyno
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.WPF.UI.Region;
    using SimTuning.WPF.UI.ViewModels.Dyno;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// DynoAudio-View.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.Dyno.DynoAudioViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("DynoRegion", mvxViewPosition.NewOrExsist)]
    public partial class DynoAudioView : MvxWpfView<DynoAudioViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoAudioView" /> class.
        /// </summary>
        public DynoAudioView()
        {
            this.InitializeComponent();
        }
    }
}