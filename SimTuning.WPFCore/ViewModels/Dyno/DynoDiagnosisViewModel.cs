using MvvmCross.Commands;
using System.Threading.Tasks;

namespace SimTuning.WPFCore.ViewModels.Dyno
{
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public DynoDiagnosisViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel; //LoadingScreen

            RefreshPlotCommand = new MvxAsyncCommand(RefreshPlot);

            //datensatz checken
            //CheckDynoData();
        }

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Bitte Datensatz auswählen um fortzufahren!");
                return false;
            }
            else { return true; }
        }

        protected new async Task RefreshPlot()
        {
            if (!CheckDynoData())
                return;

            mainWindowViewModel.LoadingAnimation = true;

            await base.RefreshPlot().ConfigureAwait(true);

            mainWindowViewModel.LoadingAnimation = false;
        }
    }
}