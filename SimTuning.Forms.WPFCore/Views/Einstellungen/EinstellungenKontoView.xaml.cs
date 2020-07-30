using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.Forms.WPFCore.Region;
using SimTuning.Forms.WPFCore.ViewModels.Einstellungen;

namespace SimTuning.Forms.WPFCore.Views.Einstellungen
{
    [MvxWpfPresenter("EinstellungenRegion", mvxViewPosition.NewOrExsist)]
    //[MvxRegionPresentation(RegionName = "KontoRegion", WindowIdentifier = nameof(EinstellungenMainView))]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class EinstellungenKontoView : MvxWpfView<EinstellungenKontoViewModel>
    {
        public EinstellungenKontoView()
        {
            InitializeComponent();
        }
    }
}