using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.SimpleAudioPlayer;
using SkiaSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels.Dyno
{
    public class DynoAudioViewModel : SimTuning.ViewModels.Dyno.AudioViewModel
    {
        private ISimpleAudioPlayer player;

        public DynoAudioViewModel()
        {
            OpenFileCommand = new Command(async () => await OpenFileDialog());
            StopCommand = new Command(Stop);
            PauseCommand = new Command(Pause);
            PlayCommand = new Command(Play);
            CutBeginnCommand = new Command(async () => await CutBeginn());
            CutEndCommand = new Command(async () => await CutEnd());

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
            if (SimTuning.Business.AudioUtils.AudioCopy(fileData.FileName, fileData.GetStream()))
                OpenFile();

            if (player != null)
            {
                await ReloadImageAudioSpectrogram();

                BadgeFileOpen = true;
            }
        }

        protected override void Play(object parameter)
        {
            if (player != null)
            {
                player.Play();

                //Position aktualisieren
                Task t = Task.Run(() =>
                {
                    while (player.IsPlaying)
                    {
                        OnPropertyChanged("AudioPosition");
                    }
                });
            }
        }

        protected override void Pause(object parameter)
        {
            if (player != null)
                player.Pause();
        }

        protected override void Stop(object parameter)
        {
            if (player != null)
            {
                player.Stop();
                _AudioPosition = 0;
                OnPropertyChanged("AudioPosition");
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

        public ImageSource ImageAudioSpectrogram
        {
            get => Get<ImageSource>();
            private set => Set(value);
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

        //Für View
        public double? AudioPosition
        {
            get
            {
                return _AudioPosition;
            }
            set
            {
                Set(value);

                if (player != null)
                {
                    player.Seek(value.Value);
                    _AudioPosition = value;
                }
            }
        }

        //private gesetzte position
        private double? _AudioPosition
        {
            get
            {
                if (player != null)
                    return player.CurrentPosition;

                return null;
            }
            set => Set(value);
        }

        protected new async Task CutBeginn()
        {
            if (player != null)
            {
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

                await TrimAudio(_AudioPosition.Value, 0);

                OpenFile();

                OnPropertyChanged("AudioPosition");

                await loadingDialog.DismissAsync();

                await ReloadImageAudioSpectrogram();
            }
        }

        protected new async Task CutEnd()
        {
            if (player != null)
            {
                var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

                await TrimAudio(0, _AudioPosition.Value);

                OpenFile();

                OnPropertyChanged("AudioPosition");

                await loadingDialog.DismissAsync();

                await ReloadImageAudioSpectrogram();
            }
        }

        protected override void OpenFile()
        {
            //initialisieren
            var stream = File.OpenRead(SimTuning.Constants.AudioFilePath);
            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load(stream);

            Task t = Task.Run(() =>
            {
                Task.Delay(1000).Wait();
                OnPropertyChanged("AudioMaximum");
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