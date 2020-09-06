﻿// project=SimTuning.WPF.UI, file=TuningDataViewModel.cs, creation=2020:7:30 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Tuning
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.WPF.UI.Business;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Tuning-Data-ViewModel.
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
            this.NewTuningCommand = new MvxCommand(this.NewTuning);
            this.DeleteTuningCommand = new MvxCommand(this.DeleteTuning);
            this.SaveTuningCommand = new MvxCommand(this.SaveTuning);
        }

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
        /// Deletes the tuning.
        /// </summary>
        /// <param name="obj">The object.</param>
        protected void DeleteTuning()
        {
            if (!base.DeleteTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        /// <summary>
        /// Creates new tuning.
        /// </summary>
        /// <param name="obj">The object.</param>
        protected void NewTuning()
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
        /// Saves the tuning.
        /// </summary>
        /// <param name="obj">The object.</param>
        protected void SaveTuning()
        {
            if (!base.SaveTuning())
            {
                Functions.ShowSnackbarDialog("Fehler beim speichern");
            }
        }

        #endregion Methods
    }
}