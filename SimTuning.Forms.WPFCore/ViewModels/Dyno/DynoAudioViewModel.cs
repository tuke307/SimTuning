// project=SimTuning.Forms.WPFCore, file=DynoAudioViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.SimpleAudioPlayer;
using SimTuning.Forms.WPFCore.Business;
using SimTuning.Forms.WPFCore.Views.Dialog;

namespace SimTuning.Forms.WPFCore.ViewModels.Dyno
{
    public class DynoAudioViewModel : SimTuning.Core.ViewModels.Dyno.AudioViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public DynoAudioViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;

            //override commands
            OpenFileCommand = new MvxAsyncCommand(async () => await OpenFileDialog());
            CutBeginnCommand = new MvxAsyncCommand(async () => await CutBeginn());
            CutEndCommand = new MvxAsyncCommand(async () => await CutEnd());

            //datensatz checken
            //CheckDynoData();
        }

        #region Values

        private BitmapSource _imageAudioFile;

        public BitmapSource ImageAudioFile
        {
            get => _imageAudioFile;
            set => SetProperty(ref _imageAudioFile, value);
        }

        #endregion Values

        #region Commands

        private async Task<bool> CheckDynoDataAsync()
        {
            if (Dyno == null)
            {
                Functions.ShowSnackbarDialog(this.rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

                return false;
            }
            else { return true; }
        }

        protected new async Task OpenFileDialog()
        {
            if (!CheckDynoDataAsync().Result)
                return;

            FileData fileData = await CrossFilePicker.Current.PickFile(new string[] { "WAVE Audio (*.wav)|*.wav", "MP3 Audio (*.mp3)|*.mp3" }).ConfigureAwait(true);

            if (fileData == null)
                return; // user canceled file picking

            await base.OpenFileDialog(fileData);

            if (MediaManager.MediaPlayer != null)
            {
                await ReloadImageAudioSpectrogram();

                BadgeFileOpen = true;
            }
        }

        protected override void OpenFile()
        {
            //initialisieren
            var stream = File.OpenRead(SimTuning.Core.Constants.AudioFilePath);
            //MediaManager = CrossSimpleAudioMediaManager.Current;
            MediaManager.Play(stream, SimTuning.Core.Constants.AudioFile);
            //ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            //player.Load(stream);
            //player.Play();
            stream.Dispose();

            base.OpenFile();
        }

        protected new async Task ReloadImageAudioSpectrogram()
        {
            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", delegate (object sender, DialogOpenedEventArgs args)
            {
                Stream stream = base.ReloadImageAudioSpectrogram();
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                ImageAudioFile = decoder.Frames[0];

                args.Session.Close();
            }).ConfigureAwait(true);
        }

        protected new async Task CutBeginn()
        {
            if (MediaManager.MediaPlayer == null)
                return;

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", async delegate (object sender, DialogOpenedEventArgs args)
            {
                await base.CutBeginn();

                args.Session.Close();
            }).ConfigureAwait(true);

            await ReloadImageAudioSpectrogram();
        }

        protected new async Task CutEnd()
        {
            if (MediaManager.MediaPlayer == null)
                return;

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", async delegate (object sender, DialogOpenedEventArgs args)
            {
                await base.CutEnd();

                args.Session.Close();
            }).ConfigureAwait(true);

            await ReloadImageAudioSpectrogram();
        }

        #endregion Commands
    }
}