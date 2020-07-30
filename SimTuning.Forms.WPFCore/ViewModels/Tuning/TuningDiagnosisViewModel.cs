using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.Forms.WPFCore.ViewModels.Tuning
{
    public class TuningDiagnosisViewModel : SimTuning.Core.ViewModels.Tuning.DiagnosisViewModel
    {
        //private MainWindowViewModel mainWindowViewModel;

        public TuningDiagnosisViewModel/*MainWindowViewModel mainWindowViewModel*/(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //this.mainWindowViewModel = mainWindowViewModel;
        }

        private bool CheckTuningData()
        {
            if (Tuning == null)
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue("Bitte Datensatz auswählen um fortzufahren!");
                return false;
            }
            else { return true; }
        }

        private void SaveTuning()
        {
            if (!CheckTuningData())
                return;
        }
    }
}