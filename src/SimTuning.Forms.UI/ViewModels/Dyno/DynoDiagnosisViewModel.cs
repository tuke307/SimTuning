// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Services;
    using SimTuning.Forms.UI.Helpers;
    using System.Threading.Tasks;
    using XF.Material.Forms.UI.Dialogs;

    /// <summary>
    /// DynoDiagnosisViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel" />
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDiagnosisViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoDiagnosisViewModel(
            ILogger<DynoDiagnosisViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger,
            IVehicleService vehicleService)
            : base(logger, navigationService, messenger, vehicleService)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            // override Commands
            this.RefreshPlotCommand = new MvxAsyncCommand(this.RefreshPlot);

            // datensatz checken CheckDynoData();

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.DiagnosisViewModel.RefreshPlot" />
        protected new async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            base.RefreshPlot();

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Überprüft ob wichtige Dyno-Audio-Daten vorhanden sind.
        /// </summary>
        private bool CheckDynoData()
        {
            if (this.Dyno == null)
            {
                Functions.ShowSnackbarDialog(SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));

                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<DynoDiagnosisViewModel> _logger;

        #endregion Values
    }
}