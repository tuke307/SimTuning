using MvvmCross.Commands;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoSpectrogramViewModel : SimTuning.Core.ViewModels.Dyno.SpectrogramViewModel
    {
        public DynoSpectrogramViewModel()
        {
            //Commands
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

        protected new async Task RefreshPlot()
        {
            if (!CheckDynoData())
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

            await Task.Run(() => base.RefreshPlot());

            RaisePropertyChanged("PlotAudio");

            await loadingDialog.DismissAsync();
        }

        protected new async Task FilterPlot()
        {
            if (!CheckDynoData())
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

            await Task.Run(() => base.FilterPlot());

            RaisePropertyChanged("PlotAudio");

            await loadingDialog.DismissAsync();
        }

        protected new async Task SpecificGraph()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Laden");

            await Task.Run(() => base.SpecificGraph());

            await RaisePropertyChanged("PlotAudio");

            await loadingDialog.DismissAsync();
        }

        private ImageSource _displayedImage;

        public ImageSource DisplayedImage
        {
            get => _displayedImage;
            set => SetProperty(ref _displayedImage, value);
        }
    }
}