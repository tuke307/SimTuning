namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Location;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Models;
    using SimTuning.Forms.UI.Business;
    using System.Globalization;
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
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="locationWatcher">The location watcher.</param>
        /// <param name="messenger"></param>
        public DynoRuntimeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ILocationService locationService, IMvxMessenger messenger)
            : base(logProvider, navigationService, locationService, messenger)
        {
            this.ShowSpectrogramCommand = new MvxAsyncCommand(async () => await this.NavigationService.Navigate<DynoSpectrogramViewModel>().ConfigureAwait(true));
            // this.CloseCommand = new MvxAsyncCommand(async () => await
            // this.NavigationService.Close(this));

            // Farben vorbelegen
            this.PageBackColor = XF.Material.Forms.Material.Color.Background;
            this.SpeedBackColor = XF.Material.Forms.Material.Color.Surface;
        }

        #region Methods

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
        /// Resets the acceleration.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected override async Task ResetBeschleunigung()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.ResetBeschleunigung().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Starts the acceleration.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected override async Task StartBeschleunigung()
        {
            if (!await this.CheckDynoData().ConfigureAwait(true))
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

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
        protected override async Task StopBeschleunigung()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.StopBeschleunigung().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Checks the dyno data.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CheckDynoData()
        {
            var location = await Functions.GetPermission<Permissions.LocationWhenInUse>().ConfigureAwait(true);
            if (!location)
            {
                Functions.ShowSnackbarDialog(this.rm.GetString("ERR_LOCATION", CultureInfo.CurrentCulture));

                return false;
            }

            var microphone = await Functions.GetPermission<Permissions.Microphone>().ConfigureAwait(true);
            if (!microphone)
            {
                Functions.ShowSnackbarDialog(this.rm.GetString("ERR_MICROPHONE", CultureInfo.CurrentCulture));

                return false;
            }

            if (this.Dyno == null)
            {
                Functions.ShowSnackbarDialog(this.rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }

            return true;
        }

        #endregion Methods
    }
}