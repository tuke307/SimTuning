using SimTuning.windows.Business;
using System.Threading.Tasks;

namespace SimTuning.windows.ViewModels.Dyno
{
    public class DynoDiagnosisViewModel : SimTuning.ViewModels.Dyno.DiagnosisViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public DynoDiagnosisViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel; //LoadingScreen

            RefreshPlotCommand = new AsyncCommand(async () => await RefreshPlot());
            InsertVehicleCommand = new ActionCommand(InsertVehicle);
            InsertEnvironmentCommand = new ActionCommand(InsertEnvironment);

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

            await Task.Run(() => base.RefreshPlot());

            OnPropertyChanged("PlotStrength");

            mainWindowViewModel.LoadingAnimation = false;
        }
    }
}