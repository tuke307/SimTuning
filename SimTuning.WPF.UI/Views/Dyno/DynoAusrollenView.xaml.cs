﻿// project=SimTuning.WPF.UI, file=DynoAusrollenView.xaml.cs, creation=2020:10:19 Copyright
// (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views.Dyno
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.WPF.UI.Region;
    using SimTuning.WPF.UI.ViewModels.Dyno;

    /// <summary>
    /// DynoAusrollenView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.Dyno.DynoAusrollenViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("DynoRegion", mvxViewPosition.NewOrExsist)]
    public partial class DynoAusrollenView : MvxWpfView<DynoAusrollenViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoAusrollenView" /> class.
        /// </summary>
        public DynoAusrollenView()
        {
            InitializeComponent();
        }
    }
}