﻿// project=SimTuning.WPF.UI, file=DynoSpectrogramViewModel.cs, creation=2020:7:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    using MaterialDesignThemes.Wpf;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Models;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Dialog;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// WPF-spezifisches Dyno-Spectrogram-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.SpectrogramViewModel" />
    public class DynoSpectrogramViewModel : SimTuning.Core.ViewModels.Dyno.SpectrogramViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoSpectrogramViewModel" />
        /// class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoSpectrogramViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
        {
            _token = messenger.Subscribe<MvxReloaderMessage>(this.ReloadData);

            // override Commands
            this.FilterPlotCommand = new MvxAsyncCommand(FilterPlot);
            this.RefreshSpectrogramCommand = new MvxAsyncCommand(ReloadImageAudioSpectrogram);
            this.RefreshPlotCommand = new MvxAsyncCommand(RefreshPlot);
            this.SpecificGraphCommand = new MvxAsyncCommand(SpecificGraph);

            // datensatz checken CheckDynoData();
        }

        #region Methods

        /// <summary>
        /// Filters the plot.
        /// </summary>
        protected new async Task FilterPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(async () =>
                {
                    await base.FilterPlot().ConfigureAwait(true);

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// Refreshes the plot.
        /// </summary>
        protected new async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    await base.RefreshPlot().ConfigureAwait(true);
                    args.Session.Close();
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// Reloads the image audio spectrogram.
        /// </summary>
        protected new async Task ReloadImageAudioSpectrogram()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(() =>
                {
                    Stream stream = base.ReloadImageAudioSpectrogram();
                    PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    this.DisplayedImage = decoder.Frames[0];

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// Specifics the graph.
        /// TODO: geht nicht mehr!
        /// </summary>
        protected new async Task SpecificGraph()
        {
            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(async () =>
                {
                    await base.SpecificGraph().ConfigureAwait(true);

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// Checks the dyno data.
        /// </summary>
        /// <returns></returns>
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

        #endregion Methods

        #region Values

        private readonly MvxSubscriptionToken _token;
        private BitmapSource _displayedImage;

        public BitmapSource DisplayedImage
        {
            get => _displayedImage;
            set => SetProperty(ref _displayedImage, value);
        }

        #endregion Values
    }
}