// project=SimTuning.Forms.UI, file=TuningInputViewModel.cs, creation=2020:6:28 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Tuning
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Forms.UI.Business;
    using System.Globalization;

    /// <summary>
    /// TuningInputViewModel.
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

        /// <summary>
        /// Checks the tuning data.
        /// </summary>
        /// <returns></returns>
        private bool CheckTuningData()
        {
            if (this.Tuning == null)
            {
                Functions.ShowSnackbarDialog(rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Saves the tuning.
        /// </summary>
        private void SaveTuning()
        {
            if (!this.CheckTuningData())
            {
                return;
            }
        }
    }
}