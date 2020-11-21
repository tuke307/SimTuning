// project=SimTuning.Forms.UI, file=DynoDiagnosisViewModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Forms.UI.Business;
    using System.Globalization;
    using System.Threading.Tasks;
    using XF.Material.Forms.UI.Dialogs;

    /// <summary>
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel" />
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDiagnosisViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoDiagnosisViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, MvvmCross.Plugin.Messenger.IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
        {
            // override Commands
            this.RefreshPlotCommand = new MvxAsyncCommand(this.RefreshPlot);

            // datensatz checken CheckDynoData();
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

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            base.RefreshPlot();

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Checks the dyno data.
        /// </summary>
        /// <returns></returns>
        private bool CheckDynoData()
        {
            if (this.Dyno == null)
            {
                Functions.ShowSnackbarDialog(SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}