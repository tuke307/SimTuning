using MvvmCross.Commands;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoAudioViewModel : SimTuning.Core.ViewModels.Dyno.AudioViewModel
    {
        public DynoAudioViewModel()
        {
            //Override commands
            OpenFileCommand = new MvxAsyncCommand(() => OpenFileDialog());
            CutBeginnCommand = new MvxAsyncCommand(() => CutBeginn());
            CutEndCommand = new MvxAsyncCommand(() => CutEnd());

            //datensatz checken
            //CheckDynoData();
        }

        #region Commands

        private async Task<bool> CheckDynoData()
        {
            if (Dyno == null)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture)).ConfigureAwait(false);
                return false;
            }
            else { return true; }
        }

        protected new async Task OpenFileDialog()
        {
            if (!CheckDynoData().Result)
                return;

            await base.OpenFileDialog().ConfigureAwait(true);

            if (player != null)
            {
                await ReloadImageAudioSpectrogram().ConfigureAwait(true);

                BadgeFileOpen = true;
            }
        }

        /// <summary>
        /// Aktualisiert das Spectrogram-Bild der Audio Datei
        /// </summary>
        /// <returns></returns>
        protected new async Task ReloadImageAudioSpectrogram()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            Stream stream = base.ReloadImageAudioSpectrogram();
            ImageAudioSpectrogram = ImageSource.FromStream(() => stream);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        protected new async Task CutBeginn()
        {
            if (player == null)
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.CutBeginn().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);

            await ReloadImageAudioSpectrogram().ConfigureAwait(false);
        }

        protected new async Task CutEnd()
        {
            if (player == null)
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.CutEnd().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);

            await ReloadImageAudioSpectrogram().ConfigureAwait(false);
        }

        #endregion Commands

        #region Values

        private ImageSource _imageAudioSpectrogram;

        public ImageSource ImageAudioSpectrogram
        {
            get => _imageAudioSpectrogram;
            private set => SetProperty(ref _imageAudioSpectrogram, value);
        }

        #endregion Values
    }
}