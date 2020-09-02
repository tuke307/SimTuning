// project=SimTuning.WPF.App, file=EinstellungenApplicationView.xaml.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.App.Views.Einstellungen
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.WPF.UI.Region;
    using SimTuning.WPF.UI.ViewModels.Einstellungen;

    /// <summary>
    /// EinstellungenApplicationView.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Wpf.Views.MvxWpfView" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    [MvxWpfPresenter("EinstellungenRegion", mvxViewPosition.NewOrExsist)]
    public partial class EinstellungenApplicationView : MvxWpfView<EinstellungenApplicationViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenApplicationView" />
        /// class.
        /// </summary>
        public EinstellungenApplicationView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Data.UnitSettings.Default.Save();
        }
    }
}