// Copyright (c) 2021 tuke productions. All rights reserved.
using Xamarin.Forms.PlatformConfiguration;

namespace SimTuning.Forms.UI.Helpers
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