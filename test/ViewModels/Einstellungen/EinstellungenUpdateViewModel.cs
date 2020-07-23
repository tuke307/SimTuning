using AutoUpdaterDotNET;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace SimTuning.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenUpdateViewModel : MvxViewModel
    {
        //private readonly MainWindowViewModel mainWindowViewModel;

        public EinstellungenUpdateViewModel(/*MainWindowViewModel mainWindowViewModel*/)
        {
            //this.mainWindowViewModel = mainWindowViewModel;

            UpdateCheckCommand = new MvxCommand<string>(UpdateCheck);
            StartUpdateCommand = new MvxCommand<string>(StartUpdate);
        }

        public ICommand UpdateCheckCommand { get; set; }
        public ICommand StartUpdateCommand { get; set; }

        private string _version_now;

        public string Version_now
        {
            get => _version_now;
            set => SetProperty(ref _version_now, value);
        }

        private string _version_new;

        public string Version_new
        {
            get => _version_new;
            set => SetProperty(ref _version_new, value);
        }

        private bool _updateButton;

        public bool UpdateButton
        {
            get => _updateButton;
            set => SetProperty(ref _updateButton, value);
        }

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
                Version_new = args.CurrentVersion.ToString();
                Version_now = args.InstalledVersion.ToString();

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
                //var flowDocument = new FlowDocument();
                //var textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                //using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                //{
                //    textRange.Load(fileStream, DataFormats.Rtf);
                //}
                //RTFtext = flowDocument;
            }
        }
    }
}