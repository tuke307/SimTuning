using System;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Data;
using Data.Models;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Playback;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.FilePicker.Abstractions;
using Plugin.SimpleAudioPlayer;
using SimTuning.Core.ModuleLogic;
using SkiaSharp;

namespace SimTuning.Core.ViewModels.Dyno
{
    public class AudioViewModel : MvxNavigationViewModel
    {
        protected AudioLogic audioLogic;
        public readonly IMediaManager MediaManager;
        protected ResourceManager rm;
        protected ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

        public AudioViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            audioLogic = new AudioLogic();
            BadgeFileOpen = false;

            StopCommand = new MvxAsyncCommand(StopAsync);
            PauseCommand = new MvxAsyncCommand(PauseAsync);
            PlayCommand = new MvxAsyncCommand(PlayAsync);

            //this.MediaManager.PositionChanged += Current_PositionChanged;
        }

        public IMvxAsyncCommand OpenFileCommand { get; set; }
        public IMvxAsyncCommand CutBeginnCommand { get; set; }
        public IMvxAsyncCommand CutEndCommand { get; set; }
        public IMvxAsyncCommand StopCommand { get; set; }
        public IMvxAsyncCommand PauseCommand { get; set; }
        public IMvxAsyncCommand PlayCommand { get; set; }

