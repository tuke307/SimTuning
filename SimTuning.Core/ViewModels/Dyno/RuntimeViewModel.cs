namespace SimTuning.Core.ViewModels.Dyno
{
    using Data;
    using Data.Models;
    using MvvmCross.Commands;
    using MvvmCross.IoC;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Location;
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
        /// <param name="locationWatcher">The location watcher.</param>
        /// <param name="messenger">The messenger.</param>
        public RuntimeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ILocationService locationService, IMvxMessenger messenger)
                                    : base(logProvider, navigationService)
        {
            this._token = messenger.Subscribe<MvxReloaderMessage>(this.ReloadData);
            this._token = messenger.Subscribe<MvxLocationMessage>(this.OnLocationUpdated);
            //this._token = messenger.SubscribeOnMainThread<MvxLocationMessage>(this.OnLocationUpdated);

            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            //this._locationWatcher = locationWatcher;

            // Commands
            this.StartAccelerationCommand = new MvxAsyncCommand(this.StartBeschleunigung);
            this.ResetAccelerationCommand = new MvxAsyncCommand(this.ResetBeschleunigung);
            this.StopAccelerationCommand = new MvxAsyncCommand(this.StopBeschleunigung);

            // Recorder
            this.recorder = new AudioRecorderService();
            this.recorder.FilePath = GeneralSettings.AudioFilePath;
            this.recorder.PreferredSampleRate = 44100;

            // Visibility
            this.StopAccelerationButtonVis = false;
            this.ShowAudioButtonVis = false;
            this.StopwatchVis = false;
            this.CountdownVis = false;

            // Anfahren
            this.CurrentState = preState;
            this.StartAccelerationButtonVis = true;

            //var test = MvxIoCProvider.Instance.Resolve<ILocationService>();
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
        protected virtual async Task ResetBeschleunigung()
        {
            try
            {
                this._stopwatch.Stop();
                this._stopwatch.Reset();

                this.StopTracking();
                await this.StopRecording().ConfigureAwait(true);

                this.PageBackColor = System.Drawing.Color.White;
                this.SpeedBackColor = System.Drawing.Color.White;
                this.CurrentState = preState;
                this.StartAccelerationButtonVis = true;

                // zuletzt probieren die Audio-Aufnahme zu löschen
                File.Delete(this.recorder.FilePath);
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler beim Reset: ", exc);
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
                //await this.StartTracking().ConfigureAwait(true);

                this.Dyno.Ausrollen = new List<AusrollenModel>();

                this.CurrentState = secondState;
                this.PageBackColor = deepSkyBlue;
                this.SpeedBackColor = skyBlue;

                _timer = new System.Timers.Timer();
                //Trigger event every second
                _timer.Interval = 100;
                _timer.Elapsed += OnCountdownTimedEvent;

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
                //await this.StartTracking().ConfigureAwait(true);

                this.Dyno.Beschleunigung = new List<BeschleunigungModel>();

                this.CurrentState = firstState;
                this.PageBackColor = darkSeaGreen;
                this.SpeedBackColor = seaGreen;

                this.CountdownVis = true;
                this._timer = new System.Timers.Timer();

                // Trigger event every 100 ms
                this._timer.Interval = 100;
                this._timer.Elapsed += this.OnCountdownTimedEvent;

                // count down from 5000 ms
                this.Countdown = 5000;

                this._timer.Enabled = true;
            }
            catch (Exception exc)
            {
                this.Log.ErrorException("Fehler beim Laden des Dyno-Datensatz: ", exc);
            }
        }

        /// <summary>
        /// Stops the acceleration.
        /// </summary>
        protected virtual async Task StopBeschleunigung()
        {
            this._stopwatch.Stop();
            this.StopTracking();
            await this.StopRecording().ConfigureAwait(true);

            using (var db = new Data.DatabaseContext())
            {
                db.Dyno.Update(this.Dyno);

                await db.SaveChangesAsync().ConfigureAwait(true);
            }
        }

        private void OnCountdownTimedEvent(object sender, ElapsedEventArgs e)
        {
            // 100ms immer abziehen
            this.Countdown -= 100;

            if (this.Countdown == 0)
            {
                this._timer.Stop();
                this._timer.Dispose();
                this.CountdownVis = false;

                if (this.StartAccelerationButtonVis)
                {
                    this.StartAccelerationButtonVis = false;
                }

                this._stopwatch = new Stopwatch();
                this._stopwatch.Reset();

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

            if (this.CurrentState == firstState)
            {
                // TODO: bei keiner Veränderung der Maximalgeschwindigkeit StartAusrollen() beginnen.

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

            if (this.CurrentState == secondState)
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

        private void OnStopwatchTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (!this._stopwatch.IsRunning)
            {
                this._timer.Stop();
                this._timer.Dispose();
            }
            else
            {
                this.RaisePropertyChanged(() => this.Stopwatch);
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

        private void StartStopwatch()
        {
            if (!this._stopwatch.IsRunning)
            {
                this.StopwatchVis = true;
                this._stopwatch.Start();

                this._timer = new System.Timers.Timer();

                // Trigger event every 100 ms
                this._timer.Interval = 100;
                this._timer.Elapsed += this.OnStopwatchTimedEvent;
                this._timer.Enabled = true;
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
        private async Task StopRecording()
        {
            await this.recorder.StopRecording().ConfigureAwait(true);
        }

        /// <summary>
        /// Stops the tracking.
        /// </summary>
        private void StopTracking()
        {
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
        private const string firstState = "Vollgas";
        private const string preState = "Anfahren";
        private const string secondState = "Ausrollen";
        private static System.Drawing.Color darkSeaGreen = System.Drawing.Color.DarkSeaGreen;
        private static System.Drawing.Color deepSkyBlue = System.Drawing.Color.DeepSkyBlue;
        private static System.Drawing.Color seaGreen = System.Drawing.Color.SeaGreen;
        private static System.Drawing.Color skyBlue = System.Drawing.Color.SkyBlue;

        //private readonly IMvxLocationWatcher _locationWatcher;
        private readonly MvxSubscriptionToken _token;

        private int _countdown;
        private bool _countdownVis;
        private string _currentState;
        private DynoModel _dyno;
        private MvxGeoLocation _lastLocation;
        private System.Drawing.Color _pageBackColor;
        private bool _showAudioButtonVis;
        private double? _speed;
        private Color _speedBackColor;
        private bool _startAccelerationButtonVis;
        private bool _stopAccelerationButtonVis;
        private System.Diagnostics.Stopwatch _stopwatch;
        private bool _stopwatchVis;
        private System.Timers.Timer _timer;
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
            get => this._speed ?? 0.0;
            set => this.SetProperty(ref this._speed, value);
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

        public string? Stopwatch
        {
            get => this._stopwatch?.Elapsed.ToString(@":mm\:ss\:ff");
        }

        public bool StopwatchVis
        {
            get => this._stopwatchVis;
            set => this.SetProperty(ref this._stopwatchVis, value);
        }

        #endregion Values
    }
}