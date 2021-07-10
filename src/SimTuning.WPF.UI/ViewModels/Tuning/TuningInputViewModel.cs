﻿// project=SimTuning.WPF.UI, file=TuningInputViewModel.cs, creation=2020:9:2 Copyright (c)
// 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Tuning
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using SimTuning.WPF.UI.Business;

    /// <summary>
    /// WPF-spezifisches Tuning-Input-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Tuning.InputViewModel" />
    public class TuningInputViewModel : SimTuning.Core.ViewModels.Tuning.InputViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TuningInputViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public TuningInputViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, MvvmCross.Plugin.Messenger.IMvxMessenger messenger)
            : base(logFactory, navigationService, messenger)
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
                Functions.ShowSnackbarDialog(SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));
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