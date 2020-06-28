using AutoUpdaterDotNET;
using SimTuning.windows.Business;
using SimTuning.windows.ViewModels;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace SimTuning.ViewModels.Einstellungen
{
    public class Einstellungen_UpdateViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public Einstellungen_UpdateViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            UpdateCheckCommand = new ActionCommand(UpdateCheck);
            StartUpdateCommand = new ActionCommand(StartUpdate);
        }

        public string version_now
        {
            get => Get<string>();
            set => Set(value);
        }

        public string version_new
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool UpdateButton
        {
            get => Get<bool>();
            set => Set(value);
        }

        public FlowDocument RTFtext
        {
            get => Get<FlowDocument>();
            set => Set(value);
        }

        public ICommand UpdateCheckCommand { get; set; }
        public ICommand StartUpdateCommand { get; set; }

        private void UpdateCheck(object parameter)
        {
            AutoUpdater.Start("https://simtuning.tuke-productions.de/download/autoupdater/?wpdmdl=42");
            AutoUpdater.ReportErrors = true;
            AutoUpdater.HttpUserAgent = "SimTuningUpdateCheck";
            AutoUpdater.CheckForUpdateEvent += UpdaterCheckForUpdateEvent;

            Get_Changelog();
        }

        private void StartUpdate(object parameter)
        {
            AutoUpdater.Start("https://simtuning.tuke-productions.de/download/autoupdater/?wpdmdl=42");
            AutoUpdater.ReportErrors = true;
            AutoUpdater.HttpUserAgent = "SimTuningDonwloadUpdate";
            AutoUpdater.CheckForUpdateEvent += UpdaterDownloadUpdateEvent;
        }

        private void UpdaterCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                version_new = args.CurrentVersion.ToString();
                version_now = args.InstalledVersion.ToString();

                if (args.IsUpdateAvailable)
                {
                    UpdateButton = true;
                }
            }
        }

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
                var flowDocument = new FlowDocument();
                var textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    textRange.Load(fileStream, DataFormats.Rtf);
                }
                RTFtext = flowDocument;
            }
        }
    }
}