using MvvmCross;
using MvvmCross.Plugin.Location;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimTuning.Core.Models
{
    public interface ILocationService
    {
    }

    public class LocationService
        : ILocationService
    {
        private readonly IMvxMessenger _messenger;
        private readonly IMvxLocationWatcher _watcher;

        public LocationService(IMvxLocationWatcher watcher, IMvxMessenger messenger)
        {
            this._watcher = watcher;
            this._messenger = messenger;

            var options = new MvxLocationOptions
            {
                Accuracy = MvxLocationAccuracy.Fine,
                TrackingMode = MvxLocationTrackingMode.Foreground,
                TimeBetweenUpdates = TimeSpan.Zero,
                MovementThresholdInM = 0,
            };

            this._watcher.Start(options, this.OnLocation, this.OnError);
        }

        private void OnError(MvxLocationError error)
        {
            // this.Log.Warn($"Location Error: {error.Code} {obj.ToString()}");
            //Mvx.Error("Seen location error {0}", error.Code);
        }

        private void OnLocation(MvxGeoLocation location)
        {
            var message = new MvxLocationMessage(this,
                                                location.Coordinates.Latitude,
                                                location.Coordinates.Longitude,
                                                location.Coordinates.Altitude,
                                                location.Coordinates.Speed);

            this._messenger.Publish(message);
        }
    }
}