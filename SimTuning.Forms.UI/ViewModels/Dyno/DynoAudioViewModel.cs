// project=SimTuning.Forms.UI, file=DynoAudioViewModel.cs, creation=2020:6:28 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Dyno
{
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

    /// <summary>
    /// DynoAudioViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.AudioViewModel" />
    public class DynoAudioViewModel : SimTuning.Core.ViewModels.Dyno.AudioViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoAudioViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoAudioViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            // Override commands
            this.OpenFileCommand = new MvxAsyncCommand(this.OpenFileDialog);
            this.CutBeginnCommand = new MvxAsyncCommand(this.CutBeginn);
            this.CutEndCommand = new MvxAsyncCommand(this.CutEnd);

            // datensatz checken CheckDynoData();
        }

        #region Methods

        /// <summary>
        /// Cuts the beginn.
        /// </summary>
        protected new async Task CutBeginn()
        {
            if (this.player == null)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.CutBeginn().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);

            await this.ReloadImageAudioSpectrogram().ConfigureAwait(false);
        }

        /// <summary>
        /// Cuts the end.
        /// </summary>
        protected new async Task CutEnd()
        {
            if (this.player == null)
            {
                return;
            }

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.CutEnd().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);

            await this.ReloadImageAudioSpectrogram().ConfigureAwait(false);
        }

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
            {
                return; // user canceled file picking
            }

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
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: this.rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            Stream stream = base.ReloadImageAudioSpectrogram();
            this.ImageAudioSpectrogram = ImageSource.FromStream(() => stream);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Checks the dyno data.
        /// </summary>
        /// <returns></returns>
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

        #endregion Methods

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