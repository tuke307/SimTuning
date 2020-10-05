using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using SimTuning.Core.Business;
using SimTuning.Core.ViewModels.Dyno;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    public class DynoBeschleunigungViewModel : BeschleunigungViewModel
    {
        public DynoBeschleunigungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
        {
            this.RefreshPlotCommand = new MvxAsyncCommand(this.RefreshPlot);

            //this.ShowAusrollenCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<DynoAusrollenViewModel>());
        }

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
        /// Refreshes the plot.
        /// </summary>
        protected new async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            //var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.RefreshPlot().ConfigureAwait(true);

            //await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        private bool CheckDynoData()
        {
            if (this.Dyno == null)
            {
                //Forms.UI.Business.Functions.ShowSnackbarDialog(this.rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }

            if (this.Dyno.Beschleunigung == null)
            {
                //Forms.UI.Business.Functions.ShowSnackbarDialog(this.rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }

            return true;
        }
    }
}