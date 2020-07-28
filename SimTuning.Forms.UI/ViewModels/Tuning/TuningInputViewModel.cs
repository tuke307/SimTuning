using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Globalization;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Tuning
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
                Task.Run(() => MaterialDialog.Instance.SnackbarAsync(rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture)));
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