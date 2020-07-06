using MvvmCross.Commands;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.SimpleAudioPlayer;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoAudioViewModel : SimTuning.Core.ViewModels.Dyno.AudioViewModel
    {
        private ISimpleAudioPlayer player;
        private readonly ResourceManager rm;

        public DynoAudioViewModel()
        {
            OpenFileCommand = new MvxAsyncCommand(() => OpenFileDialog());
            StopCommand = new MvxCommand(Stop);
            PauseCommand = new MvxCommand(Pause);
            PlayCommand = new MvxCommand(Play);
            CutBeginnCommand = new MvxAsyncCommand(() => CutBeginn());
            CutEndCommand = new MvxAsyncCommand(() => CutEnd());

            rm = new ResourceManager("resources", Assembly.GetExecutingAssembly());
            //datensatz checken
            //CheckDynoData();
        }

        private async Task<bool> CheckDynoData()
        {
            if (Dyno == null)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture)).ConfigureAwait(false);
                return false;
            }
            else { return true; }
        }

        protected new async Task OpenFileDialog()
        {
            if (!CheckDynoData().Result)
                return;

            FileData fileData = await CrossFilePicker.Current.PickFile(new string[] { ".wav", ".mp3" }).ConfigureAwait(true);

            if (fileData == null)
                return; // user canceled file picking

            //wenn Datei ausgewählt
            if (SimTuning.Core.Business.AudioUtils.AudioCopy(fileData.FileName, fileData.GetStream()))
                OpenFile();

            if (player != null)
            {
                await ReloadImageAudioSpectrogram().ConfigureAwait(true);

                BadgeFileOpen = true;
            }
        }

        protected override void Play()
        {
            if (player != null)
            {
                player.Play();

                //Position aktualisieren
                Task t = Task.Run(() =>
                {
                    while (player.IsPlaying)
                    {
                        RaisePropertyChanged("AudioPosition");
                    }
                });
            }
        }

        protected override void Pause()
        {
            if (player != null)
                player.Pause();
        }

        protected override void Stop()
        {
            if (player != null)
            {
                player.Stop();
                _audioPosition = 0;
                RaisePropertyChanged("AudioPosition");
            }
        }

        /// <summary>
        /// Aktualisiert das Spectrogram-Bild der Audio Datei
        /// </summary>
        /// <returns></returns>
        protected new async Task ReloadImageAudioSpectrogram()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            Stream stream = base.ReloadImageAudioSpectrogram();
            ImageAudioSpectrogram = ImageSource.FromStream(() => stream);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        private ImageSource _imageAudioSpectrogram;

        public ImageSource ImageAudioSpectrogram
        {
            get => _imageAudioSpectrogram;
            private set => SetProperty(ref _imageAudioSpectrogram, value);
        }

        public double? AudioMaximum
        {
            get
            {
                if (player != null)
                    return player.Duration;
                else
                    return null;
            }
        }

        private double? _audioPosition;

        public double? AudioPosition
        {
            get => _audioPosition;
            set
            {
                SetProperty(ref _audioPosition, value);

                if (player != null)
                {
                    player.Seek(value.Value);
                    _audioPosition = value;
                }
            }
        }

        protected new async Task CutBeginn()
        {
            if (player != null)
            {
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

                await TrimAudio(_audioPosition.Value, 0).ConfigureAwait(true);

                OpenFile();

                await RaisePropertyChanged("AudioPosition").ConfigureAwait(false);

                await loadingDialog.DismissAsync().ConfigureAwait(false);

                await ReloadImageAudioSpectrogram().ConfigureAwait(true);
            }
        }

        protected new async Task CutEnd()
        {
            if (player != null)
            {
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

                await TrimAudio(0, _audioPosition.Value).ConfigureAwait(true);

                OpenFile();

                await RaisePropertyChanged("AudioPosition").ConfigureAwait(false);

                await loadingDialog.DismissAsync().ConfigureAwait(false);

                await ReloadImageAudioSpectrogram().ConfigureAwait(true);
            }
        }

        protected override void OpenFile()
        {
            //initialisieren
            var stream = File.OpenRead(SimTuning.Core.Constants.AudioFilePath);
            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load(stream);
            stream.Dispose();

            Task t = Task.Run(() =>
            {
                Task.Delay(1000).Wait();
                RaisePropertyChanged("AudioMaximum");
            });
        }

        protected new async Task TrimAudio(double cutStart, double cutEnd)
        {
            player.Stop();
            player.Dispose();
            player = null;

            await Task.Run(() => base.TrimAudio(cutStart, cutEnd)).ConfigureAwait(true);
        }
    }
}