        /// <summary>
        /// Prepares this instance.
        /// called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    Dyno = db.Dyno.Where(d => d.Active == true)/*.Include(v => v.Audio)*/.First();
                }
                catch { }
            }

            //messages
            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            return base.Initialize();
        }

        #region Commands

        /// <summary>
        /// Opens the file dialog.
        /// </summary>
        protected virtual async Task OpenFileDialog(FileData fileData)
        {
            //wenn Datei ausgewählt
            if (SimTuning.Core.Business.AudioUtils.AudioCopy(fileData.FileName, fileData.GetStream()))
                OpenFile();
        }

        protected virtual async Task PlayAsync()
        {
            //if (MediaManager != null)
            //{
            await MediaManager.Play();

            //Position aktualisieren
            //Task t = Task.Run(() =>
            //{
            //    while (MediaManager.IsPlaying())
            //    {
            //        //AudioPosition = MediaManager.CurrentPosition;
            //        RaisePropertyChanged("AudioPosition");
            //    }
            //});
            //}
        }

        protected virtual async Task PauseAsync()
        {
            if (MediaManager != null)
                await MediaManager.Pause();
        }

        protected virtual async Task StopAsync()
        {
            if (MediaManager.MediaPlayer != null)
            {
                await MediaManager.Stop();
                //AudioPosition = 0;
                //RaisePropertyChanged("AudioPosition");
            }
        }

        protected virtual void OpenFile()
        {
            ////initialisieren
            //var stream = File.OpenRead(SimTuning.Core.Constants.AudioFilePath);
            ////MediaManager = CrossSimpleAudioMediaManager.Current;
            //MediaManager.Play(stream, SimTuning.Core.Constants.AudioFile);
            //stream.Dispose();

            this.MediaManager.PositionChanged += Current_PositionChanged;
            RaisePropertyChanged(() => AudioMaximum);
            //Task t = Task.Run(() =>
            //{
            //    Task.Delay(1000).Wait();
            //    RaisePropertyChanged(() => AudioMaximum);
            //});
        }

        protected virtual async Task CutBeginn()
        {
            TrimAudio(AudioPosition.Value, 0);

            OpenFile();

            await RaisePropertyChanged("AudioPosition");
        }

        protected virtual async Task CutEnd()
        {
            TrimAudio(0, AudioPosition.Value);

            OpenFile();

            await RaisePropertyChanged("AudioPosition");
        }

        protected Stream ReloadImageAudioSpectrogram()
        {
            SKBitmap spec = audioLogic.GetSpectrogram(SimTuning.Core.Constants.AudioFilePath);
            Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(spec);

            return stream;
        }

        /// <summary>
        /// Schneidet die Audio Datei zurecht
        /// speichert den geschnittenen Schnipsel in einem Stream und überschreibt diese dann
        /// </summary>
        /// <param name="cutStart">The cut start.</param>
        /// <param name="cutEnd">The cut end.</param>
        protected virtual void TrimAudio(double cutStart, double cutEnd)
        {
            MediaManager.Stop();
            MediaManager.Dispose();

            //string tempFile = Path.Combine(SimTuning.Core.Constants.FileDirectory, "temp.wav");
            Stream cuttedFileStream = new MemoryStream();
            //FileStream fsSource = new FileStream(SimTuning.Core.Constants.AudioFilePath, FileMode.Open, FileAccess.Read);

            if (cutStart > 0)
                SimTuning.Core.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(cutStart), TimeSpan.FromSeconds(0), outStream: ref cuttedFileStream, inPath: SimTuning.Core.Constants.AudioFilePath);
            else if (cutEnd > 0)
                SimTuning.Core.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(cutEnd), outStream: ref cuttedFileStream, inPath: SimTuning.Core.Constants.AudioFilePath);

            //löscht alte Datei
            File.Delete(SimTuning.Core.Constants.AudioFilePath);

            //neue gecuttete temp-Datei für alte Datei ersetzen
            //File.Create(tempFile, SimTuning.Core.Constants.AudioFilePath);

            using (var fileStream = File.Create(SimTuning.Core.Constants.AudioFilePath))
            {
                cuttedFileStream.Seek(0, SeekOrigin.Begin);
                cuttedFileStream.CopyTo(fileStream);
            }
        }

        private void Current_MediaItemFailed(object sender, MediaItemFailedEventArgs e)
        {
            Log.Debug($"Media item failed: {e.MediaItem.Title}, Message: {e.Message}, Exception: {e.Exeption?.ToString()};");
        }

        private void Current_MediaItemFinished(object sender, MediaItemEventArgs e)
        {
            Log.Debug($"Media item finished: {e.MediaItem.Title};");
        }

        private void Current_MediaItemChanged(object sender, MediaItemEventArgs e)
        {
            Log.Debug($"Media item changed, new item title: {e.MediaItem.Title};");
        }

        private void Current_StatusChanged(object sender, StateChangedEventArgs e)
        {
            //Log.Debug($"Status changed: {System.Enum.GetName(typeof(MediaManager), e.State)};");
        }

        private void Current_BufferingChanged(object sender, BufferedChangedEventArgs e)
        {
            Log.Debug($"Total buffered time is {e.Buffered};");
        }

        private void Current_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            Log.Debug($"Current position is {e.Position};");
            RaisePropertyChanged(() => AudioPosition);
        }

        #endregion Commands

        #region Values

        public IMediaItem Current => MediaManager.Queue.Current;

        public string CurrentTitle => Current.DisplayTitle;
        public string CurrentSubtitle => Current.DisplaySubtitle;

        public int Buffered => Convert.ToInt32(MediaManager.Buffered.TotalSeconds);

        public float FloatedPosition => (float)AudioPosition / (float)AudioMaximum;

        //public string TotalDuration => MediaManager.Duration.ToString(@"mm\:ss");
        //public string TotalPlayed => MediaManager.Position.ToString(@"mm\:ss");

        private DynoModel _dyno;

        public DynoModel Dyno
        {
            get => _dyno;
            set { SetProperty(ref _dyno, value); }
        }

        private bool _badgeFileOpen;

        public bool BadgeFileOpen
        {
            get => _badgeFileOpen;
            set { SetProperty(ref _badgeFileOpen, value); }
        }

        public double? AudioMaximum
        {
            get
            {
                if (MediaManager != null)
                    return MediaManager.Duration.TotalSeconds;
                else
                    return null;
            }
        }

        public double? AudioPosition
        {
            get
            {
                if (MediaManager != null)
                    return MediaManager.Position.TotalSeconds;
                else
                    return null;
            }
            set
            {
                if (MediaManager.MediaPlayer != null)
                {
                    MediaManager.SeekTo(TimeSpan.FromSeconds(value.Value));
                }
            }
        }

        #endregion Values
    }
}