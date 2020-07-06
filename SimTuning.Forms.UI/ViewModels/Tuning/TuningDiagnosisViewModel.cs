using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Tuning
{
    public class TuningDiagnosisViewModel : SimTuning.Core.ViewModels.Tuning.DiagnosisViewModel
    {
        public TuningDiagnosisViewModel()
        {
        }

        private bool CheckTuningData()
        {
            if (Tuning == null)
            {
                Task.Run(() => MaterialDialog.Instance.SnackbarAsync("Bitte Datensatz auswählen um fortzufahren!"));
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