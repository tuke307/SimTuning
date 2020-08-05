using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Einstellungen;

namespace SimTuning.Forms.UI.Views.Einstellungen
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class EinstellungenKontoView : MvxContentPage<EinstellungenKontoViewModel>
    {
        public EinstellungenKontoView()
        {
            InitializeComponent();
        }
    }
}