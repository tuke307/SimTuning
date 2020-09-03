// project=SimTuning.Core, file=AudioViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
using Data;
using Data.Models;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Playback;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.FilePicker.Abstractions;
using SimTuning.Core.ModuleLogic;
using SkiaSharp;
using System;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Dyno
{
    /// <summary>
    /// Dyno-Audio-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class AudioViewModel : MvxNavigationViewModel
    {
        public readonly IMediaManager MediaManager;
        protected AudioLogic audioLogic;

        protected ResourceManager rm;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AudioViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, MvvmCross.Plugin.Messenger.IMvxMessenger messenger)
            : base(logProvider, navigationService)
        {
            this.audioLogic = new AudioLogic();
            this.BadgeFileOpen = false;

            this.StopCommand = new MvxAsyncCommand(this.StopAsync);
            this.PauseCommand = new MvxAsyncCommand(this.PauseAsync);
            this.PlayCommand = new MvxAsyncCommand(this.PlayAsync);

            // this.MediaManager.PositionChanged += Current_PositionChanged;
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.ReloadData();

            // messages
            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        /// <summary>
        /// Reloads the DB-data.
        /// </summary>
        public void ReloadData(Models.MvxReloaderMessage mvxReloaderMessage = null)
        {
            using (var db = new DatabaseContext())
            {
                try
                {
                    Dyno = db.Dyno.Where(d => d.Active == true)/*.Include(v => v.Audio)*/.First();
                }
                catch { }
            }
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

        protected virtual void OpenFile()
        {
            ////initialisieren
            //var stream = File.OpenRead(SimTuning.Core.Constants.AudioFilePath);
            ////MediaManager = CrossSimpleAudioMediaManager.Current;
            //MediaManager.Play(stream, SimTuning.Core.Constants.AudioFile);
            //stream.Dispose();

            //this.MediaManager.PositionChanged += Current_PositionChanged;
            //RaisePropertyChanged(() => AudioMaximum);
            //Task t = Task.Run(() =>
            //{
            //    Task.Delay(1000).Wait();
            //    RaisePropertyChanged(() => AudioMaximum);
            //});
        }

        /// <summary>
        /// Opens the file dialog.
        /// </summary>
        protected virtual async Task OpenFileDialog(FileData fileData)
        {
            //wenn Datei ausgewählt
            if (SimTuning.Core.Business.AudioUtils.AudioCopy(fileData.FileName, fileData.GetStream()))
                OpenFile();
        }

        protected virtual async Task PauseAsync()
        {
            if (MediaManager != null)
                await MediaManager.Pause();
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

        protected Stream ReloadImageAudioSpectrogram()
        {
            SKBitmap spec = audioLogic.GetSpectrogram(SimTuning.Core.Constants.AudioFilePath);
            Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(spec);

            return stream;
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

        /// <summary>
        /// Schneidet die Audio Datei zurecht speichert den geschnittenen Schnipsel in
        /// einem Stream und überschreibt diese dann
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

        private void Current_BufferingChanged(object sender, BufferedChangedEventArgs e)
        {
            Log.Debug($"Total buffered time is {e.Buffered};");
        }

        private void Current_MediaItemChanged(object sender, MediaItemEventArgs e)
        {
            Log.Debug($"Media item changed, new item title: {e.MediaItem.Title};");
        }

        private void Current_MediaItemFailed(object sender, MediaItemFailedEventArgs e)
        {
            Log.Debug($"Media item failed: {e.MediaItem.Title}, Message: {e.Message}, Exception: {e.Exeption?.ToString()};");
        }

        private void Current_MediaItemFinished(object sender, MediaItemEventArgs e)
        {
            Log.Debug($"Media item finished: {e.MediaItem.Title};");
        }

        private void Current_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            Log.Debug($"Current position is {e.Position};");
            RaisePropertyChanged(() => AudioPosition);
        }

        private void Current_StatusChanged(object sender, StateChangedEventArgs e)
        {
            //Log.Debug($"Status changed: {System.Enum.GetName(typeof(MediaManager), e.State)};");
        }

        #endregion Methods

        #region Values

        #region Commands

        /// <summary>
        /// Gets or sets the cut beginn command.
        /// </summary>
        /// <value>The cut beginn command.</value>
        public IMvxAsyncCommand CutBeginnCommand { get; set; }

        /// <summary>
        /// Gets or sets the cut end command.
        /// </summary>
        /// <value>The cut end command.</value>
        public IMvxAsyncCommand CutEndCommand { get; set; }

        /// <summary>
        /// Gets or sets the open file command.
        /// </summary>
        /// <value>The open file command.</value>
        public IMvxAsyncCommand OpenFileCommand { get; set; }

        /// <summary>
        /// Gets or sets the pause command.
        /// </summary>
        /// <value>The pause command.</value>
        public IMvxAsyncCommand PauseCommand { get; set; }

        /// <summary>
        /// Gets or sets the play command.
        /// </summary>
        /// <value>The play command.</value>
        public IMvxAsyncCommand PlayCommand { get; set; }

        /// <summary>
        /// Gets or sets the stop command.
        /// </summary>
        /// <value>The stop command.</value>
        public IMvxAsyncCommand StopCommand { get; set; }

        #endregion Commands

        private bool _badgeFileOpen;

        private DynoModel _dyno;

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

        public bool BadgeFileOpen
        {
            get => _badgeFileOpen;
            set { SetProperty(ref _badgeFileOpen, value); }
        }

        public int Buffered => Convert.ToInt32(MediaManager.Buffered.TotalSeconds);

        public IMediaItem Current => MediaManager.Queue.Current;

        public string CurrentSubtitle => Current.DisplaySubtitle;

        public string CurrentTitle => Current.DisplayTitle;

        //public string TotalDuration => MediaManager.Duration.ToString(@"mm\:ss");
        //public string TotalPlayed => MediaManager.Position.ToString(@"mm\:ss");
        public DynoModel Dyno
        {
            get => _dyno;
            set { SetProperty(ref _dyno, value); }
        }

        public float FloatedPosition => (float)AudioPosition / (float)AudioMaximum;

        #endregion Values
    }
}