﻿using MvvmCross;
using MvvmCross.Logging;
using MvvmCross.Plugin.Location;
using MvvmCross.Plugin.Messenger;
using SimTuning.Core.Models;
using System;

namespace SimTuning.Core.Models
{
    public class LocationService
       : ILocationService
    {
        private readonly IMvxLog _log;
        private readonly IMvxMessenger _messenger;
        private readonly IMvxLocationWatcher _watcher;

        public LocationService(IMvxLocationWatcher watcher, IMvxMessenger messenger, IMvxLog log)
        {
            this._watcher = watcher;
            this._messenger = messenger;
            this._log = log;

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
            this._log.Warn($"Location Error: {error.Code} {error.ToString()}");
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