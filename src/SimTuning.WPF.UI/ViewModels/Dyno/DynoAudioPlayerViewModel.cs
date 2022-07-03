// Copyright (c) 2021 tuke productions. All rights reserved.
using MaterialDesignThemes.Wpf;
using MediaManager;
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using SimTuning.Core.ViewModels.Dyno;
using SimTuning.WPF.UI.Business;
using SimTuning.WPF.UI.Dialog;
using SimTuning.WPF.UI.Messages;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    /// <summary>
    /// DynoAudioPlayerViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.AudioPlayerViewModel" />
    public class DynoAudioPlayerViewModel : AudioPlayerViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoAudioPlayerViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="mediaManager">The media manager.</param>
        public DynoAudioPlayerViewModel(
            ILogger<DynoAudioPlayerViewModel> logger,
            IMvxNavigationService navigationService,
            IMediaManager mediaManager,
            IMvxMessenger messenger)
              : base(logger, navigationService, mediaManager, messenger)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.CutBeginnCommand = new MvxAsyncCommand(this.CutBeginn);
            this.CutEndCommand = new MvxAsyncCommand(this.CutEnd);

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc cref="AudioPlayerViewModel.CutBeginn" />
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

        /// <inheritdoc cref="AudioPlayerViewModel.CutEnd" />
        protected new async Task CutEnd()
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
                    await base.CutEnd().ConfigureAwait(true);

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);

            // await this.ReloadImageAudioSpectrogram().ConfigureAwait(true);
        }

        /// <inheritdoc />
        protected override async Task PlayFileAsync()
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
                    await Application.Current.Dispatcher.InvokeAsync(() => base.PlayFileAsync());
                    // await base.PlayFileAsync().ConfigureAwait(true);
                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// Überprüft ob wichtige Dyno-Audio-Daten vorhanden sind.
        /// </summary>
        private bool CheckDynoData()
        {
            if (!File.Exists(SimTuning.Core.GeneralSettings.AudioAccelerationFilePath))
            {
                _messenger.Publish(
                    new ShowSnackbarMessage(
                        this,
                        SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NOAUDIOFILE")));

                return false;
            }

            return true;
        }

        #endregion Methods

        #region Values

        private readonly ILogger<DynoAudioPlayerViewModel> _logger;

        #endregion Values
    }
}