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

            //BadgeFileOpen = false;

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

        public BitmapSource ImageAudioFile
        {
            get => Get<BitmapSource>();
            set => Set(value);
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

        //privat gesetzte position
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
                mainWindowViewModel.LoadingAnimation = true;

                await TrimAudio(_AudioPosition.Value, 0);

                OpenFile();

                OnPropertyChanged("AudioPosition");

                mainWindowViewModel.LoadingAnimation = false;

                await ReloadImageAudioSpectrogram();
            }
        }

        protected new async Task CutEnd()
        {
            if (player != null)
            {
                mainWindowViewModel.LoadingAnimation = true;

                await TrimAudio(0, _AudioPosition.Value);

                OpenFile();

                OnPropertyChanged("AudioPosition");

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