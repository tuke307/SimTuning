namespace SimTuning.Forms.WPFCore.Views.Einstellungen
{
    using MvvmCross.Platforms.Wpf.Views;
    using SimTuning.Forms.WPFCore.Region;
    using SimTuning.Forms.WPFCore.ViewModels.Einstellungen;

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