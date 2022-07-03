// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    using MaterialDesignThemes.Wpf;
    using MediaManager;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Models;
    using SimTuning.Core.Services;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Dialog;
    using SimTuning.WPF.UI.Messages;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// WPF-spezifisches Dyno-Spectrogram-ViewModel.
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
            this._token = _messenger.Subscribe<MvxReloaderMessage>(this.ReloadData);
            this._logger = logger;
        }

        #region Methods

        // private IMvxCommand _navigateCommand; public IMvxCommand NavigateCommand { get
        // { _navigateCommand = _navigateCommand ?? new MvxCommand(() =>
        // ShowViewModel<TViewModel>()); return _navigateCommand; } }

        /// <inheritdoc />
        public override Task Initialize()
        {
            // override Commands
            this.SpecificGraphCommand = new MvxAsyncCommand(this.SpecificGraph);
            this.RefreshSpectrogramCommand = new MvxAsyncCommand(this.ReloadImageAudioSpectrogram);
            // this.OpenFileCommand = new MvxAsyncCommand(this.OpenFileDialog); datensatz
            // checken CheckDynoData();

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
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(async () =>
                {
                    await base.FilterPlot().ConfigureAwait(true);

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// Opens the file dialog.
        /// </summary>
        // protected new async Task OpenFileDialog() { if (!this.CheckDynoData()) {
        // return; }

        // FileData fileData = await CrossFilePicker.Current.PickFile(new string[] { "Zip
        // Datei (*.zip)|*.zip" }).ConfigureAwait(true);

        // // user canceled file picking if (fileData == null) { return; }

        // string filepath = fileData.FilePath; fileData.Dispose();

        // await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender,
        // DialogOpenedEventArgs args) => { Task.Run(async () => { await
        // base.OpenFileDialog(filepath).ConfigureAwait(true);
        // Application.Current.Dispatcher.Invoke(() => args.Session.Close()); });
        // }).ConfigureAwait(true);

        // await ReloadImageAudioSpectrogram().ConfigureAwait(true); }

        /// <inheritdoc />
        protected override async Task RefreshAudioFileAsync()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            // using (var client = new WebClient()) {
            // client.DownloadFile("https://simtuning.tuke-productions.de/wp-content/uploads/sample.wav",
            // SimTuning.Core.GeneralSettings.AudioFilePath); }

            await this._navigationService.Navigate<DynoAudioPlayerViewModel>().ConfigureAwait(true);
            await base.RefreshAudioFileAsync().ConfigureAwait(true);
            // await this.ReloadImageAudioSpectrogram().ConfigureAwait(true);
        }

        /// <inheritdoc />
        protected override async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(async () =>
                {
                    await base.RefreshPlot().ConfigureAwait(true);

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.SpectrogramViewModel.ReloadImageAudioSpectrogram" />
        protected new async Task ReloadImageAudioSpectrogram()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(async () =>
                {
                    Stream stream = base.ReloadImageAudioSpectrogram();
                    PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    this.DisplayedImage = decoder.Frames[0];

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.SpectrogramViewModel.SpecificGraph" />
        protected new Task SpecificGraph()
        {
            return DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
             {
                 Task.Run(async () =>
                 {
                     base.SpecificGraph();

                     Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                 });
             });
        }

        /// <summary>
        /// Überprüft ob wichtige Dyno-Audio-Daten vorhanden sind.
        /// </summary>
        private bool CheckDynoData()
        {
            if (this.Dyno == null)
            {
                _messenger.Publish(
                    new ShowSnackbarMessage(
                        this,
                        SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA")));

                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<DynoSpectrogramViewModel> _logger;
        private readonly MvxSubscriptionToken _token;
        private BitmapSource _displayedImage;

        public BitmapSource DisplayedImage
        {
            get => _displayedImage;
            set => SetProperty(ref _displayedImage, value);
        }

        #endregion Values
    }
}