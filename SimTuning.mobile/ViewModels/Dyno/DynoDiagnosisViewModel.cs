using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels.Dyno
{
    public class DynoDiagnosisViewModel : SimTuning.ViewModels.Dyno.DiagnosisViewModel
    {
        public DynoDiagnosisViewModel()
        {
            //Commands
            RefreshPlotCommand = new Command(async () => await RefreshPlot());
            InsertVehicleCommand = new Command(InsertVehicle);
            InsertEnvironmentCommand = new Command(InsertEnvironment);

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

            RaisePropertyChanged("PlotStrength");

            await loadingDialog.DismissAsync();
        }
    }
}