// project=SimTuning.Core, file=AudioViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Dyno
{
    using Data;
    using Data.Models;
    using MediaManager;
    using MediaManager.Library;
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

    /// <summary>
    /// Dyno-Audio-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class AudioViewModel : MvxNavigationViewModel
    {
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

            this.StopCommand = new MvxAsyncCommand(this.MediaManager.Stop);
            this.PauseCommand = new MvxAsyncCommand(this.MediaManager.Pause);
            this.PlayCommand = new MvxAsyncCommand(this.MediaManager.PlayPause);

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
            base.Prepare();
        }

        /// <summary>
        /// Reloads the DB-data.
        /// </summary>
        public void ReloadData(Models.MvxReloaderMessage mvxReloaderMessage = null)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    this.Dyno = db.Dyno.Single(d => d.Active == true); // .Include(v => v.Audio)
                }
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler beim Laden des Dyno-Datensatz: ", exc);
            }
        }

        /// <summary>
        /// Cuts the beginn.
        /// </summary>
        protected virtual async Task CutBeginn()
        {
            this.TrimAudio(this.AudioPosition.Value, 0);

            await this.OpenFileAsync();
        }

        /// <summary>
        /// Cuts the end.
        /// </summary>
        protected virtual async Task CutEnd()
        {
            this.TrimAudio(0, AudioPosition.Value);

            await this.OpenFileAsync();
        }

        /// <summary>
        /// Opens the file.
        /// TODO: stream anstatt uri
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected virtual async Task OpenFileAsync()
        {
            // initialisieren
            //var stream = File.OpenRead(SimTuning.Core.Constants.AudioFilePath);

            this.MediaManager.PositionChanged += this.Current_PositionChanged;
            await this.MediaManager.Play(SimTuning.Core.GeneralSettings.AudioFilePath).ConfigureAwait(true);
            this.StopCommand.Execute();

            //stream.Dispose();

            await this.RaisePropertyChanged(() => this.AudioMaximum).ConfigureAwait(true);
        }

        /// <summary>
        /// Opens the file dialog.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected virtual async Task OpenFileDialog(FileData fileData)
        {
            // wenn Datei ausgewählt
            if (SimTuning.Core.Business.AudioUtils.AudioCopy(fileData.FileName, fileData.GetStream()))
            {
                await this.OpenFileAsync().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Reloads the image audio spectrogram.
        /// </summary>
        /// <returns></returns>
        protected Stream ReloadImageAudioSpectrogram()
        {
            SKBitmap spec = audioLogic.GetSpectrogram(SimTuning.Core.GeneralSettings.AudioFilePath);
            Stream stream = SimTuning.Core.Business.Converts.SKBitmapToStream(spec);

            return stream;
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
                SimTuning.Core.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(cutStart), TimeSpan.FromSeconds(0), outStream: ref cuttedFileStream, inPath: SimTuning.Core.GeneralSettings.AudioFilePath);
            else if (cutEnd > 0)
                SimTuning.Core.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(cutEnd), outStream: ref cuttedFileStream, inPath: SimTuning.Core.GeneralSettings.AudioFilePath);

            //löscht alte Datei
            File.Delete(SimTuning.Core.GeneralSettings.AudioFilePath);

            //neue gecuttete temp-Datei für alte Datei ersetzen
            //File.Create(tempFile, SimTuning.Core.Constants.AudioFilePath);

            using (var fileStream = File.Create(SimTuning.Core.GeneralSettings.AudioFilePath))
            {
                cuttedFileStream.Seek(0, SeekOrigin.Begin);
                cuttedFileStream.CopyTo(fileStream);
            }
        }

        private void Current_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            this.Log.Debug($"Current position is {e.Position};");
            this.RaisePropertyChanged(() => this.AudioPosition);
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

        #region private

        protected readonly IMediaManager MediaManager = CrossMediaManager.Current;
        protected AudioLogic audioLogic;
        protected ResourceManager rm;
        private bool _badgeFileOpen;

        private DynoModel _dyno;

        #endregion private

        /// <summary>
        /// Gets the audio maximum.
        /// </summary>
        /// <value>The audio maximum.</value>
        public double? AudioMaximum
        {
            get => this.MediaManager.Duration.TotalSeconds;
        }

        /// <summary>
        /// Gets or sets the audio position.
        /// </summary>
        /// <value>The audio position.</value>
        public double? AudioPosition
        {
            get => this.MediaManager?.Position.TotalSeconds;
            set
            {
                if (MediaManager.MediaPlayer != null)
                {
                    MediaManager.SeekTo(TimeSpan.FromSeconds(value.Value));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [badge file open].
        /// </summary>
        /// <value><c>true</c> if [badge file open]; otherwise, <c>false</c>.</value>
        public bool BadgeFileOpen
        {
            get => _badgeFileOpen;
            set { SetProperty(ref _badgeFileOpen, value); }
        }

        /// <summary>
        /// Gets the buffered.
        /// </summary>
        /// <value>The buffered.</value>
        public int Buffered => Convert.ToInt32(MediaManager.Buffered.TotalSeconds);

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        public IMediaItem Current => MediaManager.Queue.Current;

        /// <summary>
        /// Gets the current subtitle.
        /// </summary>
        /// <value>The current subtitle.</value>
        public string CurrentSubtitle => Current.DisplaySubtitle;

        /// <summary>
        /// Gets the current title.
        /// </summary>
        /// <value>The current title.</value>
        public string CurrentTitle => Current.DisplayTitle;

        //public string TotalDuration => MediaManager.Duration.ToString(@"mm\:ss");
        //public string TotalPlayed => MediaManager.Position.ToString(@"mm\:ss");

        /// <summary> Gets or sets the dyno. <value>The dyno.</value>
        public DynoModel Dyno
        {
            get => _dyno;
            set { SetProperty(ref _dyno, value); }
        }

        /// <summary>
        /// Gets the floated position.
        /// </summary>
        /// <value>The floated position.</value>
        public float FloatedPosition => (float)AudioPosition / (float)AudioMaximum;

        #endregion Values
    }
}