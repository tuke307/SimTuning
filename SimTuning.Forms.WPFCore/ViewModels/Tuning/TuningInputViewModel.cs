// project=SimTuning.Forms.WPFCore, file=TuningInputViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using System.Globalization;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.WPFCore.Business;

namespace SimTuning.Forms.WPFCore.ViewModels.Tuning
{
    public class TuningInputViewModel : SimTuning.Core.ViewModels.Tuning.InputViewModel
    {
        public TuningInputViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        private bool CheckTuningData()
        {
            if (Tuning == null)
            {
                Functions.ShowSnackbarDialog(this.rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));
                return false;
            }
            else { return true; }
        }

        private void SaveTuning()
        {
            if (!CheckTuningData())
                return;
        }
    }
}