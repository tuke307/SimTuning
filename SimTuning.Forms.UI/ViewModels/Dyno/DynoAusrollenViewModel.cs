using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using SimTuning.Core.ViewModels.Dyno;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoAusrollenViewModel : AusrollenViewModel
    {
        public DynoAusrollenViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, IMvxMessenger messenger)
              : base(logFactory, navigationService, messenger)
        {
            this.ShowDiagnosisCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<DynoDiagnosisViewModel>());
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Refreshes the plot.
        /// </summary>
        protected override async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            await base.RefreshPlot().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        private bool CheckDynoData()
        {
            if (this.Dyno == null)
            {
                Forms.UI.Business.Functions.ShowSnackbarDialog(SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));

                return false;
            }

            if (this.Dyno.Ausrollen == null)
            {
                Forms.UI.Business.Functions.ShowSnackbarDialog(SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));

                return false;
            }

            return true;
        }
    }
}