﻿// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using AutoUpdaterDotNET;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// WPF-spezifisches Einstellungen-Update-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class EinstellungenUpdateViewModel : MvxViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenUpdateViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenUpdateViewModel(
            ILogger<EinstellungenUpdateViewModel> logger,
            IMvxNavigationService navigationService)
        {
            this._navigationService = navigationService;
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.UpdateCheckCommand = new MvxCommand(this.UpdateCheck);
            this.StartUpdateCommand = new MvxCommand(this.StartUpdate);
            OpenMenuCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinstellungenMenuViewModel>());

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Gets the changelog.
        /// </summary>
        private void GetChangelog()
        {
            // Download
            using (WebClient webClient = new WebClient())
            {
                using (Stream responseStream = webClient.OpenRead(releasenotesLink))
                {
                    using (Stream fileStream = File.Create(SimTuning.Core.GeneralSettings.ReleaseNotesFilePath))
                    {
                        responseStream.CopyTo(fileStream);
                    }
                }
            }

            this.ReleaseNotes = System.IO.File.ReadAllText(SimTuning.Core.GeneralSettings.ReleaseNotesFilePath);
        }

        /// <summary>
        /// Starts the update.
        /// </summary>
        private void StartUpdate()
        {
            AutoUpdater.Start(updateLink);
            AutoUpdater.ReportErrors = true;
            AutoUpdater.HttpUserAgent = "SimTuningDonwloadUpdate";
            AutoUpdater.CheckForUpdateEvent += this.UpdaterDownloadUpdateEvent;
        }

        /// <summary>
        /// Updates the check.
        /// </summary>
        private void UpdateCheck()
        {
            AutoUpdater.Start(updateLink);
            AutoUpdater.ReportErrors = true;
            AutoUpdater.HttpUserAgent = "SimTuningUpdateCheck";
            AutoUpdater.CheckForUpdateEvent += this.UpdaterCheckForUpdateEvent;

            this.GetChangelog();
        }

        /// <summary>
        /// Updaters the check for update event.
        /// </summary>
        /// <param name="args">
        /// The <see cref="UpdateInfoEventArgs" /> instance containing the event data.
        /// </param>
        private void UpdaterCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                this.VersionNew = args.CurrentVersion.ToString();
                this.VersionNow = args.InstalledVersion.ToString();

                if (args.IsUpdateAvailable)
                {
                    this.UpdateButton = true;
                }
            }
        }

        /// <summary>
        /// Updaters the download update event.
        /// </summary>
        /// <param name="args">
        /// The <see cref="UpdateInfoEventArgs" /> instance containing the event data.
        /// </param>
        private void UpdaterDownloadUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                try
                {
                    AutoUpdater.DownloadUpdate(args);
                }
                catch
                {
                    // MessageBox.Show("FEHLER");
                }
            }
        }

        #endregion Methods

        #region Values

        protected readonly IMvxNavigationService _navigationService;
        private static string releasenotesLink = "https://simtuning.tuke-productions.de/wp-content/uploads/releasenotes.txt";
        private static string updateLink = "https://simtuning.tuke-productions.de/wp-content/uploads/AutoUpdater.xml";
        private readonly ILogger<EinstellungenUpdateViewModel> _logger;

        #region Commands

        public MvxAsyncCommand OpenMenuCommand { get; set; }

        /// <summary>
        /// Gets or sets the start update command.
        /// </summary>
        /// <value>The start update command.</value>
        public ICommand StartUpdateCommand { get; set; }

        /// <summary>
        /// Gets or sets the update check command.
        /// </summary>
        /// <value>The update check command.</value>
        public ICommand UpdateCheckCommand { get; set; }

        #endregion Commands

        private string _releaseNotes;
        private bool _updateButton;
        private string _versionNew;
        private string _versionNow;

        /// <summary>
        /// Gets or sets the release notes.
        /// </summary>
        /// <value>The release notes.</value>
        public string ReleaseNotes
        {
            get => this._releaseNotes;
            set => this.SetProperty(ref this._releaseNotes, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [update button].
        /// </summary>
        /// <value><c>true</c> if [update button]; otherwise, <c>false</c>.</value>
        public bool UpdateButton
        {
            get => this._updateButton;
            set => this.SetProperty(ref this._updateButton, value);
        }

        /// <summary>
        /// Gets or sets the version new.
        /// </summary>
        /// <value>The version new.</value>
        public string VersionNew
        {
            get => this._versionNew;
            set => this.SetProperty(ref this._versionNew, value);
        }

        /// <summary>
        /// Gets or sets the version now.
        /// </summary>
        /// <value>The version now.</value>
        public string VersionNow
        {
            get => this._versionNow;
            set => this.SetProperty(ref this._versionNow, value);
        }

        #endregion Values
    }
}