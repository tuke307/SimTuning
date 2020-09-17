namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Location;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Forms.UI.Business;
    using System.Globalization;
    using System.Threading.Tasks;
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
        public DynoRuntimeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxLocationWatcher locationWatcher, IMvxMessenger messenger)
            : base(logProvider, navigationService, locationWatcher, messenger)
        {
            ShowAudioCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<DynoAudioViewModel>());
            CloseCommand = new MvxAsyncCommand(async () => await NavigationService.Close(this));

            Functions.CheckAndRequestLocationWhenInUsePermission();
            Functions.CheckAndRequestMicrophonePermission();
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
        protected override async Task ResetAcceleration()
        {
            if (!this.CheckDynoData().Result)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.ResetAcceleration().ConfigureAwait(true);

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
            if (!this.CheckDynoData().Result)
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
        protected override async Task StopAcceleration()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.StopAcceleration().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Checks the dyno data.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CheckDynoData()
        {
            if (this.Dyno == null)
            {
                Functions.ShowSnackbarDialog(this.rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion Methods
    }
}