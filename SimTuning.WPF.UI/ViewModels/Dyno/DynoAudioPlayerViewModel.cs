﻿using MaterialDesignThemes.Wpf;
using MediaManager;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.ViewModels.Dyno;
using SimTuning.WPF.UI.Business;
using SimTuning.WPF.UI.Dialog;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    public class DynoAudioPlayerViewModel : AudioPlayerViewModel
    {
        public DynoAudioPlayerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMediaManager mediaManager)
              : base(logProvider, navigationService, mediaManager)
        {
            this.CutBeginnCommand = new MvxAsyncCommand(this.CutBeginn);
            this.CutEndCommand = new MvxAsyncCommand(this.CutEnd);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Cuts the beginn.
        /// </summary>
        protected new async Task CutBeginn()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(async () =>
                {
                    await base.CutBeginn().ConfigureAwait(true);

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);

            // await this.ReloadImageAudioSpectrogram().ConfigureAwait(true);
        }

        /// <summary>
        /// Cuts the end.
        /// </summary>
        protected new async Task CutEnd()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                _ = Task.Run(async () =>
                {
                    await base.CutEnd().ConfigureAwait(true);

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);

            //await this.ReloadImageAudioSpectrogram().ConfigureAwait(true);
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected override async Task PlayFileAsync()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }
            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(async () =>
                    {
                        await base.PlayFileAsync().ConfigureAwait(true);
                        args.Session.Close();
                    });
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// Checks the dyno data.
        /// </summary>
        /// <returns></returns>
        private bool CheckDynoData()
        {
            if (!File.Exists(SimTuning.Core.GeneralSettings.AudioAccelerationFilePath))
            {
                Functions.ShowSnackbarDialog(rm.GetString("ERR_NOAUDIOFILE", CultureInfo.CurrentCulture));

                return false;
            }

            return true;
        }
    }
}