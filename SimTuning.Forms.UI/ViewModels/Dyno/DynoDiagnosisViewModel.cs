using MvvmCross.Commands;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        public DynoDiagnosisViewModel()
        {
            //Commands
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
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync("Bitte Datensatz auswählen um fortzufahren!"));
                return false;
            }
            else { return true; }
        }

        protected new async Task RefreshPlot()
        {
            if (!CheckDynoData())
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

            await Task.Run(() => base.RefreshPlot());

            await RaisePropertyChanged("PlotStrength");

            await loadingDialog.DismissAsync();
        }
    }
}