using MaterialDesignThemes.Wpf;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Forms.WPFCore.Business;
using SimTuning.Forms.WPFCore.Views.Dialog;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimTuning.Forms.WPFCore.ViewModels.Dyno
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

        private bool CheckDynoData()
        {
            if (this.Dyno == null)
            {
                Functions.ShowSnackbarDialog(rm.GetString("ERR_NODATA", CultureInfo.CurrentCulture));
                return false;
            }
            else
            {
                return true;
            }
        }

        protected new async Task ReloadImageAudioSpectrogram()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", async delegate (object sender, DialogOpenedEventArgs args)
            {
                Stream stream = base.ReloadImageAudioSpectrogram();
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                DisplayedImage = decoder.Frames[0];

                args.Session.Close();
            }).ConfigureAwait(true);
        }

        protected new async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", async delegate (object sender, DialogOpenedEventArgs args)
            {
                await base.RefreshPlot();

                args.Session.Close();
            }).ConfigureAwait(true);
        }

        protected new async Task FilterPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", async delegate (object sender, DialogOpenedEventArgs args)
            {
                await base.FilterPlot().ConfigureAwait(true);

                args.Session.Close();
            }).ConfigureAwait(true);
        }

        protected new async Task SpecificGraph()
        {
            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", async delegate (object sender, DialogOpenedEventArgs args)
            {
                await base.SpecificGraph().ConfigureAwait(true);

                args.Session.Close();
            }).ConfigureAwait(true);
        }

        private BitmapSource _displayedImage;

        public BitmapSource DisplayedImage
        {
            get => _displayedImage;
            set => SetProperty(ref _displayedImage, value);
        }
    }
}