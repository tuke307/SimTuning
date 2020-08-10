// project=SimTuning.Forms.UI, file=DynoDiagnosisViewModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.UI.Business;
using System.Globalization;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        public DynoDiagnosisViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //override Commands
            RefreshPlotCommand = new MvxAsyncCommand(RefreshPlot);

            //datensatz checken
            //CheckDynoData();
        }

        protected new async Task RefreshPlot()
        {
            if (!CheckDynoData())
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.RefreshPlot().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                Functions.ShowSnackbarDialog(rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }
            else { return true; }
        }
    }
}