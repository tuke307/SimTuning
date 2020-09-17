namespace SimTuning.Core.ViewModels.Dyno
{
    using Data;
    using Data.Models;
    using MvvmCross.Base;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Location;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using Plugin.AudioRecorder;
    using SimTuning.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
    using System.Threading.Tasks;

    /// <summary>
    /// RuntimeViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class RuntimeViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="locationWatcher">The location watcher.</param>
        /// <param name="messenger">The messenger.</param>
        public RuntimeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxLocationWatcher locationWatcher, IMvxMessenger messenger)
                                    : base(logProvider, navigationService)
        {
            this._token = messenger.Subscribe<MvxReloaderMessage>(this.ReloadData);

            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            this._locationWatcher = locationWatcher;

            this.StartAccelerationCommand = new MvxAsyncCommand(this.StartAcceleration);
            this.ResetAccelerationCommand = new MvxAsyncCommand(this.ResetAcceleration);
            this.StopAccelerationCommand = new MvxAsyncCommand(this.StopAcceleration);

            this.recorder = new AudioRecorderService();
            this.recorder.FilePath = GeneralSettings.AudioFilePath;
            this.recorder.PreferredSampleRate = 44100;
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.ReloadData();

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
        /// Reloads the data.
        /// </summary>
        /// <param name="mvxReloaderMessage">The MVX reloader message.</param>
        public virtual void ReloadData(Models.MvxReloaderMessage mvxReloaderMessage = null)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    this.Dyno = db.Dyno.Single(d => d.Active == true);
                }

                this.RaisePropertyChanged(() => this.EndAcceleration);
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler beim Laden des Dyno-Datensatz: ", exc);
            }
        }

        /// <summary>
        /// Resets the acceleration.
        /// </summary>
        protected virtual async Task ResetAcceleration()
        {
            await this.StopAcceleration().ConfigureAwait(true);
            await this.StopRecording().ConfigureAwait(true);
        }

        /// <summary>
        /// Starts the acceleration.
        /// </summary>
        protected virtual async Task StartAcceleration()
        {
            await this.StartRecording().ConfigureAwait(true);
            await this.StartTracking().ConfigureAwait(true);

            this.Dyno.Location = new List<LocationModel>();
        }

        /// <summary>
        /// Stops the acceleration.
        /// </summary>
        protected virtual async Task StopAcceleration()
        {
            this.StopTracking();
            await this.StopRecording().ConfigureAwait(true);

            using (var db = new Data.DatabaseContext())
            {
                db.Dyno.Update(this.Dyno);

                await db.SaveChangesAsync().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Called when [location updated].
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnLocationUpdated(MvxGeoLocation obj)
        {
            this.LastLocation = obj;

            Task.Run(async () =>
            {
                var locationModel = new LocationModel()
                {
                    Dyno = this.Dyno,
                    Latitude = obj.Coordinates.Longitude,
                    Longitude = obj.Coordinates.Longitude,
                    Accuracy = obj.Coordinates.Longitude,
                    Altitude = obj.Coordinates.Longitude,
                    AltitudeAccuracy = obj.Coordinates.Longitude,
                    Heading = obj.Coordinates.Longitude,
                    HeadingAccuracy = obj.Coordinates.Longitude,
                    Speed = obj.Coordinates.Longitude,
                };

                using (var db = new Data.DatabaseContext())
                {
                    db.Location.Add(locationModel);

                    await db.SaveChangesAsync().ConfigureAwait(true);
                }
            });

            this.RaisePropertyChanged(() => this.Speed);
            this.RaisePropertyChanged(() => this.Timer);

            if ((int)this.Speed == this.EndAcceleration)
            {
                this.CurrentState = secondState;
                this.BackgroundColor = blue;
            }
        }

        /// <summary>
        /// Called when [location update error].
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnLocationUpdateError(MvxLocationError obj)
        {
            this.Log.Warn($"Location Error: {obj.Code} {obj.ToString()}");
        }

        /// <summary>
        /// Starts the recording.
        /// </summary>
        private async Task StartRecording()
        {
            if (this.recorder.IsRecording)
            {
                await this.StopRecording().ConfigureAwait(true);
            }

            // start recording audio
            var audioRecordTask = await this.recorder.StartRecording().ConfigureAwait(true);

            await audioRecordTask.ConfigureAwait(true);
        }

        /// <summary>
        /// Starts the tracking.
        /// </summary>
        private async Task StartTracking()
        {
            if (this._locationWatcher.Started)
            {
                await this.StopAcceleration().ConfigureAwait(true);
            }

            var options = new MvxLocationOptions
            {
                Accuracy = MvxLocationAccuracy.Fine,
                TrackingMode = MvxLocationTrackingMode.Foreground,
                TimeBetweenUpdates = TimeSpan.Zero,
                MovementThresholdInM = 0,
            };

            this._locationWatcher.Start(options, this.OnLocationUpdated, this.OnLocationUpdateError);

            this.CurrentState = firstState;
            this.BackgroundColor = green;
        }

        /// <summary>
        /// Stops the recording.
        /// </summary>
        private async Task StopRecording()
        {
            await this.recorder.StopRecording().ConfigureAwait(true);
        }

        /// <summary>
        /// Stops the tracking.
        /// </summary>
        private void StopTracking()
        {
            this._locationWatcher.Stop();
        }

        #endregion Methods

        #region Values

        #region Commands

        /// <summary>
        /// Gets or sets the close command.
        /// </summary>
        /// <value>The close command.</value>
        public IMvxAsyncCommand CloseCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the reset tracking command.
        /// </summary>
        /// <value>The reset tracking command.</value>
        public MvxAsyncCommand ResetAccelerationCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the show audio command.
        /// </summary>
        /// <value>The show audio command.</value>
        public IMvxAsyncCommand ShowAudioCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the start tracking command.
        /// </summary>
        /// <value>The start tracking command.</value>
        public MvxAsyncCommand StartAccelerationCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the stop acceleration command.
        /// </summary>
        /// <value>The stop acceleration command.</value>
        public MvxAsyncCommand StopAccelerationCommand { get; protected set; }

        #endregion Commands

        protected readonly ResourceManager rm;
        private const string firstState = "Vollgas";

        private const string secondState = "Ausrollen";

        private static System.Drawing.Color blue = System.Drawing.Color.SkyBlue;

        private static System.Drawing.Color green = System.Drawing.Color.DarkSeaGreen;

        private readonly IMvxLocationWatcher _locationWatcher;

        private readonly MvxSubscriptionToken _token;
        private System.Drawing.Color _backgroundColor;

        private string _currentState;

        private DynoModel _dyno;

        private int _endAcceleration;

        private MvxGeoLocation _lastLocation;

        private AudioRecorderService recorder;

        public System.Drawing.Color BackgroundColor
        {
            get => this._backgroundColor;
            set => this.SetProperty(ref this._backgroundColor, value);
        }

        /// <summary>
        /// Gets or sets the state of the current.
        /// </summary>
        /// <value>The state of the current.</value>
        public string CurrentState
        {
            get => _currentState;
            set => SetProperty(ref _currentState, value);
        }

        /// <summary>
        /// Gets or sets the dyno.
        /// </summary>
        /// <value>The dyno.</value>
        public DynoModel Dyno
        {
            get => _dyno;
            set => SetProperty(ref _dyno, value);
        }

        /// <summary>
        /// Gets or sets the end acceleration.
        /// </summary>
        /// <value>The end acceleration.</value>
        public int? EndAcceleration
        {
            get => Dyno?.EndAcceleration;
        }

        /// <summary>
        /// Gets or sets the last location.
        /// </summary>
        /// <value>The last location.</value>
        public MvxGeoLocation LastLocation
        {
            get => this._lastLocation;
            set => this.SetProperty(ref this._lastLocation, value);
        }

        public double? Speed
        {
            get => this.LastLocation?.Coordinates?.Speed ?? 0.00;
        }

        public int? Timer
        {
            get => this.LastLocation?.Timestamp.Millisecond ?? 0;
        }

        #endregion Values
    }
}