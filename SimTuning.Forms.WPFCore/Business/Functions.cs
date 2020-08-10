// project=SimTuning.Forms.WPFCore, file=Functions.cs, creation=2020:8:3
// Copyright (c) 2020 tuke productions. All rights reserved.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SimTuning.Forms.WPFCore.Views;
using SimTuning.Forms.WPFCore.Views.Dialog;

namespace SimTuning.Forms.WPFCore.Business
{
    public static class Functions
    {
        public static string GoToSite(string url)
        {
            Process myProcess = new Process();

            try
            {
                // true is the default, but it is important not to set it to false
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.FileName = url;
                myProcess.Start();
                //myProcess.Close();
                //myProcess.WaitForExit();
            }
            catch
            {
                return "Fehler beim Starten";
            }

            return null;
        }

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