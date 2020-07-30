using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.WPF.Base.ViewModels.Tuning
{
    public class TuningInputViewModel : SimTuning.Core.ViewModels.Tuning.InputViewModel
    {
        //private MainWindowViewModel mainWindowViewModel;

        public TuningInputViewModel/*MainWindowViewModel mainWindowViewModel*/(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
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