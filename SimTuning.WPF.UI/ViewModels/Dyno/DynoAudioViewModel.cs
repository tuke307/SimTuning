// project=SimTuning.WPF.UI, file=DynoAudioViewModel.cs, creation=2020:7:30 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    using MaterialDesignThemes.Wpf;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using Plugin.FilePicker;
    using Plugin.FilePicker.Abstractions;
    using SimTuning.Core.Models;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Dialog;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// WPF-spezifisches Dyno-Audio-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.AudioViewModel" />
    public class DynoAudioViewModel : SimTuning.Core.ViewModels.Dyno.AudioViewModel
    {
        private readonly MvvmCross.Plugin.Messenger.MvxSubscriptionToken _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynoAudioViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoAudioViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, MvvmCross.Plugin.Messenger.IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
        {
            _token = messenger.Subscribe<MvxReloaderMessage>(this.ReloadData);

            //this.player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            // override commands
            this.OpenFileCommand = new MvxAsyncCommand(this.OpenFileDialog);
            this.CutBeginnCommand = new MvxAsyncCommand(this.CutBeginn);
            this.CutEndCommand = new MvxAsyncCommand(this.CutEnd);

            // datensatz checken CheckDynoData();
        }

        #region Values

        private BitmapSource _imageAudioFile;

        public BitmapSource ImageAudioFile
        {
            get => _imageAudioFile;
            set => SetProperty(ref _imageAudioFile, value);
        }

        #endregion Values

        #region Commands

        protected new async Task CutBeginn()
        {
            if (this.MediaManager.MediaPlayer == null)
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", async delegate (object sender, DialogOpenedEventArgs args)
            {
                await base.CutBeginn().ConfigureAwait(true);

                args.Session.Close();
            }).ConfigureAwait(true);

            await this.ReloadImageAudioSpectrogram().ConfigureAwait(true);
        }

        protected new async Task CutEnd()
        {
            if (this.MediaManager.MediaPlayer == null)
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", async delegate (object sender, DialogOpenedEventArgs args)
            {
                await base.CutEnd().ConfigureAwait(true);

                args.Session.Close();
            }).ConfigureAwait(true);

            await this.ReloadImageAudioSpectrogram().ConfigureAwait(true);
        }

        protected override void OpenFile()
        {
            // initialisieren
            var stream = File.OpenRead(SimTuning.Core.Constants.AudioFilePath);
            //MediaManager = CrossSimpleAudioMediaManager.Current;
            //MediaManager.Play(stream, SimTuning.Core.Constants.AudioFile);

            //stream.Dispose();

            base.OpenFile();
        }

        protected new async Task OpenFileDialog()
        {
            if (!CheckDynoDataAsync().Result)
            {
                return;
            }

            FileData fileData = await CrossFilePicker.Current.PickFile(new string[] { "WAVE Audio (*.wav)|*.wav", "MP3 Audio (*.mp3)|*.mp3" }).ConfigureAwait(true);

            if (fileData == null)
            {
                return;
            }

            await base.OpenFileDialog(fileData).ConfigureAwait(true);

            //if (MediaManager.MediaPlayer != null)
            //{
            await this.ReloadImageAudioSpectrogram();

            BadgeFileOpen = true;
            //}
        }

        protected new Task ReloadImageAudioSpectrogram()
        {
            return DialogHost.Show(new DialogLoadingView(), "DialogLoading", delegate (object sender, DialogOpenedEventArgs args)
             {
                 Stream stream = base.ReloadImageAudioSpectrogram();
                 PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                 ImageAudioFile = decoder.Frames[0];

                 args.Session.Close();
             });
        }

        private async Task<bool> CheckDynoDataAsync()
        {
            if (this.Dyno == null)
            {
                Functions.ShowSnackbarDialog(this.rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }
            else { return true; }
        }

        #endregion Commands
    }
}