// project=SimTuning.Forms.WPFCore, file=TuningDataViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.ViewModels.Tuning
{
    using System;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Forms.WPFCore.Business;

    /// <summary>
    ///  WPF-spezifisches Tuning-Data-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Tuning.DataViewModel" />
    public class TuningDataViewModel : SimTuning.Core.ViewModels.Tuning.DataViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TuningDataViewModel"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public TuningDataViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this.NewTuningCommand = new MvxCommand<string>(new Action<object>(NewTuning));
            this.DeleteTuningCommand = new MvxCommand<string>(new Action<object>(DeleteTuning));
            this.SaveTuningCommand = new MvxCommand<string>(new Action<object>(SaveTuning));
        }

        #region Methods

        /// <summary>
        /// Creates new tuning.
        /// </summary>
        /// <param name="obj">The object.</param>
        protected void NewTuning(object obj)
        {
            try
            {
                NewTuning();
            }
            catch
            {
                Functions.ShowSnackbarDialog("Fehler beim erstellen");
            }
        }

        /// <summary>
        /// Deletes the tuning.
        /// </summary>
        /// <param name="obj">The object.</param>
        protected void DeleteTuning(object obj)
        {
            if (!base.DeleteTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        /// <summary>
        /// Saves the tuning.
        /// </summary>
        /// <param name="obj">The object.</param>
        protected void SaveTuning(object obj)
        {
            if (!base.SaveTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim speichern");
            }
        }

        #endregion Methods
    }
}