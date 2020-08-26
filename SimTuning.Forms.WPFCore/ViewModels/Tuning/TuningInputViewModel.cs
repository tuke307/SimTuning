﻿// project=SimTuning.Forms.WPFCore, file=TuningInputViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.ViewModels.Tuning
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Forms.WPFCore.Business;
    using System.Globalization;

    /// <summary>
    /// WPF-spezifisches Tuning-Input-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Tuning.InputViewModel" />
    public class TuningInputViewModel : SimTuning.Core.ViewModels.Tuning.InputViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TuningInputViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public TuningInputViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, MvvmCross.Plugin.Messenger.IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
        {
        }

        #region Methods

        /// <summary>
        /// Checks the tuning data.
        /// </summary>
        /// <returns></returns>
        private bool CheckTuningData()
        {
            if (Tuning == null)
            {
                Functions.ShowSnackbarDialog(this.rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));
                return false;
            }
            else { return true; }
        }

        /// <summary>
        /// Saves the tuning.
        /// </summary>
        private void SaveTuning()
        {
            if (!CheckTuningData())
                return;
        }

        #endregion Methods
    }
}