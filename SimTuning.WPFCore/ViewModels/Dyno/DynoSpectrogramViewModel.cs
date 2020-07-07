using MvvmCross.Commands;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimTuning.WPFCore.ViewModels.Dyno
{
    public class DynoSpectrogramViewModel : SimTuning.Core.ViewModels.Dyno.SpectrogramViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public DynoSpectrogramViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel; //LoadingScreen

            //SPECTROGRAM
            FilterPlotCommand = new MvxAsyncCommand(async () => await FilterPlot());
            RefreshSpectrogramCommand = new MvxAsyncCommand(async () => await ReloadImageAudioSpectrogram());
            RefreshPlotCommand = new MvxAsyncCommand(async () => await RefreshPlot());
            SpecificGraphCommand = new MvxAsyncCommand(async () => await SpecificGraph());

            //datensatz checken
            //CheckDynoData();
        }

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                mainWindowViewModel.NotificationSnackbar.Enqueue("Bitte Datensatz auswählen um fortzufahren!");
                return false;
            }
            else { return true; }
        }

        protected new async Task ReloadImageAudioSpectrogram()
        {
            if (!CheckDynoData())
                return;

            mainWindowViewModel.LoadingAnimation = true;

            await Task.Run(() =>
                {
                    Stream stream = base.ReloadImageAudioSpectrogram();
                    PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    DisplayedImage = decoder.Frames[0];
                });

            mainWindowViewModel.LoadingAnimation = false;
        }

        protected new async Task RefreshPlot()
        {
            if (!CheckDynoData())
                return;

            mainWindowViewModel.LoadingAnimation = true;

            await Task.Run(() => base.RefreshPlot());

            await RaisePropertyChanged("PlotAudio");

            mainWindowViewModel.LoadingAnimation = false;
        }

        protected new async Task FilterPlot()
        {
            if (!CheckDynoData())
                return;

            mainWindowViewModel.LoadingAnimation = true;

            await Task.Run(() => base.FilterPlot());

            await RaisePropertyChanged("PlotAudio");

            mainWindowViewModel.LoadingAnimation = false;
        }

        protected new async Task SpecificGraph()
        {
            mainWindowViewModel.LoadingAnimation = true;

            await Task.Run(() => base.SpecificGraph());

            await RaisePropertyChanged("PlotAudio");

            mainWindowViewModel.LoadingAnimation = false;
        }

        private BitmapSource _displayedImage;

        public BitmapSource DisplayedImage
        {
            get => _displayedImage;
            set => SetProperty(ref _displayedImage, value);
        }
    }
}