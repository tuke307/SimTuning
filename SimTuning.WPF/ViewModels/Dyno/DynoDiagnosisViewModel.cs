using MvvmCross.Commands;
using System.Threading.Tasks;

namespace SimTuning.WPF.ViewModels.Dyno
{
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public DynoDiagnosisViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel; //LoadingScreen

            RefreshPlotCommand = new MvxAsyncCommand(async () => await RefreshPlot());
            InsertVehicleCommand = new MvxCommand(InsertVehicle);
            InsertEnvironmentCommand = new MvxCommand(InsertEnvironment);

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

            await RaisePropertyChanged("PlotStrength");

            mainWindowViewModel.LoadingAnimation = false;
        }
    }
}