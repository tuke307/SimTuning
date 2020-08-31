using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.Forms.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenApplicationViewModel : SimTuning.Core.ViewModels.Einstellungen.ApplicationViewModel
    {
        public EinstellungenApplicationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
: base(logProvider, navigationService)
        {
        }
    }
}