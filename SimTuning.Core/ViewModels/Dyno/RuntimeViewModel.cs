namespace SimTuning.Core.ViewModels.Dyno
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Location;
    using MvvmCross.ViewModels;
    using Plugin.AudioRecorder;
    using SimTuning.Core.Business;
    using System;
    using System.Collections.Generic;
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
        public RuntimeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxLocationWatcher locationWatcher)
                                    : base(logProvider, navigationService)
        {
            this._locationWatcher = locationWatcher;

            this.StartAccelerationCommand = new MvxAsyncCommand(this.StartAcceleration);

            this.ResetAccelerationCommand = new MvxAsyncCommand(this.ResetAcceleration);
            this.StopAccelerationCommand = new MvxAsyncCommand(this.StopAcceleration);

            mvxCoordinates = new List<MvxCoordinates>();

            recorder = new AudioRecorderService();
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        private void OnLocationUpdated(MvxGeoLocation obj)
        {
            this.LastLocation = obj;
            mvxCoordinates.Add(obj.Coordinates);
            this.RaisePropertyChanged(() => this.Speed);
            this.RaisePropertyChanged(() => this.Timer);

            if ((int)this.Speed == EndAcceleration)
            {
                CurrentState = secondState;
                BackgroundColor = blue;
            }
        }

        private void OnLocationUpdateError(MvxLocationError obj)
        {
            this.Log.Warn($"Location Error: {obj.Code} {obj.ToString()}");
        }

        private async Task ResetAcceleration()
        {
            await StopAcceleration();
        }

        private async Task StartAcceleration()
        {
            await StartRecording();
            await StartTracking();
        }

        private async Task StartRecording()
        {
            if (recorder.IsRecording)
            {
                await StopRecording();
            }

            // start recording audio
            var audioRecordTask = await recorder.StartRecording();

            await audioRecordTask;
        }

        private async Task StartTracking()
        {
            if (_locationWatcher.Started)
            {
                await StopAcceleration();
            }
            // var status = await RequestPermission(); if (!status) return;

            var options = new MvxLocationOptions
            {
                Accuracy = MvxLocationAccuracy.Fine,
                TrackingMode = MvxLocationTrackingMode.Foreground,
                TimeBetweenUpdates = TimeSpan.Zero,
                MovementThresholdInM = 0,
            };

            this._locationWatcher.Start(options, this.OnLocationUpdated, this.OnLocationUpdateError);

            CurrentState = firstState;
            BackgroundColor = green;
        }

        private async Task StopAcceleration()
        {
            await StopTracking();
            await StopRecording();
        }

        private async Task StopRecording()
        {
            //stop the recording...
            await recorder.StopRecording();
        }

        private async Task StopTracking()
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

        private const string firstState = "Vollgas";
        private const string secondState = "Ausrollen";
        private static System.Drawing.Color blue = System.Drawing.Color.SkyBlue;
        private static System.Drawing.Color green = System.Drawing.Color.DarkSeaGreen;
        private readonly IMvxLocationWatcher _locationWatcher;
        private System.Drawing.Color _backgroundColor;
        private string _currentState;
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
        /// Gets or sets the end acceleration.
        /// </summary>
        /// <value>The end acceleration.</value>
        public int EndAcceleration
        {
            get => _endAcceleration;
            set => SetProperty(ref _endAcceleration, value);
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

        protected List<MvxCoordinates> mvxCoordinates { get; set; }

        #endregion Values
    }
}