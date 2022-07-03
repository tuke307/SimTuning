// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Auslass
{
    using MaterialDesignThemes.Wpf;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using SimTuning.Core.Services;
    using SimTuning.WPF.UI.Dialog;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// WPF-spezifisches Auslass-Anwendung-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Auslass.AnwendungViewModel" />
    public class AuslassAnwendungViewModel : SimTuning.Core.ViewModels.Auslass.AnwendungViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuslassAnwendungViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AuslassAnwendungViewModel(
            ILogger<AuslassAnwendungViewModel> logger,
            IMvxNavigationService navigationService,
            IVehicleService vehicleService)
            : base(logger, navigationService, vehicleService)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            // override command
            this.CalculateCommand = new MvxAsyncCommand(CalculateAsync);

            return base.Initialize();
        }

        /// <inheritdoc cref="Core.ViewModels.Auslass.AnwendungViewModel.Calculate" />
        protected new async Task CalculateAsync()
        {
            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", delegate (object sender, DialogOpenedEventArgs args)
            {
                Stream stream = base.Calculate();
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                Auspuff = decoder.Frames[0];

                args.Session.Close();
            }).ConfigureAwait(true);
        }

        #endregion Methods

        #region Values

        private readonly ILogger<AuslassAnwendungViewModel> _logger;

        private BitmapSource _auspuff;

        public BitmapSource Auspuff
        {
            get => _auspuff;
            set => SetProperty(ref _auspuff, value);
        }

        #endregion Values
    }
}