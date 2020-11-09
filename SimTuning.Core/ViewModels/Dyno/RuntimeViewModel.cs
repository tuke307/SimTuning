namespace SimTuning.Core.ViewModels.Dyno
{
    using Data;
    using Data.Models;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using Plugin.AudioRecorder;
    using SimTuning.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Threading.Tasks;
    using System.Timers;

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
        /// <param name="locationService">The location service.</param>
        /// <param name="messenger">The messenger.</param>
        public RuntimeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ILocationService locationService, IMvxMessenger messenger)
                                    : base(logProvider, navigationService)
        {
            this._token = messenger.Subscribe<MvxReloaderMessage>(this.ReloadData);
            this._token = messenger.Subscribe<MvxLocationMessage>(this.OnLocationUpdated);
            // this._token =
            // messenger.SubscribeOnMainThread<MvxLocationMessage>(this.OnLocationUpdated);

            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            this._locationService = locationService;

            // Commands
            this.StartAccelerationCommand = new MvxAsyncCommand(this.StartBeschleunigung);
            this.ResetAccelerationCommand = new MvxAsyncCommand(this.ResetRun);
            this.StopAccelerationCommand = new MvxAsyncCommand(this.StopRun);

            // Visibility
            this.StopAccelerationButtonVis = false;
            this.ShowAudioButtonVis = false;
            this.StopwatchVis = false;
            this.CountdownVis = false;

            // Anfahren
            this.CurrentState = PreState;
            this.StartAccelerationButtonVis = true;
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
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler beim Laden des Dyno-Datensatz: ", exc);
            }
        }

        /// <summary>
        /// Resets the acceleration.
        /// </summary>
        protected virtual async Task ResetRun()
        {
            try
            {
                await this.StopRun().ConfigureAwait(true);

                this.PageBackColor = System.Drawing.Color.White;
                this.SpeedBackColor = System.Drawing.Color.White;
                this.CurrentState = PreState;
                this.StartAccelerationButtonVis = true;

                // zuletzt probieren die Audio-Aufnahme zu löschen
                File.Delete(GeneralSettings.AudioAccelerationFilePath);
                File.Delete(GeneralSettings.AudioRolloutFilePath);
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei ResetBeschleunigung: ", exc);
            }
        }

        /// <summary>
        /// Starts the ausrollen.
        /// </summary>
        protected virtual async Task StartAusrollen()
        {
            try
            {
                this.Dyno.Ausrollen = new List<AusrollenModel>();

                this.CurrentState = RolloutState;
                this.PageBackColor = deepSkyBlue;
                this.SpeedBackColor = skyBlue;
                this.trackingStarted = true;

                await this.StartRecording().ConfigureAwait(true);
                //await this.StartTracking().ConfigureAwait(true);

                this.CountdownVis = true;
                timer = new System.Timers.Timer();

                // trigger bei jeder 1/100 sekunde
                timer.Interval = 10;
                timer.Elapsed += OnCountdownTimedEvent;

                // count down from 5000 ms
                this.countdownMilliseconds = 5000;

                timer.Start();
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei StartAusrollen: ", exc);
            }
        }

        /// <summary>
        /// Starts the acceleration.
        /// </summary>
        protected virtual async Task StartBeschleunigung()
        {
            try
            {
                this.Dyno.Beschleunigung = new List<BeschleunigungModel>();

                this.CurrentState = AccelerationState;
                this.PageBackColor = darkSeaGreen;
                this.SpeedBackColor = seaGreen;
                this.trackingStarted = true;

                await this.StartRecording().ConfigureAwait(true);
                //await this.StartTracking().ConfigureAwait(true);

                this.StartAccelerationButtonVis = false;

                this.CountdownVis = true;
                this.timer = new System.Timers.Timer();

                // trigger bei jeder 1/100 sekunde
                timer.Interval = 10;
                timer.Elapsed += OnCountdownTimedEvent;

                // count down from 5000 ms
                this.countdownMilliseconds = 5000;

                timer.Start();
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler bei StartBeschleunigung: ", exc);
            }
        }

        /// <summary>
        /// Stops the acceleration.
        /// </summary>
        protected virtual Task StopRun()
        {
            this.stopwatch.Stop();
            this.stopwatch.Reset();

            this.StopTracking();
            return this.StopRecording();

            //using (var db = new Data.DatabaseContext())
            //{
            //    db.Dyno.Update(this.Dyno);

            //    await db.SaveChangesAsync().ConfigureAwait(true);
            //}
        }

        private void OnCountdownTimedEvent(object sender, ElapsedEventArgs e)
        {
            countdownMilliseconds -= 10;
            this.RaisePropertyChanged(() => this.Countdown);

            if (this.countdownMilliseconds == 0)
            {
                // Timer löschen und verbergen
                this.timer.Stop();
                this.timer.Dispose();
                this.CountdownVis = false;

                // this.StopAccelerationButtonVis = true;

                // Stopwatch starten
                this.StartStopwatch();
            }
        }

        /// <summary>
        /// Called when [location updated].
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnLocationUpdated(MvxLocationMessage obj)
        {
            this.Speed = obj.Speed;

            if (!this.trackingStarted)
            {
                return;
            }

            if (this.CurrentState == AccelerationState)
            {
                // TODO: verbessern bei keiner Veränderung der Maximalgeschwindigkeit
                // StartAusrollen() beginnen.

                using (var db = new Data.DatabaseContext())
                {
                    var lastValues = db.Beschleunigung.Select(x => x.Speed.Value).Take(10).ToList();

                    if (lastValues != null && lastValues.Count > 0)
                    {
                        var max = lastValues.Max();
                        var min = lastValues.Min();

                        if ((max - min) <= 2)
                        {
                            StartAusrollen();
                        }
                    }
                }

                Task.Run(async () =>
                {
                    var beschleunigung = new BeschleunigungModel()
                    {
                        Dyno = this.Dyno,
                        Latitude = obj.Latitude,
                        Longitude = obj.Longitude,
                        Altitude = obj.Altitude,
                        Speed = obj.Speed,
                    };

                    using (var db = new Data.DatabaseContext())
                    {
                        db.Beschleunigung.Add(beschleunigung);

                        await db.SaveChangesAsync().ConfigureAwait(true);
                    }
                });
            }

            if (this.CurrentState == RolloutState)
            {
                Task.Run(async () =>
                {
                    var ausrollen = new AusrollenModel()
                    {
                        Dyno = this.Dyno,
                        Latitude = obj.Latitude,
                        Longitude = obj.Longitude,
                        Altitude = obj.Altitude,
                        Speed = obj.Speed,
                    };

                    using (var db = new Data.DatabaseContext())
                    {
                        db.Ausrollen.Add(ausrollen);

                        await db.SaveChangesAsync().ConfigureAwait(true);
                    }
                });
            }
        }

        /// <summary>
        /// Called when [stopwatch timed event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        /// The <see cref="ElapsedEventArgs" /> instance containing the event data.
        /// </param>
        private void OnStopwatchTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (!this.stopwatch.IsRunning)
            {
                this.timer.Stop();
                this.timer.Dispose();
            }
            else
            {
                this.RaisePropertyChanged(() => this.Stopwatch);
            }
        }

        /// <summary>
        /// Starts the recording.
        /// </summary>
        private Task StartRecording()
        {
            // Recorder
            this.recorder = new AudioRecorderService();

            // audio datei zum schreiben auswählen
            if (this.CurrentState == AccelerationState)
            {
                this.recorder.FilePath = GeneralSettings.AudioAccelerationFilePath;
            }
            else if (this.CurrentState == RolloutState)
            {
                this.recorder.FilePath = GeneralSettings.AudioRolloutFilePath;
            }

            this.recorder.PreferredSampleRate = 44100;

            // if (this.recorder.IsRecording) { await
            // this.StopRecording().ConfigureAwait(true); }

            // start recording audio
            return this.recorder.StartRecording();
        }

        /// <summary>
        /// Starts the stopwatch.
        /// </summary>
        private void StartStopwatch()
        {
            this.stopwatch = new Stopwatch();

            if (!this.stopwatch.IsRunning)
            {
                this.StopwatchVis = true;
                this.stopwatch.Start();

                this.timer = new System.Timers.Timer();

                // Trigger nei 1/10 s bzw aller 100 ms
                this.timer.Interval = 100;
                this.timer.Elapsed += this.OnStopwatchTimedEvent;

                this.timer.Start();
            }
        }

        ///// <summary>
        ///// Starts the tracking.
        ///// </summary>
        //private async Task StartTracking()
        //{
        //    if (this._locationWatcher.Started)
        //    {
        //        await this.StopBeschleunigung().ConfigureAwait(true);
        //    }
        //}

        /// <summary>
        /// Stops the recording.
        /// </summary>
        private Task StopRecording()
        {
            return this.recorder.StopRecording();
        }

        /// <summary>
        /// Stops the tracking.
        /// </summary>
        private void StopTracking()
        {
            trackingStarted = false;
            //this._locationWatcher.Stop();
        }

        #endregion Methods

        #region Values

        #region Commands

        /// <summary>
        /// Gets or sets the close command.
        /// </summary>
        /// <value>The close command.</value>
        //public IMvxAsyncCommand CloseCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the reset tracking command.
        /// </summary>
        /// <value>The reset tracking command.</value>
        public MvxAsyncCommand ResetAccelerationCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the show audio command.
        /// </summary>
        /// <value>The show audio command.</value>
        public IMvxAsyncCommand ShowSpectrogramCommand { get; protected set; }

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

        private const string AccelerationState = "Vollgas";
        private const string PreState = "Anfahren";
        private const string RolloutState = "Ausrollen";
        private static System.Drawing.Color darkSeaGreen = System.Drawing.Color.DarkSeaGreen;
        private static System.Drawing.Color deepSkyBlue = System.Drawing.Color.DeepSkyBlue;
        private static System.Drawing.Color seaGreen = System.Drawing.Color.SeaGreen;
        private static System.Drawing.Color skyBlue = System.Drawing.Color.SkyBlue;

        private readonly ILocationService _locationService;
        private readonly MvxSubscriptionToken _token;
        private bool _countdownVis;
        private string _currentState;
        private DynoModel _dyno;
        private System.Drawing.Color _pageBackColor;
        private bool _showAudioButtonVis;
        private double? _speed;
        private Color _speedBackColor;
        private bool _startRunButtonVis;
        private bool _stopRunButtonVis;
        private bool _stopwatchVis;
        private double countdownMilliseconds;
        private AudioRecorderService recorder;
        private System.Diagnostics.Stopwatch stopwatch;
        private System.Timers.Timer timer;
        private bool trackingStarted;

        /// <summary>
        /// Gets the countdown.
        /// </summary>
        /// <value>The countdown.</value>
        public string Countdown
        {
            get => string.Format("{0:D2}:{1:D2}", CountdownTimeSpan.Seconds, CountdownTimeSpan.Milliseconds);
        }

        /// <summary>
        /// Gets the countdown time span.
        /// </summary>
        /// <value>The countdown time span.</value>
        public TimeSpan CountdownTimeSpan
        {
            get => TimeSpan.FromMilliseconds(countdownMilliseconds);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [countdown vis].
        /// </summary>
        /// <value><c>true</c> if [countdown vis]; otherwise, <c>false</c>.</value>
        public bool CountdownVis
        {
            get => this._countdownVis;
            protected set => this.SetProperty(ref this._countdownVis, value);
        }

        /// <summary>
        /// Gets or sets the state of the current.
        /// </summary>
        /// <value>The state of the current.</value>
        public string CurrentState
        {
            get => _currentState;
            protected set => SetProperty(ref _currentState, value);
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
        /// Gets or sets the color of the page back.
        /// </summary>
        /// <value>The color of the page back.</value>
        public System.Drawing.Color PageBackColor
        {
            get => this._pageBackColor;
            set => this.SetProperty(ref this._pageBackColor, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show audio button vis].
        /// </summary>
        /// <value><c>true</c> if [show audio button vis]; otherwise, <c>false</c>.</value>
        public bool ShowAudioButtonVis
        {
            get => this._showAudioButtonVis;
            set => this.SetProperty(ref this._showAudioButtonVis, value);
        }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        public double? Speed
        {
            get
            {
                if (this._speed == null)
                {
                    return 0.0;
                }
                else
                {
                    return Math.Round((double)this._speed, 2);
                }
            }
            set => this.SetProperty(ref this._speed, value);
        }

        /// <summary>
        /// Gets or sets the color of the speed back.
        /// </summary>
        /// <value>The color of the speed back.</value>
        public System.Drawing.Color SpeedBackColor
        {
            get => this._speedBackColor;
            set => this.SetProperty(ref this._speedBackColor, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [start acceleration button vis].
        /// </summary>
        /// <value>
        /// <c>true</c> if [start acceleration button vis]; otherwise, <c>false</c>.
        /// </value>
        public bool StartAccelerationButtonVis
        {
            get => this._startRunButtonVis;
            set => this.SetProperty(ref this._startRunButtonVis, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [stop acceleration button vis].
        /// </summary>
        /// <value>
        /// <c>true</c> if [stop acceleration button vis]; otherwise, <c>false</c>.
        /// </value>
        public bool StopAccelerationButtonVis
        {
            get => this._stopRunButtonVis;
            set => this.SetProperty(ref this._stopRunButtonVis, value);
        }

        /// <summary>
        /// Gets the stopwatch.
        /// </summary>
        /// <value>The stopwatch.</value>
        public string? Stopwatch
        {
            get => string.Format("{0:D2}:{1:D2}:{2:D2}", this.stopwatch?.Elapsed.Minutes, this.stopwatch?.Elapsed.Seconds, this.stopwatch?.Elapsed.Milliseconds);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [stopwatch vis].
        /// </summary>
        /// <value><c>true</c> if [stopwatch vis]; otherwise, <c>false</c>.</value>
        public bool StopwatchVis
        {
            get => this._stopwatchVis;
            set => this.SetProperty(ref this._stopwatchVis, value);
        }

        #endregion Values
    }
}