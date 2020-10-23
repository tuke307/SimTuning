// project=SimTuning.WPF.UI, file=Functions.cs, creation=2020:9:2 Copyright (c) 2020 tuke
// productions. All rights reserved.
using MaterialDesignThemes.Wpf;
using SimTuning.WPF.UI.Dialog;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SimTuning.WPF.UI.Business
{
    public static class Functions
    {
        public static async void ShowSnackbarDialog(object content)
        {
            if (content == null)
            {
                return;
            }

            var view = new DialogSnackbarView();
            view.MySnackbar.MessageQueue.IgnoreDuplicate = true;

            if (content.GetType().IsGenericType && content is IEnumerable)
            {
                List<string> messages = (List<string>)content;

                foreach (var message in messages)
                {
                    view.MySnackbar.MessageQueue.Enqueue(message);
                }
            }
            else
            {
                view.MySnackbar.MessageQueue.Enqueue(content);
            }

            await DialogHost.Show(view, "DialogSnackbar").ConfigureAwait(true);
        }
    }
}