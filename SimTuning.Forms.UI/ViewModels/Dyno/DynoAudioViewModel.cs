using MvvmCross.Commands;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoAudioViewModel : SimTuning.Core.ViewModels.Dyno.AudioViewModel
    {
        private ISimpleAudioPlayer player;

        public DynoAudioViewModel()
        {
            OpenFileCommand = new MvxAsyncCommand(() => OpenFileDialog());
            StopCommand = new MvxCommand(Stop);
            PauseCommand = new MvxCommand(Pause);
            PlayCommand = new MvxCommand(Play);
            CutBeginnCommand = new MvxAsyncCommand(() => CutBeginn());
            CutEndCommand = new MvxAsyncCommand(() => CutEnd());

            //datensatz checken
            //CheckDynoData();
        }

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync("Bitte Datensatz auswählen um fortzufahren!"));
                return false;
            }
            else { return true; }
        }

        protected new async Task OpenFileDialog()
        {
            if (!CheckDynoData())
                return;

            FileData fileData = await CrossFilePicker.Current.PickFile(new string[] { ".wav", ".mp3" });

            if (fileData == null)
                return; // user canceled file picking

            //wenn Datei ausgewählt
            if (SimTuning.Core.Business.AudioUtils.AudioCopy(fileData.FileName, fileData.GetStream()))
                OpenFile();

            if (player != null)
            {
                await ReloadImageAudioSpectrogram();

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
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

            await Task.Run(() =>
            {
                Stream stream = base.ReloadImageAudioSpectrogram();
                ImageAudioSpectrogram = ImageSource.FromStream(() => stream);
            }
            );

            await loadingDialog.DismissAsync();
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
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

                await TrimAudio(_audioPosition.Value, 0);

                OpenFile();

                RaisePropertyChanged("AudioPosition");

                await loadingDialog.DismissAsync();

                await ReloadImageAudioSpectrogram();
            }
        }

        protected new async Task CutEnd()
        {
            if (player != null)
            {
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

                await TrimAudio(0, _audioPosition.Value);

                OpenFile();

                RaisePropertyChanged("AudioPosition");

                await loadingDialog.DismissAsync();

                await ReloadImageAudioSpectrogram();
            }
        }

        protected override void OpenFile()
        {
            //initialisieren
            var stream = File.OpenRead(SimTuning.Core.Constants.AudioFilePath);
            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load(stream);

            Task t = Task.Run(() =>
            {
                Task.Delay(1000).Wait();
                RaisePropertyChanged("AudioMaximum");
            });
        }

        protected new async Task TrimAudio(double cut_start, double cut_end)
        {
            player.Stop();
            player.Dispose();
            player = null;

            await Task.Run(() => base.TrimAudio(cut_start, cut_end));
        }
    }
}