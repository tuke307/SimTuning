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
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Threading;
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

            // Commands
            this.StartAccelerationCommand = new MvxAsyncCommand(this.StartBeschleunigung);
            this.ResetAccelerationCommand = new MvxAsyncCommand(this.ResetAcceleration);
            this.StopAccelerationCommand = new MvxAsyncCommand(this.StopAcceleration);

            // Recorder
            this.recorder = new AudioRecorderService();
            this.recorder.FilePath = GeneralSettings.AudioFilePath;
            this.recorder.PreferredSampleRate = 44100;

            // Visibility
            this.StopAccelerationButtonVis = false;
            this.ShowAudioButtonVis = false;
            this.TimerVis = false;
            this.CountdownVis = false;

            // Anfahren
            this.CurrentState = preState;
            this.StartAccelerationButtonVis = true;
        }

        #region Methods

        private System.Timers.Timer _timer;

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
            try
            {
                await this.StopAcceleration().ConfigureAwait(true);
                await this.StopRecording().ConfigureAwait(true);

                File.Delete(this.recorder.FilePath);
                this.PageBackColor = System.Drawing.Color.White;
                this.SpeedBackColor = System.Drawing.Color.White;
                this.CurrentState = preState;
                this.StartAccelerationButtonVis = true;
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler beim Laden des Dyno-Datensatz: ", exc);
            }
        }

        /// <summary>
        /// Starts the ausrollen.
        /// </summary>
        protected virtual async Task StartAusrollen()
        {
            try
            {
                await this.StartRecording().ConfigureAwait(true);
                await this.StartTracking().ConfigureAwait(true);

                this.Dyno.Ausrollen = new List<AusrollenModel>();

                this.CurrentState = secondState;
                this.PageBackColor = deepSkyBlue;
                this.SpeedBackColor = skyBlue;

                this.TimerVis = true;
                _timer = new System.Timers.Timer();
                //Trigger event every second
                _timer.Interval = 100;
                _timer.Elapsed += OnTimedEvent;

                //count down 5000 ms
                Countdown = 5000;

                _timer.Enabled = true;
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler beim Laden des Dyno-Datensatz: ", exc);
            }
        }

        /// <summary>
        /// Starts the acceleration.
        /// </summary>
        protected virtual async Task StartBeschleunigung()
        {
            try
            {
                await this.StartRecording().ConfigureAwait(true);
                await this.StartTracking().ConfigureAwait(true);

                this.Dyno.Beschleunigung = new List<BeschleunigungModel>();

                this.CurrentState = firstState;
                this.PageBackColor = darkSeaGreen;
                this.SpeedBackColor = seaGreen;

                this.CountdownVis = true;
                _timer = new System.Timers.Timer();
                //Trigger event every second
                _timer.Interval = 100;
                _timer.Elapsed += OnTimedEvent;

                //count down 5000 ms
                Countdown = 5000;

                _timer.Enabled = true;
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler beim Laden des Dyno-Datensatz: ", exc);
            }
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
        /// TODO: bei keiner Veränderung der Maximalgeschwindigkeit StartAusrollen() beginnen.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnLocationUpdated(MvxGeoLocation obj)
        {
            this.LastLocation = obj;

            Task.Run(async () =>
            {
                var locationModel = new BeschleunigungModel()
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
                    db.Beschleunigung.Add(locationModel);

                    await db.SaveChangesAsync().ConfigureAwait(true);
                }
            });

            this.RaisePropertyChanged(() => this.Speed);
            this.RaisePropertyChanged(() => this.Timer);
        }

        /// <summary>
        /// Called when [location update error].
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnLocationUpdateError(MvxLocationError obj)
        {
            this.Log.Warn($"Location Error: {obj.Code} {obj.ToString()}");
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Timer abziehen
            Countdown -= 100;

            if (Countdown == 0)
            {
                _timer.Stop();
                _timer.Dispose();
                this.CountdownVis = false;

                if (this.StartAccelerationButtonVis)
                {
                    this.StartAccelerationButtonVis = false;
                }
            }
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
        private const string preState = "Anfahren";
        private const string secondState = "Ausrollen";

        private static System.Drawing.Color darkSeaGreen = System.Drawing.Color.DarkSeaGreen;
        private static System.Drawing.Color deepSkyBlue = System.Drawing.Color.DeepSkyBlue;
        private static System.Drawing.Color seaGreen = System.Drawing.Color.SeaGreen;
        private static System.Drawing.Color skyBlue = System.Drawing.Color.SkyBlue;
        private readonly IMvxLocationWatcher _locationWatcher;

        private readonly MvxSubscriptionToken _token;
        private int _countdown;
        private bool _countdownVis;
        private string _currentState;
        private DynoModel _dyno;
        private int _endAcceleration;
        private MvxGeoLocation _lastLocation;
        private System.Drawing.Color _pageBackColor;
        private bool _showAudioButtonVis;
        private Color _speedBackColor;
        private bool _startAccelerationButtonVis;
        private bool _stopAccelerationButtonVis;
        private bool _timerVis;
        private AudioRecorderService recorder;

        public int Countdown
        {
            get => this._countdown;
            set => this.SetProperty(ref this._countdown, value);
        }

        public bool CountdownVis
        {
            get => this._countdownVis;
            set => this.SetProperty(ref this._countdownVis, value);
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
        /// Gets or sets the last location.
        /// </summary>
        /// <value>The last location.</value>
        public MvxGeoLocation LastLocation
        {
            get => this._lastLocation;
            set => this.SetProperty(ref this._lastLocation, value);
        }

        public System.Drawing.Color PageBackColor
        {
            get => this._pageBackColor;
            set => this.SetProperty(ref this._pageBackColor, value);
        }

        public bool ShowAudioButtonVis
        {
            get => this._showAudioButtonVis;
            set => this.SetProperty(ref this._showAudioButtonVis, value);
        }

        public double? Speed
        {
            get => this.LastLocation?.Coordinates?.Speed ?? 0.00;
        }

        public System.Drawing.Color SpeedBackColor
        {
            get => this._speedBackColor;
            set => this.SetProperty(ref this._speedBackColor, value);
        }

        public bool StartAccelerationButtonVis
        {
            get => this._startAccelerationButtonVis;
            set => this.SetProperty(ref this._startAccelerationButtonVis, value);
        }

        public bool StopAccelerationButtonVis
        {
            get => this._stopAccelerationButtonVis;
            set => this.SetProperty(ref this._stopAccelerationButtonVis, value);
        }

        public int? Timer
        {
            get => this.LastLocation?.Timestamp.Millisecond ?? 0;
        }

        public bool TimerVis
        {
            get => this._timerVis;
            set => this.SetProperty(ref this._timerVis, value);
        }

        #endregion Values
    }
}