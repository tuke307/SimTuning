// project=SimTuning.Forms.WPFCore, file=DynoDataViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.ViewModels.Dyno
{
    using System;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Forms.WPFCore.Business;

    /// <summary>
    ///  WPF-spezifisches Dyno-Data-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.DataViewModel" />
    public class DynoDataViewModel : SimTuning.Core.ViewModels.Dyno.DataViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDataViewModel"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoDataViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this.NewDynoCommand = new MvxCommand(NewDyno);
            this.DeleteDynoCommand = new MvxCommand(DeleteDyno);
            this.SaveDynoCommand = new MvxCommand(SaveDyno);
        }

        #region Methods

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