using SimTuning.windows.Business;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimTuning.windows.ViewModels.Dyno
{
    public class DynoSpectrogramViewModel : SimTuning.ViewModels.Dyno.SpectrogramViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public DynoSpectrogramViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel; //LoadingScreen

            //SPECTROGRAM
            FilterPlotCommand = new AsyncCommand(async () => await FilterPlot());
            RefreshSpectrogram = new AsyncCommand(async () => await ReloadImageAudioSpectrogram());
            RefreshPlot = new AsyncCommand(async () => await Refresh_Plot());
            SpecificGraphCommand = new AsyncCommand(async () => await SpecificGraph());

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

        protected new async Task Refresh_Plot()
        {
            if (!CheckDynoData())
                return;

            mainWindowViewModel.LoadingAnimation = true;

            await Task.Run(() => base.Refresh_Plot());

            OnPropertyChanged("PlotAudio");

            mainWindowViewModel.LoadingAnimation = false;
        }

        protected new async Task FilterPlot()
        {
            if (!CheckDynoData())
                return;

            mainWindowViewModel.LoadingAnimation = true;

            await Task.Run(() => base.FilterPlot());

            OnPropertyChanged("PlotAudio");

            mainWindowViewModel.LoadingAnimation = false;
        }

        protected new async Task SpecificGraph()
        {
            mainWindowViewModel.LoadingAnimation = true;

            await Task.Run(() => base.SpecificGraph());

            OnPropertyChanged("PlotAudio");

            mainWindowViewModel.LoadingAnimation = false;
        }

        public BitmapSource DisplayedImage
        {
            get => Get<BitmapSource>();
            set => Set(value);
        }
    }
}