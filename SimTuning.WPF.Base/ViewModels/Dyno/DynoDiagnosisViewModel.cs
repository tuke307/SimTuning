using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace SimTuning.WPF.Base.ViewModels.Dyno
{
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        //private readonly MainWindowViewModel mainWindowViewModel;

        public DynoDiagnosisViewModel/*MainWindowViewModel mainWindowViewModel*/(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //this.mainWindowViewModel = mainWindowViewModel; //LoadingScreen

            RefreshPlotCommand = new MvxAsyncCommand(RefreshPlot);

            //datensatz checken
            //CheckDynoData();
        }

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue("Bitte Datensatz auswählen um fortzufahren!");
                return false;
            }
            else { return true; }
        }

        protected new async Task RefreshPlot()
        {
            if (!CheckDynoData())
                return;

            //mainWindowViewModel.LoadingAnimation = true;

            await base.RefreshPlot().ConfigureAwait(true);

            //mainWindowViewModel.LoadingAnimation = false;
        }
    }
}