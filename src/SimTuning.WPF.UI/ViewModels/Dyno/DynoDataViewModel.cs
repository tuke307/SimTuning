// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    using MaterialDesignThemes.Wpf;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using Plugin.FilePicker;
    using Plugin.FilePicker.Abstractions;
    using SimTuning.Core.Services;
    using SimTuning.WPF.UI.Dialog;
    using SimTuning.WPF.UI.Messages;
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
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="messenger">The messenger.</param>
        public DynoDataViewModel(
            ILogger<DynoDataViewModel> logger,
            IMvxNavigationService navigationService,
            IVehicleService vehicleService,
            IMvxMessenger messenger)
            : base(logger, navigationService, vehicleService, messenger)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.NewDynoCommand = new MvxCommand(this.NewDyno);
            this.DeleteDynoCommand = new MvxCommand(this.DeleteDyno);
            this.SaveDynoCommand = new MvxCommand(this.SaveDyno);

            this.ImportDynoCommand = new MvxAsyncCommand(this.ImportDyno);

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.DataViewModel.DeleteDyno" />
        protected new void DeleteDyno()
        {
            if (!base.DeleteDyno())
            {
                _messenger.Publish(new ShowSnackbarMessage(this, "Fehler beim löschen"));
            }
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.DataViewModel.ImportDyno(string)" />
        protected new async Task ImportDyno()
        {
            // using (var client = new WebClient()) {
            // client.DownloadFile("https://simtuning.tuke-productions.de/wp-content/uploads/DataExport.zip",
            // SimTuning.Core.GeneralSettings.DataExportFilePath); }

            // if (!this.CheckDynoData()) { return; }

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

            // await ReloadImageAudioSpectrogram().ConfigureAwait(true);
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.DataViewModel.NewDyno" />
        protected new void NewDyno()
        {
            if (!base.NewDyno())
            {
                _messenger.Publish(new ShowSnackbarMessage(
                    this,
                    "Fehler beim erstellen"));
            }
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.DataViewModel.SaveDyno" />
        protected new void SaveDyno()
        {
            if (!base.SaveDyno())
            {
                _messenger.Publish(
                    new ShowSnackbarMessage(
                        this,
                        "Fehler beim speichern"));
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<DynoDataViewModel> _logger;

        #endregion Values
    }
}