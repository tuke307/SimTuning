// project=SimTuning.Forms.UI, file=Functions.cs, creation=2020:8:9 Copyright (c) 2020
// tuke productions. All rights reserved.
using Xamarin.Forms.PlatformConfiguration;

namespace SimTuning.Forms.UI.Business
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    using XF.Material.Forms.UI.Dialogs;
    using static Xamarin.Essentials.Permissions;

    /// <summary>
    /// mobile-Funktionen.
    /// </summary>
    public static class Functions
    {
        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <typeparam name="TPermission">The type of the permission.</typeparam>
        /// <returns></returns>
        public static async Task<bool> GetPermission<TPermission>()
            where TPermission : BasePermission, new()
        {
            var hasPermission = await CheckStatusAsync<TPermission>().ConfigureAwait(true);

            if (hasPermission == PermissionStatus.Granted)
            {
                return true;
            }
            else if (hasPermission == PermissionStatus.Disabled)
            {
                return false;
            }

            var result = await RequestAsync<TPermission>().ConfigureAwait(true);
            if (result != PermissionStatus.Granted)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks the and request location when in use permission.
        /// </summary>
        /// <returns></returns>
        //public static async Task<PermissionStatus> CheckAndRequestLocationWhenInUsePermission()
        //{
        //    var locationWhenInUse = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>().ConfigureAwait(true);

        // if (locationWhenInUse != PermissionStatus.Granted) { locationWhenInUse = await
        // Permissions.RequestAsync<Permissions.LocationWhenInUse>().ConfigureAwait(true);
        // }

        //    return locationWhenInUse;
        //}

        /// <summary>
        /// Checks the and request microphone permission.
        /// </summary>
        /// <returns></returns>
        //public static async Task<PermissionStatus> CheckAndRequestMicrophonePermission()
        //{
        //    var microphone = await Permissions.CheckStatusAsync<Permissions.Microphone>().ConfigureAwait(true);

        // if (microphone != PermissionStatus.Granted) { microphone = await
        // Permissions.RequestAsync<Permissions.Microphone>().ConfigureAwait(true); }

        //    return microphone;
        //}

        /// <summary>
        /// Checks the and request network state permission.
        /// </summary>
        /// <returns></returns>
        //public static async Task<PermissionStatus> CheckAndRequestNetworkStatePermission()
        //{
        //    var networkState = await Permissions.CheckStatusAsync<Permissions.NetworkState>().ConfigureAwait(true);

        // if (networkState != PermissionStatus.Granted) { networkState = await
        // Permissions.RequestAsync<Permissions.NetworkState>().ConfigureAwait(true); }

        //    return networkState;
        //}

        /// <summary>
        /// Checks the and request storage read permission.
        /// </summary>
        /// <returns></returns>
        //public static async Task<PermissionStatus> CheckAndRequestStorageReadPermission()
        //{
        //    var storageRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>().ConfigureAwait(true);

        // if (storageRead != PermissionStatus.Granted) { storageRead = await
        // Permissions.RequestAsync<Permissions.StorageRead>().ConfigureAwait(true); }

        //    return storageRead;
        //}

        /// <summary>
        /// Checks the and request storage write permission.
        /// </summary>
        /// <returns></returns>
        //public static async Task<PermissionStatus> CheckAndRequestStorageWritePermission()
        //{
        //    var storageWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>().ConfigureAwait(true);

        // if (storageWrite != PermissionStatus.Granted) { storageWrite = await
        // Permissions.RequestAsync<Permissions.StorageWrite>().ConfigureAwait(true); }

        //    return storageWrite;
        //}

        /// <summary>
        /// Shows the snackbar dialog.
        /// </summary>
        /// <param name="content">The content.</param>
        public static async void ShowSnackbarDialog(object content)
        {
            if (content == null)
            {
                return;
            }

            if (content.GetType().IsGenericType && content is IEnumerable)
            {
                List<string> messages = (List<string>)content;

                foreach (var message in messages)
                {
                    await MaterialDialog.Instance.SnackbarAsync(
                        message: message,
                        msDuration: MaterialSnackbar.DurationLong).ConfigureAwait(true);
                }
            }
            else
            {
                await MaterialDialog.Instance.SnackbarAsync(
                    message: content.ToString(),
                    msDuration: MaterialSnackbar.DurationLong).ConfigureAwait(true);
            }
        }
    }
}