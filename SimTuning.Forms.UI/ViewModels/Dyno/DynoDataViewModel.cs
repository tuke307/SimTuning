// project=SimTuning.Forms.UI, file=DynoDataViewModel.cs, creation=2020:6:28 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core;
    using SimTuning.Forms.UI.Business;
    using System;
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
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoDataViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, MvvmCross.Plugin.Messenger.IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
        {
            // Commands
            this.NewDynoCommand = new MvxCommand(this.NewDyno);
            this.DeleteDynoCommand = new MvxCommand(this.DeleteDyno);
            this.SaveDynoCommand = new MvxCommand(this.SaveDyno);

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