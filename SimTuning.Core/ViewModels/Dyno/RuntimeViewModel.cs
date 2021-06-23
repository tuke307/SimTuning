namespace SimTuning.Core.ViewModels.Dyno
{
    using Data;
    using Data.Models;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
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
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="locationService">The location service.</param>
        /// <param name="messenger">The messenger.</param>
        public RuntimeViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, ILocationService locationService, IMvxMessenger messenger)
                                    : base(logFactory, navigationService)
        {
            this._token = messenger.Subscribe<MvxReloaderMessage>(this.ReloadData);
            this._token = messenger.Subscribe<MvxLocationMessage>(this.OnLocationUpdated);
            // this._token =
            // messenger.SubscribeOnMainThread<MvxLocationMessage>(this.OnLocationUpdated);

            this._locationService = locationService;

            // Commands
            this.StartAccelerationCommand = new MvxAsyncCommand(this.StartBeschleunigung);
            this.ResetAccelerationCommand = new MvxAsyncCommand(this.ResetRun);
            //this.StopAccelerationCommand = new MvxAsyncCommand(this.StopRun);

            // Visibility
            //this.StopAccelerationButtonVis = false;
            this.ShowAudioButtonVis = true;
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
                    db.Entry(this.Dyno).Collection(x => x.Geschwindigkeit).Load();
                    db.Entry(this.Dyno).Collection(x => x.Ausrollen).Load();

                    if (this.Dyno.Geschwindigkeit == null)
                    {
                        Dyno.Geschwindigkeit = new List<GeschwindigkeitModel>();
                    }

                    if (this.Dyno.Ausrollen == null)
                    {
                        Dyno.Ausrollen = new List<AusrollenModel>();
                    }
                }
            }
            catch (Exception exc)
            {
                this.Log.LogError("Fehler beim Laden des Dyno-Datensatz: ", exc);
            }
        }

        /// <summary>
        /// Ends the acceleration asynchronous.
        /// </summary>
        protected async Task EndRunAsync()
        {
            // tracking und recording stoppen
            await this.recorder.StopRecording().ConfigureAwait(true);
            this.trackingStarted = false;

            // UI aktualisieren
            this.ShowAudioButtonVis = true;
            this.StopwatchVis = false;
            this.CurrentState = PostState;
            this.PageBackColor = stdBg;
            this.SpeedBackColor = stdSur;

            // Timer und Stopwatch stoppen
            if (this.stopwatch != null)
            {
                this.stopwatch.Stop();
                this.stopwatch.Reset();
            }
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Dispose();
            }
        }

        protected void OnCountdownTimedEvent(object sender, ElapsedEventArgs e)
        {
            countdownMilliseconds -= 10;
            this.RaisePropertyChanged(() => this.Countdown);

            if (this.countdownMilliseconds == 0)
            {
                // Timer löschen und verbergen
                this.timer.Stop();
                this.timer.Dispose();
                this.CountdownVis = false;

                this.trackingStarted = true;

                // this.StopAccelerationButtonVis = true;

                // Stopwatch starten
                this.StartStopwatch();
            }
        }

        /// <summary>
        /// Called when [location updated].
        /// </summary>
        /// <param name="obj">The object.</param>
        protected void OnLocationUpdated(MvxLocationMessage obj)
        {
            this.Speed = obj.Speed;

            if (!this.trackingStarted)
            {
                return;
            }

            List<double?> lastSpeedValues;

            if (this.CurrentState == AccelerationState)
            {
                // TODO: verbessern bei keiner Veränderung der Maximalgeschwindigkeit
                // StartAusrollen() beginnen.

                using (var db = new Data.DatabaseContext())
                {
                    lastSpeedValues = db.Geschwindigkeit.OrderByDescending(x => x.CreatedDate).Select(x => x.Speed).Take(10).ToList();
                }

                if (lastSpeedValues != null && lastSpeedValues.Count == 10)
                {
                    var max = lastSpeedValues.Max();
                    var min = lastSpeedValues.Min();
                    var avg = lastSpeedValues.Average();

                    // im Bereich von 2 km/h
                    if ((avg - min) <= 2 && (max - avg) <= 2)
                    {
                        Task.Run(() => StartAusrollen());
                        return;
                    }
                }

                // asynchrones speichern der Beschlenugigungswerte
                //Task.Run(async () =>
                //{
                GeschwindigkeitModel beschleunigung = new GeschwindigkeitModel()
                {
                    Latitude = obj.Latitude,
                    Longitude = obj.Longitude,
                    Altitude = obj.Altitude,
                    Speed = obj.Speed,
                };

                using (var db = new Data.DatabaseContext())
                {
                    Dyno.Geschwindigkeit.Add(beschleunigung);

                    db.Update(this.Dyno);

                    db.SaveChanges();
                }
                //});
            }

            if (this.CurrentState == RolloutState)
            {
                using (var db = new Data.DatabaseContext())
                {
                    lastSpeedValues = db.Ausrollen.OrderByDescending(x => x.CreatedDate).Select(x => x.Speed).Take(5).ToList();
                }

                if (lastSpeedValues != null && lastSpeedValues.Count == 5)
                {
                    //var max = lastSpeedValues.Max();
                    //var min = lastSpeedValues.Min();
                    var avg = lastSpeedValues.Average();

                    // im Bereich unter 1 km/h
                    if (avg < 1)
                    {
                        Task.Run(() => EndRunAsync());
                        return;
                    }
                }

                //Task.Run(async () =>
                //{
                AusrollenModel ausrollen = new AusrollenModel()
                {
                    Latitude = obj.Latitude,
                    Longitude = obj.Longitude,
                    Altitude = obj.Altitude,
                    Speed = obj.Speed,
                };

                using (var db = new Data.DatabaseContext())
                {
                    Dyno.Ausrollen.Add(ausrollen);

                    db.Update(this.Dyno);

                    db.SaveChanges();
                }
                //});
            }
        }

        /// <summary>
        /// Called when [stopwatch timed event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        /// The <see cref="ElapsedEventArgs" /> instance containing the event data.
        /// </param>
        protected void OnStopwatchTimedEvent(object sender, ElapsedEventArgs e)
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
        /// Resets the dyno data.
        /// </summary>
        protected void ResetDynoData()
        {
            using (var db = new DatabaseContext())
            {
                db.Geschwindigkeit.RemoveRange(this.Dyno.Geschwindigkeit);
                db.Ausrollen.RemoveRange(this.Dyno.Ausrollen);
                //db.Update(this.Dyno);
                db.SaveChanges();
            }

            this.Dyno.Geschwindigkeit.Clear();

            this.Dyno.Ausrollen.Clear();
        }

        /// <summary>
        /// Resets the acceleration.
        /// </summary>
        protected virtual async Task ResetRun()
        {
            try
            {
                // Timer und Stopwatch stoppen
                if (this.stopwatch != null)
                {
                    this.stopwatch.Stop();
                    this.stopwatch.Reset();
                }
                if (this.timer != null)
                {
                    this.timer.Stop();
                    this.timer.Dispose();
                }

                // tracking und recording stoppen
                await this.recorder.StopRecording();
                this.trackingStarted = false;

                // UI aktualisieren
                await RaisePropertyChanged(() => Stopwatch).ConfigureAwait(true);
                this.PageBackColor = System.Drawing.Color.White;
                this.SpeedBackColor = System.Drawing.Color.White;
                this.CurrentState = PreState;
                this.StopwatchVis = false;
                this.CountdownVis = false;
                this.StartAccelerationButtonVis = true;
                this.ShowAudioButtonVis = true;

                // zuletzt probieren die Audio-Aufnahme zu löschen
                File.Delete(GeneralSettings.AudioAccelerationFilePath);
                File.Delete(GeneralSettings.AudioRolloutFilePath);
            }
            catch (Exception exc)
            {
                this.Log.LogError("Fehler bei ResetBeschleunigung: ", exc);
            }
        }

        /// <summary>
        /// Starts the ausrollen.
        /// </summary>
        protected virtual async Task StartAusrollen()
        {
            try
            {
                this.StopwatchVis = false;

                this.CurrentState = RolloutState;
                this.PageBackColor = deepSkyBlue;
                this.SpeedBackColor = skyBlue;
                this.trackingStarted = true;

                await this.StartRecording().ConfigureAwait(true);

                timer = new System.Timers.Timer();

                // trigger bei jeder 1/100 sekunde
                timer.Interval = 10;
                timer.Elapsed += OnCountdownTimedEvent;

                // count down from 5000 ms
                this.countdownMilliseconds = 5000;

                timer.Start();
                this.CountdownVis = true;
            }
            catch (Exception exc)
            {
                this.Log.LogError("Fehler bei StartAusrollen: ", exc);
            }
        }

        /// <summary>
        /// Starts the acceleration.
        /// </summary>
        protected virtual async Task StartBeschleunigung()
        {
            try
            {
                this.ResetDynoData();

                this.CurrentState = AccelerationState;
                this.PageBackColor = darkSeaGreen;
                this.SpeedBackColor = seaGreen;

                // es kann nun nicht mehr weiter navigiert werden
                this.ShowAudioButtonVis = false;

                await this.StartRecording().ConfigureAwait(true);

                this.StartAccelerationButtonVis = false;

                this.timer = new System.Timers.Timer();

                // trigger bei jeder 1/100 sekunde
                timer.Interval = 10;
                timer.Elapsed += OnCountdownTimedEvent;

                // count down from 10000 ms / 10s
                this.countdownMilliseconds = 10000;

                timer.Start();
                this.CountdownVis = true;
            }
            catch (Exception exc)
            {
                this.Log.LogError("Fehler bei StartBeschleunigung: ", exc);
            }
        }

        /// <summary>
        /// Starts the recording.
        /// </summary>
        protected Task StartRecording()
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

            // start recording audio
            return this.recorder.StartRecording();
        }

        /// <summary>
        /// Starts the stopwatch.
        /// </summary>
        protected void StartStopwatch()
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

        protected static System.Drawing.Color stdBg;
        protected static System.Drawing.Color stdSur;

        private const string AccelerationState = "Vollgas";
        private const string PostState = "Fertig";
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

        //private bool _stopRunButtonVis;
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
        //public bool StopAccelerationButtonVis
        //{
        //    get => this._stopRunButtonVis;
        //    set => this.SetProperty(ref this._stopRunButtonVis, value);
        //}

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