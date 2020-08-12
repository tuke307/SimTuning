// project=SimTuning.Forms.UI, file=Functions.cs, creation=2020:8:9
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.Business
{
    using System.Collections;
    using System.Collections.Generic;
    using XF.Material.Forms.UI.Dialogs;

    /// <summary>
    /// mobile-Funktionen.
    /// </summary>
    public static class Functions
    {
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