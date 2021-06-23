// project=SimTuning.WPF.UI, file=MotorSteuerdiagrammView.xaml.cs, creation=2020:9:7
// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Views.Motor
{
    using MvvmCross.Platforms.Wpf.Presenters.Attributes;
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.WPF.UI.Region;
    using SimTuning.WPF.UI.ViewModels.Motor;

    /// <summary>
    /// MotorSteuerdiagrammView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.Motor.MotorSteuerdiagrammViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("MotorRegion", mvxViewPosition.NewOrExsist)]
    public partial class MotorSteuerdiagrammView : MvxWpfView<MotorSteuerdiagrammViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MotorSteuerdiagrammView" />
        /// class.
        /// </summary>
        public MotorSteuerdiagrammView()
        {
            this.InitializeComponent();
        }
    }
}