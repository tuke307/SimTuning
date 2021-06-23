using MediaManager;
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using SimTuning.Core.ViewModels.Dyno;
using System.IO;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoAudioPlayerViewModel : AudioPlayerViewModel
    {
        public DynoAudioPlayerViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, IMediaManager mediaManager)
              : base(logFactory, navigationService, mediaManager)
        {
            this.CutBeginnCommand = new MvxAsyncCommand(this.CutBeginn);
            this.CutEndCommand = new MvxAsyncCommand(this.CutEnd);
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
        /// Cuts the beginn.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected new async Task CutBeginn()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message:
            SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            await base.CutBeginn().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);

            //await this.ReloadImageAudioSpectrogram().ConfigureAwait(false);
        }

        /// <summary>
        /// Cuts the end.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected new async Task CutEnd()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            await base.CutEnd().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);

            //await this.ReloadImageAudioSpectrogram().ConfigureAwait(false);
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <returns>
        /// <placeholder>A <see cref="Task" /> representing the asynchronous
        /// operation.</placeholder>
        /// </returns>
        protected override async Task PlayFileAsync()
        {
            var check = this.CheckDynoData();
            if (!check)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_LOAD")).ConfigureAwait(false);

            await base.PlayFileAsync().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Checks the dyno data.
        /// </summary>
        /// <returns></returns>
        private bool CheckDynoData()
        {
            if (!File.Exists(SimTuning.Core.GeneralSettings.AudioAccelerationFilePath))
            {
                Forms.UI.Business.Functions.ShowSnackbarDialog(SimTuning.Core.Business.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NOAUDIOFILE"));

                return false;
            }

            return true;
        }
    }
}