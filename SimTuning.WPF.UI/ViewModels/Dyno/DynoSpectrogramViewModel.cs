// project=SimTuning.WPF.UI, file=DynoSpectrogramViewModel.cs, creation=2020:9:2 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    using MaterialDesignThemes.Wpf;
    using MediaManager;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using Plugin.FilePicker;
    using Plugin.FilePicker.Abstractions;
    using SimTuning.Core.Models;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Dialog;
    using System.Globalization;
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
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoSpectrogramViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger, IMediaManager mediaManager)
            : base(logProvider, navigationService, messenger, mediaManager)
        {
            this._token = messenger.Subscribe<MvxReloaderMessage>(this.ReloadData);

            // override Commands
            this.FilterPlotCommand = new MvxAsyncCommand(this.FilterPlot);
            this.RefreshPlotCommand = new MvxAsyncCommand(this.RefreshPlot);
            this.SpecificGraphCommand = new MvxAsyncCommand(this.SpecificGraph);
            this.RefreshSpectrogramCommand = new MvxAsyncCommand(this.ReloadImageAudioSpectrogram);

            this.OpenFileCommand = new MvxAsyncCommand(this.OpenFileDialog);

            // datensatz checken CheckDynoData();
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
        /// Filters the plot.
        /// </summary>
        protected new async Task FilterPlot()
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
        protected new async Task OpenFileDialog()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            FileData fileData = await CrossFilePicker.Current.PickFile(new string[] { "Zip Datei (*.zip)|*.zip" }).ConfigureAwait(true);

            // user canceled file picking
            if (fileData == null)
            {
                return;
            }

            string filepath = fileData.FilePath;
            fileData.Dispose();

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    await base.OpenFileDialog(filepath).ConfigureAwait(true);
                    args.Session.Close();
                });
            }).ConfigureAwait(true);

            await this.ReloadImageAudioSpectrogram().ConfigureAwait(true);
        }

        /// <summary>
        /// Refreshes the plot.
        /// </summary>
        protected new async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    await base.RefreshPlot().ConfigureAwait(true);
                    args.Session.Close();
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// Reloads the image audio spectrogram.
        /// </summary>
        protected new async Task ReloadImageAudioSpectrogram()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Stream stream = base.ReloadImageAudioSpectrogram();
                        PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                        this.DisplayedImage = decoder.Frames[0];
                    });
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        args.Session.Close();
                    });
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// Specifics the graph.
        /// </summary>
        protected new Task SpecificGraph()
        {
            return DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
             {
                 Task.Run(() =>
                 {
                     Application.Current.Dispatcher.Invoke(() =>
                 {
                     base.SpecificGraph();
                     args.Session.Close();
                 });
                 });
             });
        }

        /// <summary>
        /// Checks the dyno data.
        /// </summary>
        /// <returns></returns>
        private bool CheckDynoData()
        {
            if (this.Dyno == null)
            {
                Functions.ShowSnackbarDialog(rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion Methods

        #region Values

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