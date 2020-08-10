// project=SimTuning.Forms.UI, file=DynoSpectrogramViewModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.UI.Business;
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
        public DynoSpectrogramViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //override Commands
            FilterPlotCommand = new MvxAsyncCommand(FilterPlot);
            RefreshSpectrogramCommand = new MvxAsyncCommand(ReloadImageAudioSpectrogram);
            RefreshPlotCommand = new MvxAsyncCommand(RefreshPlot);
            SpecificGraphCommand = new MvxAsyncCommand(SpecificGraph);

            //datensatz checken
            //CheckDynoData();
        }

        private async Task<bool> CheckDynoData()
        {
            if (Dyno == null)
            {
                Functions.ShowSnackbarDialog(rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));

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

            await base.RefreshPlot().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        protected new async Task FilterPlot()
        {
            if (!CheckDynoData().Result)
                return;

            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.FilterPlot().ConfigureAwait(true);

            await loadingDialog.DismissAsync().ConfigureAwait(false);
        }

        protected new async Task SpecificGraph()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: rm.GetString("MES_LOAD", CultureInfo.CurrentCulture)).ConfigureAwait(false);

            await base.SpecificGraph().ConfigureAwait(true);

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