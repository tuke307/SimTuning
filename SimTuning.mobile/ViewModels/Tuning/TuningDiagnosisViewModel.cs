using OxyPlot;
using SimTuning.ModuleLogic;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels
{
    public class TuningDiagnosisViewModel : SimTuning.ViewModels.Tuning.DiagnosisViewModel
    {
        public TuningDiagnosisViewModel()
        {
        }

        private bool CheckTuningData()
        {
            if (Tuning == null)
            {
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync("Bitte Datensatz auswählen um fortzufahren!"));
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