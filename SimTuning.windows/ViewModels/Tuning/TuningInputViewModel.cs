using System;
using System.Collections.Generic;
using System.Text;

namespace SimTuning.windows.ViewModels.Tuning
{
    public class TuningInputViewModel : SimTuning.ViewModels.Tuning.InputViewModel
    {
        MainWindowViewModel mainWindowViewModel;

        public TuningInputViewModel(MainWindowViewModel mainWindowViewModel)
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
