using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.Business
{
    public static class Functions
    {
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
                    await MaterialDialog.Instance.SnackbarAsync(message: message,
                                           msDuration: MaterialSnackbar.DurationLong).ConfigureAwait(true);
                }
            }
            else
            {
                await MaterialDialog.Instance.SnackbarAsync(message: content.ToString(),
                                           msDuration: MaterialSnackbar.DurationLong).ConfigureAwait(true);
            }
        }
    }
}