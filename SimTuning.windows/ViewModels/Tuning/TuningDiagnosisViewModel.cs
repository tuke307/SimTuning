using OxyPlot;
using SimTuning.ModuleLogic;

namespace SimTuning.windows.ViewModels.Tuning
{
    public class TuningDiagnosisViewModel : SimTuning.ViewModels.Tuning.DiagnosisViewModel
    {
        MainWindowViewModel mainWindowViewModel;
        public TuningDiagnosisViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;


        }

        private bool CheckTuningData()
        {
            if (Tuning == null)
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Bitte Datensatz auswählen um fortzufahren!");
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