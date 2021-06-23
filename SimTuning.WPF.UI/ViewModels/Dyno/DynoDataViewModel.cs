// project=SimTuning.WPF.UI, file=DynoDataViewModel.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    using MaterialDesignThemes.Wpf;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using Plugin.FilePicker;
    using Plugin.FilePicker.Abstractions;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Dialog;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// WPF-spezifisches Dyno-Data-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.DataViewModel" />
    public class DynoDataViewModel : SimTuning.Core.ViewModels.Dyno.DataViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDataViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="messenger">The messenger.</param>
        public DynoDataViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logFactory, navigationService, messenger)
        {
            this.NewDynoCommand = new MvxCommand(this.NewDyno);
            this.DeleteDynoCommand = new MvxCommand(this.DeleteDyno);
            this.SaveDynoCommand = new MvxCommand(this.SaveDyno);

            this.ImportDynoCommand = new MvxAsyncCommand(this.ImportDyno);
        }

        #region Methods

        /// <summary>
        /// Deletes the dyno.
        /// </summary>
        protected override void DeleteDyno()
        {
            try
            {
                base.DeleteDyno();
            }
            catch
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        /// <summary>
        /// Imports the dyno.
        /// </summary>
        protected new async Task ImportDyno()
        {
            //using (var client = new WebClient())
            //{
            //    client.DownloadFile("https://simtuning.tuke-productions.de/wp-content/uploads/DataExport.zip", SimTuning.Core.GeneralSettings.DataExportFilePath);
            //}

            //if (!this.CheckDynoData())
            //{
            //    return;
            //}

            FileData fileData = await CrossFilePicker.Current.PickFile(new string[] { "Zip Datei (*.zip)|*.zip" }).ConfigureAwait(true);

            // user canceled file picking
            if (fileData == null)
            {
                return;
            }

            string filepath = fileData.FilePath;
            fileData.Dispose();

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(async () =>
                {
                    await base.ImportDyno(filepath).ConfigureAwait(true);
                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);

            //await ReloadImageAudioSpectrogram().ConfigureAwait(true);
        }

        /// <summary>
        /// Creates new dyno.
        /// </summary>
        protected override void NewDyno()
        {
            try
            {
                base.NewDyno();
            }
            catch
            {
                Functions.ShowSnackbarDialog("Fehler beim erstellen");
            }
        }

        /// <summary>
        /// Saves the dyno.
        /// </summary>
        protected new void SaveDyno()
        {
            if (!base.SaveDyno())
            {
                Functions.ShowSnackbarDialog("Fehler beim speichern");
            }
        }

        #endregion Methods
    }
}