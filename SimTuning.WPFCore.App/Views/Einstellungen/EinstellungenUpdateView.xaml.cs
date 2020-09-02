// project=SimTuning.WPF.UI, file=EinstellungenUpdateView.xaml.cs, creation=2020:7:7
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPFCore.App.Views.Einstellungen
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.WPF.UI.Region;
    using SimTuning.WPF.UI.ViewModels.Einstellungen;

    /// <summary>
    /// EinstellungenUpdateView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView{SimTuning.WPF.UI.ViewModels.Einstellungen.EinstellungenUpdateViewModel}" />
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