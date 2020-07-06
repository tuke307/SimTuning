﻿using MvvmCross.Commands;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoSpectrogramViewModel : SimTuning.Core.ViewModels.Dyno.SpectrogramViewModel
    {
        private readonly ResourceManager rm;

        public DynoSpectrogramViewModel()
        {
            //Commands
            FilterPlotCommand = new MvxAsyncCommand(() => FilterPlot());
            RefreshSpectrogramCommand = new MvxAsyncCommand(() => ReloadImageAudioSpectrogram());
            RefreshPlotCommand = new MvxAsyncCommand(() => RefreshPlot());
            SpecificGraphCommand = new MvxAsyncCommand(() => SpecificGraph());

            rm = new ResourceManager("resources", Assembly.GetExecutingAssembly());
            //datensatz checken
            //CheckDynoData();
        }

        private async Task<bool> CheckDynoData()
        {
            if (Dyno == null)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture)).ConfigureAwait(false);
                return false;
            }
            else { return true; }
        }

        protected new async Task ReloadImageAudioSpectrogram()
        {
            if (!CheckDynoData().Result)
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            Stream stream = base.ReloadImageAudioSpectrogram();
            DisplayedImage = ImageSource.FromStream(() => stream);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        protected new async Task RefreshPlot()
        {
            if (!CheckDynoData().Result)
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await Task.Run(() => base.RefreshPlot()).ConfigureAwait(true);

            await RaisePropertyChanged("PlotAudio").ConfigureAwait(false);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        protected new async Task FilterPlot()
        {
            if (!CheckDynoData().Result)
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await Task.Run(() => base.FilterPlot()).ConfigureAwait(true);

            await RaisePropertyChanged("PlotAudio").ConfigureAwait(false);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        protected new async Task SpecificGraph()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await Task.Run(() => base.SpecificGraph()).ConfigureAwait(true);

            await RaisePropertyChanged("PlotAudio").ConfigureAwait(false);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        private ImageSource _displayedImage;

        public ImageSource DisplayedImage
        {
            get => _displayedImage;
            set => SetProperty(ref _displayedImage, value);
        }
    }
}