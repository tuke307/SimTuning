using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels.Einstellungen;

namespace SimTuning.Forms.WPFCore.Views.Einstellungen
{
    [MvxWpfPresenter("EinstellungRegion", mvxViewPosition.NewOrExsist)]
    //[MvxRegionPresentation(RegionName = "UpdateRegion", WindowIdentifier = nameof(EinstellungenMainView))]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class EinstellungenUpdateView : MvxWpfView<EinstellungenUpdateViewModel>
    {
        public EinstellungenUpdateView()
        {
            InitializeComponent();
        }
    }
}