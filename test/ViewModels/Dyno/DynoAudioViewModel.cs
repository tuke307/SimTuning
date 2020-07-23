using MaterialDesignThemes.Wpf;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.WPFCore.Views;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimTuning.WPFCore.ViewModels.Dyno
{
    public class DynoAudioViewModel : SimTuning.Core.ViewModels.Dyno.AudioViewModel
    {
        //private readonly MainWindowViewModel mainWindowViewModel;
        private readonly IMvxNavigationService _navigationService;

        public DynoAudioViewModel/*MainWindowViewModel mainWindowViewModel*/(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //this.mainWindowViewModel = mainWindowViewModel; //LoadingScreen
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

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue(rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));
                return false;
            }
            else { return true; }
        }

        protected new async Task OpenFileDialog()
        {
            if (!CheckDynoData())
                return;

            await base.OpenFileDialog();

            if (player != null)
            {
                await ReloadImageAudioSpectrogram();

                BadgeFileOpen = true;
            }
        }

        protected new async Task ReloadImageAudioSpectrogram()
        {
            //mainWindowViewModel.LoadingAnimation = true;
            //await DialogHost.Show(new LoadingView(), "LoadingDialog", delegate (object sender, DialogOpenedEventArgs args)
            //{
            //    args.Session.Close(false);
            //});

            await Task.Run(() =>
            {
                Stream stream = base.ReloadImageAudioSpectrogram();
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                ImageAudioFile = decoder.Frames[0];
            }
            );

            //mainWindowViewModel.LoadingAnimation = false;
        }

        protected new async Task CutBeginn()
        {
            if (player == null)
                return;

            //mainWindowViewModel.LoadingAnimation = true;

            await base.CutBeginn();

            //mainWindowViewModel.LoadingAnimation = false;

            await ReloadImageAudioSpectrogram();
        }

        protected new async Task CutEnd()
        {
            if (player == null)
                return;

            //mainWindowViewModel.LoadingAnimation = true;

            await base.CutEnd().ConfigureAwait(true);

            //mainWindowViewModel.LoadingAnimation = false;

            await ReloadImageAudioSpectrogram();
        }

        #endregion Commands
    }
}