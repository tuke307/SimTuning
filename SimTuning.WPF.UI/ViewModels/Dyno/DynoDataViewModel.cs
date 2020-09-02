// project=SimTuning.WPF.UI, file=DynoDataViewModel.cs, creation=2020:7:30 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.WPF.UI.Business;

    /// <summary>
    /// WPF-spezifisches Dyno-Data-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.DataViewModel" />
    public class DynoDataViewModel : SimTuning.Core.ViewModels.Dyno.DataViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDataViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="messenger">The messenger.</param>
        public DynoDataViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
        {
            this.NewDynoCommand = new MvxCommand(this.NewDyno);
            this.DeleteDynoCommand = new MvxCommand(this.DeleteDyno);
            this.SaveDynoCommand = new MvxCommand(this.SaveDyno);
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
                return;
            }

            // Refresh aller Dyno-Datensätze im Dyno-Modul
            var message = new Core.Models.MvxReloaderMessage(this, this.Dyno);

            this._messenger.Publish(message);
        }

        #endregion Methods
    }
}