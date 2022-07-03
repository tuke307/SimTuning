// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Models;
    using SimTuning.Core.Services;
    using SimTuning.Forms.UI.Helpers;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    using XF.Material.Forms.UI.Dialogs;

    /// <summary>
    /// DynoRuntimeViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.RuntimeViewModel" />
    public class DynoRuntimeViewModel : SimTuning.Core.ViewModels.Dyno.RuntimeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoRuntimeViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="locationWatcher">The location watcher.</param>
        /// <param name="messenger"></param>
        public DynoRuntimeViewModel(
            ILogger<DynoRuntimeViewModel> logger,
            IMvxNavigationService navigationService,
            ILocationService locationService,
            IMvxMessenger messenger,
            IVehicleService vehicleService)
            : base(logger, navigationService, locationService, messenger, vehicleService)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.ShowSpectrogramCommand = new MvxAsyncCommand(async () =>
            {
                // await
                // this.NavigationService.Navigate<DynoAudioPlayerViewModel>().ConfigureAwait(true);
                await _navigationService.Navigate<DynoSpectrogramViewModel>().ConfigureAwait(true);
            });
            // this.CloseCommand = new MvxAsyncCommand(async () => await
            // this.NavigationService.Close(this));

            // Farben vorbelegen
            stdBg = XF.Material.Forms.Material.Color.Background;
            stdSur = XF.Material.Forms.Material.Color.Surface;
            this.PageBackColor = stdBg;
            this.SpeedBackColor = stdSur;

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc />
        protected override async Task ResetRun()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            await base.ResetRun().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override async Task StartBeschleunigung()
        {
            if (!await this.CheckDynoData().ConfigureAwait(true))
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            await base.StartBeschleunigung().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Stops the acceleration.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        // protected override async Task StopRun() { var loadingDialog = await
        // MaterialDialog.Instance.LoadingDialogAsync(message:
        // SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources),
        // "MES_LOAD")).ConfigureAwait(false);

        // await base.StopRun().ConfigureAwait(true);

        // await loadingDialog.DismissAsync().ConfigureAwait(false); }

        /// <summary>
        /// Überprüft ob wichtige Dyno-Audio-Daten vorhanden sind.
        /// </summary>
        private async Task<bool> CheckDynoData()
        {
            var location = await Functions.GetPermission<Permissions.LocationWhenInUse>().ConfigureAwait(true);
            if (!location)
            {
                Functions.ShowSnackbarDialog(SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_LOCATION"));

                return false;
            }

            var microphone = await Functions.GetPermission<Permissions.Microphone>().ConfigureAwait(true);
            if (!microphone)
            {
                Functions.ShowSnackbarDialog(SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_MICROPHONE"));

                return false;
            }

            if (this.Dyno == null)
            {
                Functions.ShowSnackbarDialog(SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));

                return false;
            }

            return true;
        }

        #endregion Methods

        #region Values

        private readonly ILogger<DynoRuntimeViewModel> _logger;

        #endregion Values
    }
}