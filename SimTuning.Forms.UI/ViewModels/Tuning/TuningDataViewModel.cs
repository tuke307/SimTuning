// project=SimTuning.Forms.UI, file=TuningDataViewModel.cs, creation=2020:6:28 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Tuning
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Forms.UI.Business;
    using System;

    /// <summary>
    /// TuningDataViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Tuning.DataViewModel" />
    public class TuningDataViewModel : SimTuning.Core.ViewModels.Tuning.DataViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TuningDataViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public TuningDataViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
        {
            //Commands
            this.NewTuningCommand = new MvxCommand(this.NewTuning);
            this.DeleteTuningCommand = new MvxCommand(this.DeleteTuning);
            this.SaveTuningCommand = new MvxCommand(this.SaveTuning);
        }

        /// <summary>
        /// Deletes the tuning.
        /// </summary>
        protected new void DeleteTuning()
        {
            if (!base.DeleteTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        /// <summary>
        /// Creates new tuning.
        /// </summary>
        protected override void NewTuning()
        {
            try
            {
                base.NewTuning();
            }
            catch (Exception)
            {
                Functions.ShowSnackbarDialog("Fehler beim erstellen");
            }
        }

        /// <summary>
        /// Saves the tuning.
        /// </summary>
        protected new void SaveTuning()
        {
            if (!base.SaveTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim speichern");
            }
        }
    }
}