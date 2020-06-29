using Microsoft.Win32;
using Plugin.SimpleAudioPlayer;
using SimTuning.windows.Business;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimTuning.windows.ViewModels.Dyno
{
    public class DynoAudioViewModel : SimTuning.ViewModels.Dyno.AudioViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        private ISimpleAudioPlayer player;

        public DynoAudioViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel; //LoadingScreen

            OpenFileCommand = new AsyncCommand(async () => await OpenFileDialog());
            StopCommand = new ActionCommand(Stop);
            PauseCommand = new ActionCommand(Pause);
            PlayCommand = new ActionCommand(Play);
            CutBeginnCommand = new AsyncCommand(async () => await CutBeginn());
            CutEndCommand = new AsyncCommand(async () => await CutEnd());

            //datensatz checken
            //CheckDynoData();
        }

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Bitte Datensatz auswählen um fortzufahren!");
                return false;
            }
            else { return true; }
        }

        protected new async Task OpenFileDialog()
        {
            if (!CheckDynoData())
                return;

            //Windows Auswahlfenster
            var filePath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Datei (*.mp3;*.wav)|*.mp3;*.wav;";

            //wenn Datei ausgewählt
            if ((bool)openFileDialog.ShowDialog())
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;

                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                if (SimTuning.Business.AudioUtils.AudioCopy(Path.GetFileName(filePath), fileStream))
                    OpenFile();

                if (player != null)
                {
                    await ReloadImageAudioSpectrogram();

                    BadgeFileOpen = true;
                }
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
                        //AudioPosition = player.CurrentPosition;
                        RaisePropertyChanged("AudioPosition");
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
                AudioPosition = 0;
                RaisePropertyChanged("AudioPosition");
            }
        }

        protected new async Task ReloadImageAudioSpectrogram()
        {
            mainWindowViewModel.LoadingAnimation = true;

            await Task.Run(() =>
            {
                Stream stream = base.ReloadImageAudioSpectrogram();
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                ImageAudioFile = decoder.Frames[0];
            }
            );

            mainWindowViewModel.LoadingAnimation = false;
        }

        private BitmapSource _imageAudioFile;

        public BitmapSource ImageAudioFile
        {
            get => _imageAudioFile;
            set => SetProperty(ref _imageAudioFile, value);
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
                mainWindowViewModel.LoadingAnimation = true;

                await TrimAudio(AudioPosition.Value, 0);

                OpenFile();

                RaisePropertyChanged("AudioPosition");

                mainWindowViewModel.LoadingAnimation = false;

                await ReloadImageAudioSpectrogram();
            }
        }

        protected new async Task CutEnd()
        {
            if (player != null)
            {
                mainWindowViewModel.LoadingAnimation = true;

                await TrimAudio(0, AudioPosition.Value);

                OpenFile();

                RaisePropertyChanged("AudioPosition");

                mainWindowViewModel.LoadingAnimation = false;

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