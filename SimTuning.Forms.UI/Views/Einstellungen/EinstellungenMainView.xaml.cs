using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Einstellungen;

namespace SimTuning.Forms.UI.Views.Einstellungen
{
    [MvxTabbedPagePresentation(TabbedPosition.Root)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class EinstellungenMainView : MvxTabbedPage<EinstellungenMainViewModel>
    {
        public EinstellungenMainView(/*MainWindowViewModel mainWindowViewModel*/)
        {
            InitializeComponent();
        }
    }
}