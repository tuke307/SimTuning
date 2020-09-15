namespace SimTuning.Core.ViewModels.Dyno
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Location;
    using MvvmCross.ViewModels;
    using System;
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

            this.StartTrackingCommand = new MvxAsyncCommand(this.StartTracking, () => !this._locationWatcher.Started);

            this.ResetTrackingCommand = new MvxAsyncCommand(this.ResetTracking);
        }

        #region Methods

        private void OnLocationUpdated(MvxGeoLocation obj)
        {
            this.LastLocation = obj;
        }

        private void OnLocationUpdateError(MvxLocationError obj)
        {
            System.Diagnostics.Debug.WriteLine($"Location Error: {obj.Code} {obj.ToString()}");
        }

        private async Task ResetTracking()
        {
            this._locationWatcher.Stop();
        }

        private async Task StartTracking()
        {
            // var status = await RequestPermission(); if (!status) return;

            var options = new MvxLocationOptions
            {
                Accuracy = MvxLocationAccuracy.Fine,
                TrackingMode = MvxLocationTrackingMode.Foreground,
                TimeBetweenUpdates = TimeSpan.Zero,
            };

            this._locationWatcher.Start(options, this.OnLocationUpdated, this.OnLocationUpdateError);
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
        public MvxAsyncCommand ResetTrackingCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the show audio command.
        /// </summary>
        /// <value>The show audio command.</value>
        public IMvxAsyncCommand ShowAudioCommand { get; protected set; }

        /// <summary>
        /// Gets or sets the start tracking command.
        /// </summary>
        /// <value>The start tracking command.</value>
        public MvxAsyncCommand StartTrackingCommand { get; protected set; }

        #endregion Commands

        private const string firstState = "Vollgas";

        private const string secondState = "Ausrollen";

        private readonly IMvxLocationWatcher _locationWatcher;

        private string _currentState;

        private int _endAcceleration;

        private MvxGeoLocation _lastLocation;

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

        #endregion Values
    }
}