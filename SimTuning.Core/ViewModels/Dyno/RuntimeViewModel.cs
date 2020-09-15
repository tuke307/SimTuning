using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Location;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Dyno
{
    public class RuntimeViewModel : MvxNavigationViewModel
    {
        private readonly IMvxLocationWatcher _locationWatcher;

        private MvxGeoLocation _lastLocation;

        public MvxGeoLocation LastLocation
        {
            get => this._lastLocation;
            set => this.SetProperty(ref this._lastLocation, value);
        }

        public MvxAsyncCommand StartCommand { get; }

        public RuntimeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxLocationWatcher locationWatcher)
                                    : base(logProvider, navigationService)
        {
            this._locationWatcher = locationWatcher;

            this.StartCommand = new MvxAsyncCommand(this.DoStartCommand, () => !this._locationWatcher.Started);
        }

        private async Task DoStartCommand()
        {
            // var status = await RequestPermission(); if (!status) return;

            var options = new MvxLocationOptions
            {
                Accuracy = MvxLocationAccuracy.Coarse,
                TrackingMode = MvxLocationTrackingMode.Foreground,
                TimeBetweenUpdates = TimeSpan.Zero,
            };

            this._locationWatcher.Start(options, this.OnLocationUpdated, this.OnLocationUpdateError);
        }

        private void OnLocationUpdated(MvxGeoLocation obj)
        {
            this.LastLocation = obj;
        }

        private void OnLocationUpdateError(MvxLocationError obj)
        {
            System.Diagnostics.Debug.WriteLine($"Location Error: {obj.Code} {obj.ToString()}");
        }
    }
}