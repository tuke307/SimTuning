using MvvmCross.Commands;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        private readonly ResourceManager rm;

        public DynoDiagnosisViewModel()
        {
            //Commands
            RefreshPlotCommand = new MvxAsyncCommand(() => RefreshPlot());
            InsertVehicleCommand = new MvxCommand(InsertVehicle);
            InsertEnvironmentCommand = new MvxCommand(InsertEnvironment);

            rm = new ResourceManager("resources", Assembly.GetExecutingAssembly());
            //datensatz checken
            //CheckDynoData();
        }

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync(message: rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture)).ConfigureAwait(false));
                return false;
            }
            else { return true; }
        }

        protected new async Task RefreshPlot()
        {
            if (!CheckDynoData())
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await Task.Run(() => base.RefreshPlot()).ConfigureAwait(true);

            await RaisePropertyChanged("PlotStrength").ConfigureAwait(false);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }
    }
}