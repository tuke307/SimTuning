// project=SimTuning.Forms.UI, file=TuningDiagnosisViewModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.UI.Business;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Tuning
{
    public class TuningDiagnosisViewModel : SimTuning.Core.ViewModels.Tuning.DiagnosisViewModel
    {
        public TuningDiagnosisViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        /// <summary>
        /// Prepares this instance.
        /// called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            //messages
            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            return base.Initialize();
        }

        private bool CheckTuningData()
        {
            if (this.Tuning == null)
            {
                Functions.ShowSnackbarDialog(rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }
            else { return true; }
        }

        private void SaveTuning()
        {
            if (!this.CheckTuningData())
                return;
        }
    }
}