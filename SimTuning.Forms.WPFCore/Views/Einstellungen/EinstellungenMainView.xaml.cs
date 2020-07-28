using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimTuning.WPFCore.Region;
using SimTuning.WPFCore.ViewModels.Einstellungen;

namespace SimTuning.Forms.WPFCore.Views.Einstellungen
{
    //[MvxWindowPresentation(Identifier = nameof(EinstellungenMainView), Modal = true)]
    //[MvxViewFor(typeof(EinstellungenMainViewModel))]
    //[MvxRegionPresentation(RegionName = "PageContent", WindowIdentifier = nameof(MainView))]
    [MvxWpfPresenter("PageContent", mvxViewPosition.NewOrExsist)]
    //[MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class EinstellungenMainView /*: MvxWpfView<EinstellungenMainViewModel>*/
    {
        public EinstellungenMainView()
        {
            InitializeComponent();
        }
    }
}