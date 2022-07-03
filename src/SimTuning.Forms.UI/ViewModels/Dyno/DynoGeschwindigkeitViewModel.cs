// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using SimTuning.Core.Services;
using SimTuning.Core.ViewModels.Dyno;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    /// <summary>
    /// DynoGeschwindigkeitViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.GeschwindigkeitViewModel" />
    public class DynoGeschwindigkeitViewModel : GeschwindigkeitViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoGeschwindigkeitViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="messenger">The messenger.</param>
        public DynoGeschwindigkeitViewModel(
            ILogger<DynoGeschwindigkeitViewModel> logger,
            IMvxNavigationService navigationService,
            IVehicleService vehicleService,
            IMvxMessenger messenger)
            : base(logger, navigationService, vehicleService, messenger)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.ShowAusrollenCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<DynoAusrollenViewModel>());

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc />
        protected override async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            await base.RefreshPlot().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Überprüft ob wichtige Dyno-Audio-Daten vorhanden sind.
        /// </summary>
        private bool CheckDynoData()
        {
            if (this.Dyno == null)
            {
                Forms.UI.Helpers.Functions.ShowSnackbarDialog(SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));

                return false;
            }

            if (this.Dyno.Geschwindigkeit == null)
            {
                Forms.UI.Helpers.Functions.ShowSnackbarDialog(SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));

                return false;
            }

            return true;
        }

        #endregion Methods

        #region Values

        private readonly ILogger<DynoGeschwindigkeitViewModel> _logger;

        #endregion Values
    }
}