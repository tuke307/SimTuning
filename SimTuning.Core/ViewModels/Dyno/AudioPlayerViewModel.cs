﻿// project=SimTuning.Core, file=AudioPlayerViewModel.cs, creation=2020:10:21 Copyright (c)
// 2021 tuke productions. All rights reserved.
using MediaManager;
using MediaManager.Library;
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Dyno
{
    /// <summary>
    /// AudioPlayerViewModel.
    /// </summary>
    public class AudioPlayerViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// AudioPlayerViewModel.
        /// </summary>
        /// <param name="logFactory"></param>
        /// <param name="navigationService"></param>
        /// <param name="mediaManager"></param>
        public AudioPlayerViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, IMediaManager mediaManager)
             : base(logFactory, navigationService)
        {
            MediaManager = mediaManager;

            this.PlayPauseCommand = new MvxAsyncCommand(() => this.PlayPause());
            this.SkipBackwardsCommand = new MvxAsyncCommand(() => this.MediaManager.StepBackward());
            this.SkipForwardCommand = new MvxAsyncCommand(() => this.MediaManager.StepForward());
            this.DragCompletedCommand = new MvxAsyncCommand(() =>
            {
                this.DragStarted = false;
                return this.MediaManager.SeekTo(TimeSpan.FromSeconds(this.Position));
            });
            this.DragStartedCommand = new MvxCommand(() => this.DragStarted = true);

            // MediaItem von Quee holen, falls bereits Play ausgelöst wurde
            Source = MediaManager.Queue.Current;

            if (Source != null)
            {
                TimeSpanPosition = MediaManager.Position;
                Position = MediaManager.Position.TotalSeconds;
                TimeSpanDuration = MediaManager.Duration;
                Duration = MediaManager.Duration.TotalSeconds;
            }

            MediaManager.MediaItemChanged += this.MediaManager_MediaItemChanged;
            MediaManager.PositionChanged += this.MediaManager_PositionChanged;
            MediaManager.StateChanged += this.MediaManager_StateChanged;
            MediaManager.MediaItemFailed += this.Current_MediaItemFailed;
            MediaManager.MediaItemFinished += this.Current_MediaItemFinished;
            MediaManager.BufferedChanged += this.Current_BufferingChanged;
        }

        #region Values

        protected readonly IMediaManager MediaManager;

        private bool _dragStarted = false;

        private double _duration = 100;

        private bool _isPlaying;
        private double _position = 0;

        private IMediaItem _source;

        private TimeSpan _timeSpanDuration = TimeSpan.Zero;

        private TimeSpan _timeSpanPosition = TimeSpan.Zero;

        /// <summary>
        /// Gets or sets the cut beginn command.
        /// </summary>
        /// <value>The cut beginn command.</value>
        public IMvxAsyncCommand CutBeginnCommand { get; set; }

        /// <summary>
        /// Gets or sets the pause command.
        /// </summary>
        /// <value>The pause command.</value>
        //public IMvxAsyncCommand PauseCommand { get; set; }
        /// <summary>
        /// Gets or sets the cut end command.
        /// </summary>
        /// <value>The cut end command.</value>
        public IMvxAsyncCommand CutEndCommand { get; set; }

        public IMvxAsyncCommand DragCompletedCommand { get; set; }

        public bool DragStarted
        {
            get => _dragStarted;
            set => SetProperty(ref _dragStarted, value);
        }

        public IMvxCommand DragStartedCommand { get; set; }

        public double Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        public bool IsPlaying
        {
            get => this.MediaManager.IsPlaying();
        }

        /// <summary>
        /// Gets or sets the play command.
        /// </summary>
        /// <value>The play command.</value>
        public IMvxAsyncCommand PlayPauseCommand { get; set; }

        public double Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        public IMvxAsyncCommand SkipBackwardsCommand { get; set; }

        public IMvxAsyncCommand SkipForwardCommand { get; set; }

        public IMediaItem Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        public TimeSpan TimeSpanDuration
        {
            get => this._timeSpanDuration;
            set => this.SetProperty(ref this._timeSpanDuration, value);
        }

        public TimeSpan TimeSpanPosition
        {
            get => this._timeSpanPosition;
            set => this.SetProperty(ref _timeSpanPosition, value);
        }

        /*= CrossMediaManager.Current*/

        #endregion Values

        #region Methods

        protected void Current_BufferingChanged(object sender, MediaManager.Playback.BufferedChangedEventArgs e)
        {
            Log.LogDebug($"Total buffered time is {e.Buffered};");
        }

        protected void Current_MediaItemFailed(object sender, MediaManager.Media.MediaItemFailedEventArgs e)
        {
            Log.LogDebug($"Media item failed: {e.MediaItem.Title}, Message: {e.Message}, Exception: {e.Exeption?.ToString()};");
        }

        protected void Current_MediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
            Log.LogDebug($"Media item finished: {e.MediaItem.Title};");
        }

        /// <summary>
        /// Cuts the beginn.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected virtual async Task CutBeginn()
        {
            await MediaManager.Stop().ConfigureAwait(true);
            this.MediaManager.Queue.Clear();
            //this.MediaManager.Dispose();

            this.TrimAudio(this.Position, 0);

            await PlayFileAsync().ConfigureAwait(true);
        }

        /// <summary>
        /// Cuts the end.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected virtual async Task CutEnd()
        {
            await MediaManager.Stop().ConfigureAwait(true);
            this.MediaManager.Queue.Clear();
            // this.MediaManager.Dispose();

            this.TrimAudio(0, Position);

            await PlayFileAsync().ConfigureAwait(true);
        }

        protected void MediaManager_MediaItemChanged(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
            this.Source = e.MediaItem;
        }

        protected void MediaManager_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            if (!this.DragStarted)
            {
                this.TimeSpanPosition = e.Position;
                this.Position = e.Position.TotalSeconds;
            }
            this.TimeSpanDuration = this.MediaManager.Duration;
            this.Duration = this.MediaManager.Duration.TotalSeconds;

            this.RaisePropertyChanged(() => this.IsPlaying);

            this.Log.LogDebug($"Current position is {e.Position};");
        }

        protected void MediaManager_StateChanged(object sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            Log.LogDebug($"Status changed: {System.Enum.GetName(typeof(MediaManager.Player.MediaPlayerState), e.State)};");
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        protected virtual async Task PlayFileAsync()
        {
            try
            {
                Source = await MediaManager.Play(SimTuning.Core.GeneralSettings.AudioAccelerationFilePath).ConfigureAwait(true);
            }
            catch (Exception exc)
            {
                this.Log.LogError("Fehler bei PlayFileAsync: ", exc);
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
            try
            {
                Stream cuttedFileStream = new MemoryStream();

                if (cutStart > 0)
                {
                    SimTuning.Core.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(cutStart), TimeSpan.FromSeconds(0), outStream: ref cuttedFileStream, inPath: SimTuning.Core.GeneralSettings.AudioAccelerationFilePath);
                }
                else if (cutEnd > 0)
                {
                    SimTuning.Core.Business.AudioUtils.TrimWavFile(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(cutEnd), outStream: ref cuttedFileStream, inPath: SimTuning.Core.GeneralSettings.AudioAccelerationFilePath);
                }

                // löscht alte Datei
                File.Delete(SimTuning.Core.GeneralSettings.AudioAccelerationFilePath);

                // neue gecuttete temp-Datei für alte Datei ersetzen
                using (var fileStream = File.Create(SimTuning.Core.GeneralSettings.AudioAccelerationFilePath))
                {
                    cuttedFileStream.Seek(0, SeekOrigin.Begin);
                    cuttedFileStream.CopyTo(fileStream);
                }
            }
            catch (Exception exc)
            {
                this.Log.LogError("Fehler bei TrimAudio: ", exc);
            }
        }

        private async Task PlayPause()
        {
            if (Source == null)
            {
                // abspielen zum ersten mal
                await PlayFileAsync().ConfigureAwait(true);
            }
            else
            {
                await MediaManager.PlayPause().ConfigureAwait(true);
            }

            await this.RaisePropertyChanged(() => this.IsPlaying).ConfigureAwait(true);
        }

        ///// <summary>
        ///// Gets the audio maximum. wenn 0 dann gibt es Fehler in xamarin anwendung.
        ///// </summary>
        ///// <value>The audio maximum.</value>
        //public double? AudioMaximum
        //{
        //    get => this.MediaManager.Duration.TotalMilliseconds == 0 ? 100 : this.MediaManager.Duration.TotalMilliseconds;
        //}

        ///// <summary>
        ///// Gets or sets the audio position.
        ///// </summary>
        ///// <value>The audio position.</value>
        //public double? AudioPosition
        //{
        //    get => this.MediaManager?.Position.Milliseconds ?? 0;
        //    set
        //    {
        //        this.MediaManager.SeekTo(TimeSpan.FromMilliseconds(value.Value));
        //    }
        //}

        #endregion Methods
    }
}