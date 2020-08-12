// project=SimTuning.Forms.UI, file=TuningDiagnosisViewModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Tuning
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Forms.UI.Business;
    using System.Globalization;
    using System.Resources;
    using System.Threading.Tasks;

    /// <summary>
    /// TuningDiagnosisViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Tuning.DiagnosisViewModel" />
    public class TuningDiagnosisViewModel : SimTuning.Core.ViewModels.Tuning.DiagnosisViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TuningDiagnosisViewModel" />
        /// class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public TuningDiagnosisViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            // messages
            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
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
            else { return true; }
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