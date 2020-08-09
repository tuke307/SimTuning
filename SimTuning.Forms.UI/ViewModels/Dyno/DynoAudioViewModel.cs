using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using SimTuning.Forms.UI.Business;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoAudioViewModel : SimTuning.Core.ViewModels.Dyno.AudioViewModel
    {
        public DynoAudioViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //Override commands
            OpenFileCommand = new MvxAsyncCommand(() => OpenFileDialog());
            CutBeginnCommand = new MvxAsyncCommand(() => CutBeginn());
            CutEndCommand = new MvxAsyncCommand(() => CutEnd());

            //datensatz checken
            //CheckDynoData();
        }

        #region Commands

        /// <summary>
        /// Opens the file dialog.
        /// </summary>
        protected new async Task OpenFileDialog()
        {
            if (!this.CheckDynoData().Result)
            {
                return;
            }

            FileData fileData = await CrossFilePicker.Current.PickFile(new string[] { ".wav", ".mp3" }).ConfigureAwait(true);

            if (fileData == null)
                return; // user canceled file picking

            await base.OpenFileDialog(fileData).ConfigureAwait(true);

            if (this.player != null)
            {
                await this.ReloadImageAudioSpectrogram().ConfigureAwait(true);

                this.BadgeFileOpen = true;
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

        private async Task<bool> CheckDynoData()
        {
            if (this.Dyno == null)
            {
                Functions.ShowSnackbarDialog(this.rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }
            else
            {
                return true;
            }
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