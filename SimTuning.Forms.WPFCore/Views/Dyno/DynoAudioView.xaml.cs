// project=SimTuning.Forms.WPFCore, file=DynoAudioView.xaml.cs, creation=2020:7:7
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.Views.Dyno
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.Forms.WPFCore.Region;
    using SimTuning.Forms.WPFCore.ViewModels.Dyno;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// DynoAudio-View.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.Forms.WPFCore.ViewModels.Dyno.DynoAudioViewModel}" />
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