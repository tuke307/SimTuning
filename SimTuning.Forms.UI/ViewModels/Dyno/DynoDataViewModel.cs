// project=SimTuning.Forms.UI, file=DynoDataViewModel.cs, creation=2020:6:28 Copyright (c)
// 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using SimTuning.Core;
    using SimTuning.Forms.UI.Business;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Xamarin.Essentials;

    /// <summary>
    /// DynoDataViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.DataViewModel" />
    public class DynoDataViewModel : SimTuning.Core.ViewModels.Dyno.DataViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDataViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoDataViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, MvvmCross.Plugin.Messenger.IMvxMessenger messenger)
            : base(logFactory, navigationService, messenger)
        {
            // Commands
            this.NewDynoCommand = new MvxCommand(this.NewDyno);
            this.DeleteDynoCommand = new MvxCommand(this.DeleteDyno);
            this.SaveDynoCommand = new MvxCommand(this.SaveDyno);

            this.ImportDynoCommand = new MvxAsyncCommand(this.ImportDyno);

            this.ShowSettingsCommand = new MvxAsyncCommand(async () => await this.NavigationService.Navigate<DynoRuntimeViewModel>());
        }

        #region Values

        /// <summary>
        /// Gets the show settings command.
        /// </summary>
        /// <value>The show settings command.</value>
        public IMvxAsyncCommand ShowSettingsCommand { get; private set; }

        #endregion Values

        #region Methods

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
        /// Deletes the dyno.
        /// </summary>
        protected override void DeleteDyno()
        {
            try
            {
                base.DeleteDyno();
            }
            catch (Exception)
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        /// <summary>
        /// Exports the dyno.
        /// </summary>
        protected override async void ExportDyno()
        {
            try
            {
                base.ExportDyno();

                await Share.RequestAsync(new ShareFileRequest
                {
                    File = new ShareFile(GeneralSettings.DataExportArchivePath),
                }).ConfigureAwait(true);
            }
            catch (Exception)
            {
                Functions.ShowSnackbarDialog("Fehler beim exportieren");
            }
        }

        /// <summary>
        /// Imports the dyno.
        /// </summary>
        protected new async Task ImportDyno()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://simtuning.tuke-productions.de/wp-content/uploads/DataExport.zip", SimTuning.Core.GeneralSettings.DataExportArchivePath);
            }

            await base.ImportDyno(SimTuning.Core.GeneralSettings.DataExportArchivePath).ConfigureAwait(true);
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
            catch (Exception)
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