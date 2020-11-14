using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SimTuning.Forms.UI.ViewModels.Einstellungen;

namespace SimTuning.Forms.UI.Views.Einstellungen
{
    /// <summary>
    /// EinstellungenAussehenView.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Views.MvxContentPage{SimTuning.Forms.UI.ViewModels.Einstellungen.EinstellungenAussehenViewModel}" />
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class EinstellungenAussehenView : MvxContentPage<EinstellungenAussehenViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenAussehenView" />
        /// class.
        /// </summary>
        public EinstellungenAussehenView()
        {
            InitializeComponent();
        }
    }
}