// project=SimTuning.Forms.UI, file=DynoSpectrogramViewModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    using MediaManager;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Forms.UI.Business;
    using System.Globalization;
    using System.IO;
    using System.Net;
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
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoSpectrogramViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger, IMediaManager mediaManager)
            : base(logProvider, navigationService, messenger, mediaManager)
        {
            // override Commands
            this.FilterPlotCommand = new MvxAsyncCommand(this.FilterPlot);
            this.RefreshSpectrogramCommand = new MvxAsyncCommand(this.ReloadImageAudioSpectrogram);
            this.RefreshAudioFileCommand = new MvxAsyncCommand(this.PlayFileAsync);
            this.RefreshPlotCommand = new MvxAsyncCommand(this.RefreshPlot);
            this.SpecificGraphCommand = new MvxAsyncCommand(this.SpecificGraph);

            this.ShowBeschleunigungCommand = new MvxAsyncCommand(() => this.NavigationService.Navigate<DynoBeschleunigungViewModel>());
            // datensatz checken CheckDynoData();
        }

        #region Values

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
        /// Filters the plot.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected new async Task FilterPlot()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.FilterPlot().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected new async Task PlayFileAsync()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            //using (var client = new WebClient())
            //{
            //    client.DownloadFile("https://simtuning.tuke-productions.de/wp-content/uploads/sample.wav", SimTuning.Core.GeneralSettings.AudioFilePath);
            //}
            await this.NavigationService.Navigate<DynoAudioPlayerViewModel>().ConfigureAwait(true);

            await base.PlayFileAsync().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);

            await this.ReloadImageAudioSpectrogram().ConfigureAwait(true);
        }

        /// <summary>
        /// Refreshes the plot.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected new async Task RefreshPlot()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.RefreshPlot().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Reloads the image audio spectrogram.
        /// </summary>
        protected new async Task ReloadImageAudioSpectrogram()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            Stream stream = base.ReloadImageAudioSpectrogram();
            this.DisplayedImage = ImageSource.FromStream(() => stream);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Specifics the graph.
        /// </summary>
        protected new async Task SpecificGraph()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            base.SpecificGraph();

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Checks the dyno data.
        /// </summary>
        /// <returns></returns>
        private bool CheckDynoData()
        {
            if (!File.Exists(SimTuning.Core.GeneralSettings.AudioFilePath))
            {
                Functions.ShowSnackbarDialog(this.rm.GetString("ERR_NOAUDIOFILE", CultureInfo.CurrentCulture));

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