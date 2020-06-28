using SkiaSharp;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels.Dyno
{
    public class DynoSpectrogramViewModel : SimTuning.ViewModels.Dyno.SpectrogramViewModel
    {
        public DynoSpectrogramViewModel()
        {
            //Commands
            FilterPlotCommand = new Command(async () => await FilterPlot());
            RefreshSpectrogram = new Command(async () => await ReloadImageAudioSpectrogram());
            RefreshPlot = new Command(async () => await Refresh_Plot());
            SpecificGraphCommand = new Command(async () => await SpecificGraph());

            //datensatz checken
            //CheckDynoData();
        }

        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                Task.Run(async () => await MaterialDialog.Instance.SnackbarAsync("Bitte Datensatz auswählen um fortzufahren!"));
                return false;
            }
            else { return true; }
        }

        protected new async Task ReloadImageAudioSpectrogram()
        {
            if (!CheckDynoData())
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

            await Task.Run(() =>
            {
                Stream stream = base.ReloadImageAudioSpectrogram();
                DisplayedImage = ImageSource.FromStream(() => stream);
            }
            );

            await loadingDialog.DismissAsync();
        }

        protected new async Task Refresh_Plot()
        {
            if (!CheckDynoData())
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

            await Task.Run(() => base.Refresh_Plot());

            OnPropertyChanged("PlotAudio");

            await loadingDialog.DismissAsync();
        }

        protected new async Task FilterPlot()
        {
            if (!CheckDynoData())
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

            await Task.Run(() => base.FilterPlot());

            OnPropertyChanged("PlotAudio");

            await loadingDialog.DismissAsync();
        }

        protected new async Task SpecificGraph()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

            await Task.Run(() => base.SpecificGraph());

            OnPropertyChanged("PlotAudio");

            await loadingDialog.DismissAsync();
        }

        public ImageSource DisplayedImage
        {
            get => Get<ImageSource>();
            set => Set(value);
        }
    }
}