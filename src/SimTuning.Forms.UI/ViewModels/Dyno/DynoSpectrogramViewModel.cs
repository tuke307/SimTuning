// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    using MediaManager;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Services;
    using SimTuning.Forms.UI.Helpers;
    using System.IO;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using XF.Material.Forms.UI.Dialogs;

    /// <summary>
    /// DynoSpectrogramViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.SpectrogramViewModel" />
    public class DynoSpectrogramViewModel : SimTuning.Core.ViewModels.Dyno.SpectrogramViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoSpectrogramViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoSpectrogramViewModel(
            ILogger<DynoSpectrogramViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger,
            IVehicleService vehicleService,
            IMediaManager mediaManager)
            : base(logger, navigationService, messenger, vehicleService, mediaManager)
        {
            this._logger = logger;
        }

        #region Values

        private readonly ILogger<DynoSpectrogramViewModel> _logger;
        private ImageSource _displayedImage;

        public ImageSource DisplayedImage
        {
            get => _displayedImage;
            set => SetProperty(ref _displayedImage, value);
        }

        /// <summary>
        /// Gets the show beschleunigung command.
        /// </summary>
        /// <value>The show beschleunigung command.</value>
        public MvxAsyncCommand ShowBeschleunigungCommand { get; private set; }

        #endregion Values

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            // override Commands
            this.RefreshSpectrogramCommand = new MvxAsyncCommand(this.ReloadImageAudioSpectrogram);
            this.SpecificGraphCommand = new MvxAsyncCommand(this.SpecificGraph);

            this.ShowBeschleunigungCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<DynoGeschwindigkeitViewModel>());
            // datensatz checken CheckDynoData();

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc />
        protected override async Task FilterPlot()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            await base.FilterPlot().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override async Task RefreshAudioFileAsync()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            // using (var client = new WebClient()) {
            // client.DownloadFile("https://simtuning.tuke-productions.de/wp-content/uploads/sample.wav",
            // SimTuning.Core.GeneralSettings.AudioFilePath); } await
            // this.NavigationService.Navigate<DynoAudioPlayerViewModel>().ConfigureAwait(true);

            await base.RefreshAudioFileAsync().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);

            await this.ReloadImageAudioSpectrogram().ConfigureAwait(true);
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.SpectrogramViewModel.RefreshPlot" />
        protected override async Task RefreshPlot()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            await base.RefreshPlot().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.SpectrogramViewModel.ReloadImageAudioSpectrogram" />
        protected new async Task ReloadImageAudioSpectrogram()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            Stream stream = base.ReloadImageAudioSpectrogram();
            this.DisplayedImage = ImageSource.FromStream(() => stream);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.SpectrogramViewModel.SpecificGraph" />
        protected new async Task SpecificGraph()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            base.SpecificGraph();

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Überprüft ob wichtige Dyno-Audio-Daten vorhanden sind.
        /// </summary>
        private bool CheckDynoData()
        {
            if (!File.Exists(SimTuning.Core.GeneralSettings.AudioAccelerationFilePath))
            {
                Functions.ShowSnackbarDialog(SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NOAUDIOFILE"));

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
    }
}