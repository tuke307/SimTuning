using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels.Tuning
{
    public class TuningInputViewModel : SimTuning.ViewModels.Tuning.InputViewModel
    {
        public TuningInputViewModel()
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
