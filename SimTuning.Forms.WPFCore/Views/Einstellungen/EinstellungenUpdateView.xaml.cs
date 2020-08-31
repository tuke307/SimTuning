// project=SimTuning.Forms.WPFCore, file=EinstellungenUpdateView.xaml.cs,
// creation=2020:7:31 Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.Views.Einstellungen
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.Forms.WPFCore.Region;
    using SimTuning.Forms.WPFCore.ViewModels.Einstellungen;

    /// <summary>
    /// EinstellungenUpdateView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.Forms.WPFCore.ViewModels.Einstellungen.EinstellungenUpdateViewModel}" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("EinstellungenRegion", mvxViewPosition.NewOrExsist)]
    public partial class EinstellungenUpdateView : MvxWpfView<EinstellungenUpdateViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenUpdateView" />
        /// class.
        /// </summary>
        public EinstellungenUpdateView()
        {
            InitializeComponent();
        }
    }
}