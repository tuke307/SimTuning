﻿// project=SimTuning.Forms.UI, file=Functions.cs, creation=2020:8:9 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.Business
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    using XF.Material.Forms.UI.Dialogs;

    /// <summary>
    /// mobile-Funktionen.
    /// </summary>
    public static class Functions
    {
        /// <summary>
        /// Checks the and request storage read permission.
        /// </summary>
        /// <returns></returns>
        public static async Task<PermissionStatus> CheckAndRequestStorageReadPermission()
        {
            var storageRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>().ConfigureAwait(true);

            if (storageRead != PermissionStatus.Granted)
            {
                storageRead = await Permissions.RequestAsync<Permissions.StorageRead>().ConfigureAwait(true);
            }

            return storageRead;
        }

        /// <summary>
        /// Checks the and request storage write permission.
        /// </summary>
        /// <returns></returns>
        public static async Task<PermissionStatus> CheckAndRequestStorageWritePermission()
        {
            var storageWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>().ConfigureAwait(true);

            if (storageWrite != PermissionStatus.Granted)
            {
                storageWrite = await Permissions.RequestAsync<Permissions.StorageWrite>().ConfigureAwait(true);
            }

            return storageWrite;
        }

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