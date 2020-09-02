// project=SimTuning.WPF.UI, file=EinstellungenUpdateViewModel.cs, creation=2020:7:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using AutoUpdaterDotNET;
    using MvvmCross.Commands;
    using MvvmCross.ViewModels;
    using System.IO;
    using System.Net;
    using System.Windows;
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
        public EinstellungenUpdateViewModel()
        {
            this.UpdateCheckCommand = new MvxCommand<string>(UpdateCheck);
            this.StartUpdateCommand = new MvxCommand<string>(StartUpdate);
        }

        #region Methods

        /// <summary>
        /// Gets the changelog.
        /// </summary>
        private void Get_Changelog()
        {
            string file = "https://simtuning.tuke-productions.de/download/releasenotes/?wpdmdl=65";
            string fileName = @"releasenotes.rtf";

            // Download
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(file, fileName);

            // lesen und einfügen
            if (File.Exists(fileName))
            {
                //var flowDocument = new FlowDocument();
                //var textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                //using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                //{
                //    textRange.Load(fileStream, DataFormats.Rtf);
                //}
                //RTFtext = flowDocument;
            }
        }

        /// <summary>
        /// Starts the update.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void StartUpdate(object parameter)
        {
            AutoUpdater.Start("https://simtuning.tuke-productions.de/download/autoupdater/?wpdmdl=42");
            AutoUpdater.ReportErrors = true;
            AutoUpdater.HttpUserAgent = "SimTuningDonwloadUpdate";
            AutoUpdater.CheckForUpdateEvent += UpdaterDownloadUpdateEvent;
        }

        /// <summary>
        /// Updates the check.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void UpdateCheck(object parameter)
        {
            AutoUpdater.Start("https://simtuning.tuke-productions.de/download/autoupdater/?wpdmdl=42");
            AutoUpdater.ReportErrors = true;
            AutoUpdater.HttpUserAgent = "SimTuningUpdateCheck";
            AutoUpdater.CheckForUpdateEvent += UpdaterCheckForUpdateEvent;

            Get_Changelog();
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
                Version_new = args.CurrentVersion.ToString();
                Version_now = args.InstalledVersion.ToString();

                if (args.IsUpdateAvailable)
                {
                    UpdateButton = true;
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
                    if (AutoUpdater.DownloadUpdate(args))
                        Application.Current.Shutdown();
                }
                catch
                {
                    //MessageBox.Show("FEHLER");
                }
            }
        }

        #endregion Methods

        #region Values

        #region Commands

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

        private bool _updateButton;
        private string _version_new;
        private string _version_now;

        public bool UpdateButton
        {
            get => _updateButton;
            set => SetProperty(ref _updateButton, value);
        }

        public string Version_new
        {
            get => _version_new;
            set => SetProperty(ref _version_new, value);
        }

        public string Version_now
        {
            get => _version_now;
            set => SetProperty(ref _version_now, value);
        }

        #endregion Values
    }
}